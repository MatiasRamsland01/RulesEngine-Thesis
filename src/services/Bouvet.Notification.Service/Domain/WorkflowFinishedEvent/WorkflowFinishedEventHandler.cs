using Bouvet.Notification.Service.Statistic;
using Dapr.Client;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using System.Text.Json;

namespace Bouvet.Notification.Service.Domain.WorkflowFinished {
  public class WorkflowFinishedEventHandler : INotificationHandler<WorkflowFinishedEvent> {
    private const string DAPR_SERVICE_NAME = "bouvet-rule-engine-service";
    private const string DAPR_SERVICE_METHOD = "api/v0/getworkflowresult";
    private readonly ILogger<WorkflowFinishedEventHandler> _logger;
    private readonly DaprClient _daprClient;
    public WorkflowFinishedEventHandler(ILogger<WorkflowFinishedEventHandler> logger, DaprClient daprClient) {
      _logger = logger;
      _daprClient = daprClient;
    }
    public async Task Handle(WorkflowFinishedEvent notification, CancellationToken cancellationToken) {
      var result = await _daprClient.InvokeMethodAsync<OperationResult<WorkflowResultDTO>>(HttpMethod.Get, DAPR_SERVICE_NAME, DAPR_SERVICE_METHOD + $"?workflowExecutionId={notification.WorkflowId.Value}", cancellationToken);
      if (result == null) {
        _logger.LogError("Workflow execution result is null");
        return;
      }
      _logger.LogCritical("Workflow execution result is {Result}", JsonSerializer.Serialize(result));
      if (result.Value.WorkflowResult.outcome.Value) {
        _logger.LogCritical("Workflow execution result is true");
        EmployeeNotifcationMetric.TotalNumberOfSatifiedCVs.Inc();
      }
      else {
        _logger.LogCritical("Workflow execution result is false");
        EmployeeNotifcationMetric.TotalNumberOfUnsatifiedCVs.Inc();
        EmployeeNotifcationMetric.NotifyEmployeeCounter.Inc();
      }
    }
  }

}
