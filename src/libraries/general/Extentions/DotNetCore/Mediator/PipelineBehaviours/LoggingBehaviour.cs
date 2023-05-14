// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 02-28-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="LoggingBehaviour.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours {
  /// <summary>
  /// Class LoggingBehaviour.
  /// Implements the <see cref="IPipelineBehavior{TRequest, TResponse}" />
  /// </summary>
  /// <typeparam name="TRequest">The type of the t request.</typeparam>
  /// <typeparam name="TResponse">The type of the t response.</typeparam>
  /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
  public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IOperationResult, new() {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) {
      _logger = logger;
    }

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="next">The next.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>TResponse.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
      // Create a new correlation ID for the current request

      // Log the incoming request
      _logger.LogInformation(
          "Handling request {RequestType}",
          typeof(TRequest).Name
      );

      // Log the request properties using structured logging
      var requestProperties = new Dictionary<string, object>();
      foreach (PropertyInfo prop in request.GetType().GetProperties()) {
        requestProperties[prop.Name] = prop.GetValue(request)!;
      }
      _logger.LogInformation(
          "Request {RequestType} properties: {@Properties}",
          typeof(TRequest).Name,
          requestProperties
      );
      // Call the next handler in the pipeline
      TResponse response = await next();
      //Post Handler. Log if request ran successfully or not
      if (response.Success) {
        _logger.LogInformation("Handled response {ResponseType}", typeof(TResponse).Name);
      }
      else {
        _logger.LogError(response.Errors.ToString(), "Error handling request {RequestType}", typeof(TRequest).Name);
      }
      return response;
    }
  }
}
