// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="ExecuteWorkflowHandler.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Bouvet.Rule.Engine.Service.BackroundService;
using fields.Entities.Base.Fields.Custom;
using FluentValidation;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
namespace Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow {
  /// <summary>
  /// Class ExecuteWorkflow.
  /// </summary>
  public class ExecuteWorkflowHandler : IRequestHandler<ExecuteWorkflowCommand, OperationResult<ExecuteWorkflowDTO>> {
    /// <summary>
    /// The queue
    /// </summary>
    private readonly IBackgroundTaskQueue _queue;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ExecuteWorkflowHandler> _logger;
    private readonly IValidator<IIdField<Guid>> _idValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteWorkflowHandler" /> class.
    /// </summary>
    /// <param name="queue">The queue.</param>
    /// <param name="logger">The logger.</param>
    public ExecuteWorkflowHandler(IBackgroundTaskQueue queue, ILogger<ExecuteWorkflowHandler> logger, IValidator<IIdField<Guid>> idValidator) {
      _queue = queue;
      _logger = logger;
      _idValidator = idValidator;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<ExecuteWorkflowDTO>> Handle(ExecuteWorkflowCommand command, CancellationToken cancellationToken) {
      await _queue.QueueBackgroundWorkItemAsync(new BackgroundModel(command.workflow, command.input, command.workflowId));
      return OperationResult<ExecuteWorkflowDTO>.CreateSuccess(new ExecuteWorkflowDTO(command.workflowId), $"Workflow with id {command.workflowId.Value} added to queue", 200);
    }
  }
}

