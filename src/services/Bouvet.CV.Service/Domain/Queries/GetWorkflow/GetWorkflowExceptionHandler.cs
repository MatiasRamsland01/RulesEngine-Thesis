using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.CV.Service.Domain.Queries.GetWorkflow {
  public class GetWorkflowExceptionHandler : RequestExceptionHandler<GetWorkflowQuery, OperationResult<WorkflowDTO>, Exception> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetWorkflowExceptionHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetWorkflowExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public GetWorkflowExceptionHandler(ILogger<GetWorkflowExceptionHandler> logger) {
      _logger = logger;
    }
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The state.</param>
    protected override void Handle(GetWorkflowQuery query, Exception exception, RequestExceptionHandlerState<OperationResult<WorkflowDTO>> state) {
      var errorMsg = $"Failed to handle query {typeof(GetWorkflowQuery).Name}";
      _logger.LogError(errorMsg);
      var result = OperationResult<WorkflowDTO>.CreateFailure(default!, exception, typeof(Exception).FullName, errorMsg);
      state.SetHandled(result);
    }
  }
}
