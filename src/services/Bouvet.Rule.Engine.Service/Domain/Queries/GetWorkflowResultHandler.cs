using System.Text.Json;
using Dapr.Client;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.Rule.Engine.Service.Domain.Queries {
  public class GetWorkflowResultHandler : IRequestHandler<GetWorkflowResultQuery, OperationResult<WorkflowResultDTO>> {
    private const string DAPR_STATESTORE_WORKFLOW = "ruleengine-statestore-result";
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetWorkflowResultHandler> _logger;
    private readonly DaprClient _daprClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRuleHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="db"></param>
    public GetWorkflowResultHandler(ILogger<GetWorkflowResultHandler> logger, DaprClient daprClient) {
      _logger = logger;
      _daprClient = daprClient;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<WorkflowResultDTO>> Handle(GetWorkflowResultQuery query, CancellationToken cancellationToken) {
      var workflowResult = await _daprClient.GetStateAsync<PipelineResult>(DAPR_STATESTORE_WORKFLOW, query.workflowResultId.Value.ToString());
      if (workflowResult == null) {
        throw new Exception($"WorkflowResult with id {query.workflowResultId.Value} not found");
      }
      return OperationResult<WorkflowResultDTO>.CreateSuccess(new WorkflowResultDTO(workflowResult), $"WorkflowResult fetched successfully", 200);
    }
  }
}
