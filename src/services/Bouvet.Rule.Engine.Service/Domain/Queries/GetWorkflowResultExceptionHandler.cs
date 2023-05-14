using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.Rule.Engine.Service.Domain.Queries {
  public class GetWorkflowResultExceptionHandler : RequestExceptionHandler<GetWorkflowResultQuery, OperationResult<WorkflowResultDTO>, Exception> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetWorkflowResultExceptionHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetWorkflowResultExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public GetWorkflowResultExceptionHandler(ILogger<GetWorkflowResultExceptionHandler> logger) {
      _logger = logger;
    }
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The state.</param>
    protected override void Handle(GetWorkflowResultQuery query, Exception exception, RequestExceptionHandlerState<OperationResult<WorkflowResultDTO>> state) {
      var errorMsg = $"Failed to handle query {typeof(GetWorkflowResultQuery).Name}";
      _logger.LogError(errorMsg);
      state.SetHandled(OperationResult<WorkflowResultDTO>.CreateFailure(default!, exception, typeof(Exception).FullName, errorMsg));
    }
  }
}
