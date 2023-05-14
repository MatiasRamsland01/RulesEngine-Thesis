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
using System.Diagnostics;
using Bouvet.Rule.Engine.Service.BackroundService;
using Bouvet.Rule.Engine.Service.Statistics;
using Dapr.Client;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;

/// <summary>
/// Class QueuedHostedService. This class cannot be inherited.
/// Implements the <see cref="Microsoft.Extensions.Hosting.BackgroundService" />
/// </summary>
/// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
public class QueuedHostedService : BackgroundService {
  private const string DAPR_PUBSUB_NAME = "ruleengine-pubsub";
  private const string DAPR_PUBSUB_WORKFLOW_FINISHED = "WorkflowFinishedEvent";
  private const string DAPR_PUBSUB_WORKFLOW_STARTED = "WorkflowStartedEvent";

  private const string DAPR_STATESTORE_WORKFLOW = "ruleengine-statestore-result";
  /// <summary>
  /// The task queue
  /// </summary>
  private readonly IBackgroundTaskQueue _taskQueue;
  /// <summary>
  /// The logger
  /// </summary>
  private readonly ILogger<QueuedHostedService> _logger;
  /// <summary>
  /// The last ran workflow
  /// </summary>
  private readonly DaprClient _daprClient;
  private readonly HttpClient _httpClient = new HttpClient();
  private BackgroundModel LastRanWorkflow { get; set; } = default!;


  /// <summary>
  /// Initializes a new instance of the <see cref="QueuedHostedService"/> class.
  /// </summary>
  /// <param name="taskQueue">The task queue.</param>
  /// <param name="logger">The logger.</param>
  /// <param name="daprClient"></param>
  public QueuedHostedService(
    IBackgroundTaskQueue taskQueue,
    ILogger<QueuedHostedService> logger,
    DaprClient daprClient) =>
    (_taskQueue, _logger, _daprClient) = (taskQueue, logger, daprClient);
  /// <summary>
  /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
  /// the lifetime of the long running operation(s) being performed.
  /// </summary>
  /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
  /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.</returns>
  /// <remarks>See <see href="https://docs.microsoft.com/dotnet/core/extensions/workers">Worker Services in .NET</see> for implementation guidelines.</remarks>
  protected override Task ExecuteAsync(CancellationToken stoppingToken) {
    _logger.LogInformation("Queued Hosted Service is running.");
    return ProcessTaskQueueAsync(stoppingToken);
  }

  /// <summary>
  /// Process task queue as an asynchronous operation.
  /// </summary>
  /// <param name="stoppingToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken) {
    while (!stoppingToken.IsCancellationRequested) {
      var workItem = await _taskQueue.DequeueAsync(stoppingToken);
      var start = Stopwatch.GetTimestamp();
      RulesEngineMetrics.RuleEngineExecutionCounter.Inc();
      LastRanWorkflow = workItem;
      var engine = new BRuleEngine();
      await PublishEvent(DAPR_PUBSUB_WORKFLOW_STARTED, new WorkflowStartedEvent(FieldsFactory.IdField(Guid.NewGuid()), FieldsFactory.DateField(DateTime.UtcNow.ToShortDateString()), workItem.id), stoppingToken);
      var executeWorkflowResult = await engine.ExecuteWorkflowAsync(workItem.Workflow, workItem.Input, _logger);
      await _daprClient.SaveStateAsync(DAPR_STATESTORE_WORKFLOW, workItem.id.Value.ToString(), executeWorkflowResult);
      await PublishEvent(DAPR_PUBSUB_WORKFLOW_FINISHED, new WorkflowFinishedEvent(FieldsFactory.IdField(Guid.NewGuid()), FieldsFactory.DateField(DateTime.UtcNow.ToShortDateString()), workItem.id), stoppingToken);
      RulesEngineMetrics.RuleEngineExecutionSucceededCounter.Inc();
      var end = Stopwatch.GetTimestamp();
      var elapsed = end - start;
      var elapsedMilliseconds = (double)elapsed / Stopwatch.Frequency;
      RulesEngineMetrics.RuleEngineExecutionTime.Observe(elapsedMilliseconds);
    }
  }
  public async Task PublishEvent(string topic, dynamic data, CancellationToken cancellationToken) {
    await _daprClient.PublishEventAsync(DAPR_PUBSUB_NAME, topic, data, cancellationToken);
  }
  /// <summary>
  /// Stop as an asynchronous operation.
  /// </summary>
  /// <param name="stoppingToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public override async Task StopAsync(CancellationToken stoppingToken) {
    _logger.LogCritical(
        $"{nameof(QueuedHostedService)} is stopping.");

    await base.StopAsync(stoppingToken);
  }
}
