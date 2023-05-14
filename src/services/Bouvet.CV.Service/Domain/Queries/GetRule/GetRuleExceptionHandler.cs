using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.CV.Service.Domain.Queries.GetRule {
  public class GetRuleExceptionHandler : RequestExceptionHandler<GetRuleQuery, OperationResult<RuleDTO>, Exception> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetRuleExceptionHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRuleExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public GetRuleExceptionHandler(ILogger<GetRuleExceptionHandler> logger) {
      _logger = logger;
    }
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The state.</param>
    protected override void Handle(GetRuleQuery query, Exception exception, RequestExceptionHandlerState<OperationResult<RuleDTO>> state) {
      var errorMsg = $"Failed to handle query {typeof(GetRuleQuery).Name}";
      _logger.LogError(errorMsg);
      var result = OperationResult<RuleDTO>.CreateFailure(default!, exception, typeof(Exception).FullName, errorMsg);
      state.SetHandled(result);
    }
  }
}
