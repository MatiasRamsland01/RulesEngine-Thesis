// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-01-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="QueueHostedService.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Bouvet.CV.Service.Domain.Queries.GetWorkflow;
using Bouvet.CV.Service.MetricQueries;
using Dapr.Client;
using fields.Factories;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using System.Text.Json;
using System.Diagnostics;
/// <summary>
/// Class HostedService. This class cannot be inherited.
/// Implements the <see cref="Microsoft.Extensions.Hosting.BackgroundService" />
/// </summary>
/// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
public sealed class CvSchedulerBackgroundService : BackgroundService {
  private const string WORKFLOWNAME = "rulesEngine";
  private const string COMPANY_ID_BOUVET = "4f460a2de8ee2a74be000001";
  private const string DAPR_PUBSUB_NAME = "ruleengine-pubsub";
  private const string CV_UPDATED_EVENT = nameof(CVUpdatedEvent);

  /// <summary>
  /// The logger
  /// </summary>
  public readonly ILogger<CvSchedulerBackgroundService> _logger;
  /// <summary>
  /// The last ran workflow
  /// </summary>
  public CvPartnerClient? _cvPartnerClient;
  public DaprClient? _daprClient;
  public IMediator? _mediator;

  public readonly IServiceScopeFactory _serviceScopeFactory;

  /// <summary>
  /// Initializes a new instance of the <see cref="HostedService"/> class.
  /// </summary>
  /// <param name="cvPartnerClient">The CV partner HTTP client.</param>
  /// <param name="logger">The logger.</param>
  /// <param name="daprClient"></param>
  public CvSchedulerBackgroundService(ILogger<CvSchedulerBackgroundService> logger, IServiceScopeFactory serviceScopeFactory) => (_logger, _serviceScopeFactory) = (logger, serviceScopeFactory);

  protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
    Task.Delay(10000, cancellationToken).Wait();
    // Log that the service has started
    _logger.LogInformation("CV Background Scheduler started at: {time}", DateTime.Now);
    using (var scope = _serviceScopeFactory.CreateScope()) {
      while (!cancellationToken.IsCancellationRequested) {
        var start = Stopwatch.GetTimestamp();
        _cvPartnerClient = scope.ServiceProvider.GetRequiredService<CvPartnerClient>();
        _daprClient = scope.ServiceProvider.GetRequiredService<DaprClient>();
        _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await GetCvsAsync(cancellationToken);
        var timeToWait = TimeToWait();
        _logger.LogInformation("ExecuteAsync finished, waiting for next execution in: {Milli}", timeToWait);
        var end = Stopwatch.GetTimestamp();
        var elapsed = end - start;
        var elapsedSeconds = (double)elapsed / Stopwatch.Frequency;
        CVMetrics.CVFetchingProcessTime.IncTo(elapsedSeconds);
        Task.Delay(Convert.ToInt32(timeToWait.TotalMilliseconds), cancellationToken).Wait();
      }
    }
  }
  public async Task GetCvsAsync(CancellationToken cancellationToken) {
    _logger.LogInformation($"{nameof(CvSchedulerBackgroundService)} started fetching cv's at: {DateTimeOffset.Now}{Environment.NewLine}");
    var users = await GetAllUsersSummary(cancellationToken);
    await GetAllDetailedCvs(users, cancellationToken);
    _logger.LogInformation($"{nameof(CvSchedulerBackgroundService)} finished fetching cv's at: {DateTimeOffset.Now}{Environment.NewLine}");
  }
  public async Task<SwaggerResponse<ICollection<Anonymous>>> GetAllUsersSummary(CancellationToken cancellationToken) {
    var usersResult = await _cvPartnerClient!.UsersAsync();
    if (usersResult?.Result.Count() < 1 || usersResult?.Result == null) {
      _logger.LogError("There is no CV's in the list to use for fetching detailed CV's");
      throw new Exception("There is no CV's in the list to use for fetching detailed CV's");
    }
    return usersResult;
  }
  public async Task GetAllDetailedCvs(SwaggerResponse<ICollection<Anonymous>> listOfUsers, CancellationToken cancellationToken) {
    // if (listOfUsers?.Result.Count() < 1 || listOfUsers?.Result == null) {
    //   _logger.LogError("There is no CV's in the list to use for fetching detailed CV's");
    //   throw new Exception("There is no CV's in the list to use for fetching detailed CV's");
    // }
    // foreach (var user in listOfUsers.Result) {
    //   // Check if the stoppingToken hasn't been cancelled
    //   if (!cancellationToken.IsCancellationRequested && user.Company_id == COMPANY_ID_BOUVET) {
    //     var CV_Response = ModifyInput(await _cvPartnerClient!.CvsAsync(user.User_id, user.Default_cv_id));
    //     var cvCollection = await _daprClient!.GetStateAsync<List<Response>?>("ruleengine-statestore-cv", CV_Response.Result.Email);
    //     CVMetrics.CVFetchCounter.Inc();
    //     if (CVHasNotBeenUpdated(cvCollection, CV_Response)) { continue; }
    //     await SaveToStateStore(cvCollection, CV_Response.Result);
    //     var workflow = await GetWorkflow()!;
    //     await InvokeRuleEngine(workflow, CV_Response.Result);
    //     Task.Delay(Convert.ToInt32(100), cancellationToken).Wait();
    //   }

    // }
    if (listOfUsers?.Result.Count() < 1 || listOfUsers?.Result == null) {
      _logger.LogError("There is no CV's in the list to use for fetching detailed CV's");
      throw new Exception("There is no CV's in the list to use for fetching detailed CV's");
    }
    var workflow = await GetWorkflow();
    List<Task> tasks = new List<Task>();
    foreach (var user in listOfUsers.Result) {
      // Check if the stoppingToken hasn't been cancelled
      if (!cancellationToken.IsCancellationRequested && user.Company_id == COMPANY_ID_BOUVET) {
        tasks.Add(Task.Run(async () => {
          var CV_Response = ModifyInput(await _cvPartnerClient!.CvsAsync(user.User_id, user.Default_cv_id));
          var cvCollection = await _daprClient!.GetStateAsync<List<Response>?>("ruleengine-statestore-cv", CV_Response.Result.Email);
          CVMetrics.CVFetchCounter.Inc();
          if (CVHasNotBeenUpdated(cvCollection, CV_Response)) { return; }
          await SaveToStateStore(cvCollection, CV_Response.Result);
          await InvokeRuleEngine(workflow, CV_Response.Result);
        }));
      }
    }
    await Task.WhenAll(tasks);
  }
  public bool CVHasNotBeenUpdated(IEnumerable<Response>? cvCollection, SwaggerResponse<Response> fetchedCv) {
    if (CVDoesNotExsist(cvCollection, fetchedCv)) { return false; }
    if (CVHasChanged(cvCollection, fetchedCv)) { return false; }
    return true;
  }
  public bool CVHasChanged(IEnumerable<Response>? cvCollection, SwaggerResponse<Response> fetchedCv) {
    if (cvCollection != null && JsonSerializer.Serialize(cvCollection.Last()) != JsonSerializer.Serialize(fetchedCv.Result) && fetchedCv.Result != null && fetchedCv.Result.Email != null) {
      CVMetrics.CVUpdatedCounter.Inc();
      return true;
    }
    return false;
  }
  public bool CVDoesNotExsist(IEnumerable<Response>? cvCollection, SwaggerResponse<Response> fetchedCv) {
    if (cvCollection == null && fetchedCv.Result.Email != null && fetchedCv.Result != null) {
      return true;
    }
    return false;
  }
  public async Task<BWorkflow> GetWorkflow() {
    // This is done so the CV will be stored in the database no matter if there is something wrong with the workflow
    OperationResult<WorkflowDTO> operationResult = await _mediator!.Send(new GetWorkflowQuery(FieldsFactory.StringField("rulesEngine"))) ?? throw new Exception("Unable to retrieve workflow");
    BWorkflow workflow = operationResult.Value.Workflow;
    if (workflow == null) {
      _logger.LogError("Unable to retrieve workflow");
      throw new Exception("Unable to retrieve workflow");
    }
    return workflow;
  }
  public SwaggerResponse<Response> ModifyInput(SwaggerResponse<Response> input) {
    if (input.Result == null || input.Result.Image == null || input.Result.Image.Url == null) {
      return input;
    }
    input.Result.Image.Url = "image";
    return input;
  }
  public TimeSpan TimeToWait() {
    var currentTime = DateTime.Now;
    var targetTime = DateTime.Today.AddHours(7);
    // If the current time is after the target time, schedule for the next day
    if (currentTime > targetTime) {
      targetTime = targetTime.AddDays(1);
    }
    // Calculate the time until the next target time
    return targetTime.Subtract(currentTime);
  }
  public async Task<object> InvokeRuleEngine(BWorkflow workflow, Response cv) {
    var contract = new ExecuteWorkflowCommandContractData(workflow, new BInputs(cv));
    var request = _daprClient!.CreateInvokeMethodRequest<ExecuteWorkflowCommandContractData>(HttpMethod.Post, "bouvet-rule-engine-service", "api/v0/executeworkflow", contract);
    return await SendRequest(request);
  }
  public async Task<object> SendRequest(HttpRequestMessage request) => await _daprClient!.InvokeMethodAsync<object>(request);
  public async Task SaveToStateStore(List<Response>? cvCollection, Response cv) {
    if (cvCollection == null) {
      cvCollection = new List<Response>();
    }
    cvCollection.Add(cv);
    await _daprClient!.SaveStateAsync("ruleengine-statestore-cv", cv.Email, cvCollection);
    await _daprClient!.PublishEventAsync(DAPR_PUBSUB_NAME, CV_UPDATED_EVENT, new CVUpdatedEvent(FieldsFactory.IdField(Guid.NewGuid()), FieldsFactory.DateField(DateTime.UtcNow.ToLongTimeString()), FieldsFactory.StringField(cv.Email!)));
  }
  public override async Task StopAsync(CancellationToken cancellationToken) {
    _logger.LogCritical(
        $"{nameof(CvSchedulerBackgroundService)} is stopping.");
    await base.StopAsync(cancellationToken);
  }
}
