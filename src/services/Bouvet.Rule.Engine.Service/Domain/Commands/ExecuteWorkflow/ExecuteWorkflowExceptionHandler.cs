// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="ExecuteWorkflowExceptionHandler.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text.Json;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR.Pipeline;

namespace Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow {
  /// <summary>
  /// Class ExecuteWorkflowExceptionHandler.
  /// Implements the <see cref="RequestExceptionHandler{ExecuteWorkflowCommand, OperationResult, Exception}" />
  /// </summary>
  /// <seealso cref="RequestExceptionHandler{ExecuteWorkflowCommand, OperationResult, Exception}" />
  public class ExecuteWorkflowExceptionHandler : RequestExceptionHandler<ExecuteWorkflowCommand, OperationResult<ExecuteWorkflowDTO>, Exception> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ExecuteWorkflowExceptionHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteWorkflowExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ExecuteWorkflowExceptionHandler(ILogger<ExecuteWorkflowExceptionHandler> logger) {
      _logger = logger;
    }
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The state.</param>
    protected override void Handle(ExecuteWorkflowCommand command, Exception exception, RequestExceptionHandlerState<OperationResult<ExecuteWorkflowDTO>> state) {
      var errorMsg = $"Failed to handle command {typeof(ExecuteWorkflowCommand).Name} with correlationId {command.workflowId.Value}";
      _logger.LogError(errorMsg);
      var result = OperationResult<ExecuteWorkflowDTO>.CreateFailure(default!, exception, typeof(Exception).FullName, errorMsg);
      state.SetHandled(result);
    }
  }
}
