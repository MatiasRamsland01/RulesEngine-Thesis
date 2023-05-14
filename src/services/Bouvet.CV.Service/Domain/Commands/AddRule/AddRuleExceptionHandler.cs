using System.Text.Json;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.CV.Service.Domain.Commands.AddRule {
  public class AddRuleExceptionHandler : RequestExceptionHandler<AddRuleCommand, OperationResult<AddRuleCommand>, Exception> {
    private readonly ILogger<AddRuleExceptionHandler> _logger;
    public AddRuleExceptionHandler(ILogger<AddRuleExceptionHandler> logger) {
      _logger = logger;
    }
    protected override void Handle(AddRuleCommand command, Exception exception, RequestExceptionHandlerState<OperationResult<AddRuleCommand>> state) {
      var errorMsg = $"Failed to handle command {typeof(AddRuleCommand).Name}";
      _logger.LogError(errorMsg);
      var result = OperationResult<AddRuleCommand>.CreateFailure(command, exception, typeof(Exception).FullName, errorMsg);
      state.SetHandled(result);
    }
  }
}
