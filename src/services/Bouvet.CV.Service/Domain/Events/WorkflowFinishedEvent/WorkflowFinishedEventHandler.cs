using Bouvet.CV.Service.Domain.Services;
using Dapr.Client;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using System.Text.Json;

namespace Bouvet.Notification.Service.Domain.Events.WorkflowFinished {
  public class WorkflowFinishedEventHandler : INotificationHandler<WorkflowFinishedEvent> {
    private const string DAPR_SERVICE_NAME = "bouvet-rule-engine-service";
    private const string DAPR_SERVICE_METHOD = "api/v0/getworkflowresult";
    private readonly ILogger<WorkflowFinishedEventHandler> _logger;
    private readonly DaprClient _daprClient;
    private readonly ICVService _cvService;
    public WorkflowFinishedEventHandler(ILogger<WorkflowFinishedEventHandler> logger, DaprClient daprClient, ICVService cvService) {
      _logger = logger;
      _daprClient = daprClient;
      _cvService = cvService;
    }
    public async Task Handle(WorkflowFinishedEvent notification, CancellationToken cancellationToken) {
      var result = await _daprClient.InvokeMethodAsync<OperationResult<WorkflowResultDTO>>(HttpMethod.Get, DAPR_SERVICE_NAME, DAPR_SERVICE_METHOD + $"?workflowExecutionId={notification.WorkflowId.Value}", cancellationToken);
      if (result == null) {
        _logger.LogError("Workflow execution result is null");
        return;
      }
      foreach (var rule in result.Value.WorkflowResult.RulesRan) {
        if (!result.Value.WorkflowResult.RulesFailed.Any(failedRule => failedRule.Value == rule.Value)) {
          await _cvService.UpdateCVSuccessCount(rule);
        }
        else {
          await _cvService.UpdateCVFailureCount(rule);
        }
      }
    }
  }
}
