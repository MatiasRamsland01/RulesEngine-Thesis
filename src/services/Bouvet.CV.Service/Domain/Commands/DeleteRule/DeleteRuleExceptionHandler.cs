using System.Text.Json;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.CV.Service.Domain.Commands.DeleteRule {
  public class AddRuleExceptionHandler : RequestExceptionHandler<DeleteRuleCommand, OperationResult<DeleteRuleCommand>, Exception> {
    private readonly ILogger<AddRuleExceptionHandler> _logger;
    public AddRuleExceptionHandler(ILogger<AddRuleExceptionHandler> logger) {
      _logger = logger;
    }
    protected override void Handle(DeleteRuleCommand command, Exception exception, RequestExceptionHandlerState<OperationResult<DeleteRuleCommand>> state) {
      var errorMsg = $"Failed to handle command {typeof(DeleteRuleCommand).Name}";
      _logger.LogError(errorMsg);
      var result = OperationResult<DeleteRuleCommand>.CreateFailure(default!, exception, typeof(Exception).FullName, errorMsg);
      state.SetHandled(result);
    }
  }
}
