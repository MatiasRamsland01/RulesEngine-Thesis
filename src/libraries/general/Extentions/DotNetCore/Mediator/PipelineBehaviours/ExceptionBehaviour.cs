using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours {
  public class ExceptionBehaviour<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException> where TResponse : IOperationResult, new()
           where TRequest : IRequest<TResponse>
           where TException : Exception {
    private readonly ILogger<ExceptionBehaviour<TRequest, TResponse, TException>> _logger;

    public ExceptionBehaviour(ILogger<ExceptionBehaviour<TRequest, TResponse, TException>> logger) {
      _logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken) {
      _logger.LogError(exception, "Something went wrong while handling request of type {@requestType}", typeof(TRequest));

      // TODO: when we want to show the user somethig went wrong, we need to expand this with something like
      // a ResponseBase where we wrap the actual response and return an indication whether the call was successful or not.
      Error error = Error.FromException(exception, exception.Message);
      var response = new TResponse() {
        Success = false,
        Errors = new List<Error>() { error },
        Message = $"Something went wrong while handling request of type {typeof(TRequest).Name}",
        HttpStatusCode = 400
      };
      state.SetHandled(response);
      return Task.CompletedTask;
    }
  }
}
