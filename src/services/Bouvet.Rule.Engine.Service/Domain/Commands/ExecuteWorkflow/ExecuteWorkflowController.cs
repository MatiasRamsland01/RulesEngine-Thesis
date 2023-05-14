// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="ExecuteWorkflowController.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text.Json;
using fields.Entities.Base.Fields.Custom;
using FluentValidation;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class ExecuteWorkflowController : ControllerBase {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The string validator
    /// </summary>
    private readonly IValidator<IIdField<Guid>> _idValidator;


    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ExecuteWorkflowController> logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public ExecuteWorkflowController(
    ILogger<ExecuteWorkflowController> logger,
    IMediator mediator,
    IValidator<IIdField<Guid>> idValidator
    ) {
      this.logger = logger;
      _mediator = mediator;
      _idValidator = idValidator;

    }
    /// <summary>
    /// Executes the workflow.
    /// </summary>
    /// <returns>ActionResult&lt;PipelineResult&gt;.</returns>
    [HttpPost("executeworkflow")]
    public async Task<OperationResult<ExecuteWorkflowDTO>> ExecuteWorkflow([FromBody] ExecuteWorkflowCommandContractData contract) {
      logger.LogCritical("Workflow: {workflow}", JsonSerializer.Serialize(contract.Workflow));
      var correlationId = Guid.NewGuid();
      var result = await _mediator.Send(new ExecuteWorkflowCommand(new IdField<Guid>(correlationId, _idValidator), contract.Workflow, contract.Input));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }
}
