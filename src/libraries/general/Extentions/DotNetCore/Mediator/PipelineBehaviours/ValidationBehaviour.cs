// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-01-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-01-2023
// ***********************************************************************
// <copyright file="ValidationBehaviour.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours {


  /// <summary>
  /// Class ValidationBehaviour.
  /// Implements the <see cref="IPipelineBehavior{TRequest, TResponse}" />
  /// </summary>
  /// <typeparam name="TRequest">The type of the t request.</typeparam>
  /// <typeparam name="TResponse">The type of the t response.</typeparam>
  /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
  public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IOperationResult, new() {
    /// <summary>
    /// The validators
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehaviour{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validators.</param>
    /// <param name="logger">The logger.</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logger) {
      _validators = validators;
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
      if (_validators.Any()) {
        _logger.LogInformation("Validating request {RequestType}", typeof(TRequest).Name);
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Count != 0) {
          _logger.LogError("Validation errors - {RequestType} - Command: {@Command} - Errors: {@ValidationErrors}", typeof(TRequest).Name, request, failures);
          throw new ValidationException(failures);
        };
      }
      return await next();
    }
  }
}
