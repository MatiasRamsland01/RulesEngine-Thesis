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
using Dapr;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Contracts.Bouvet.Rule.Engine.Service.Events.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.Rule.Engine.Service.Domain.Commands.WorkflowFinished {

  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class WorkflowFinishedEventController : ControllerBase {
    private const string DAPR_PUBSUB_NAME = "ruleengine-pubsub";
    private const string DAPR_PUBSUB_WORKFLOWFINISHEDTOPIC = "WorkflowFinishedEvent";
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<WorkflowFinishedEventController> _logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkflowFinishedEventController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public WorkflowFinishedEventController(
    ILogger<WorkflowFinishedEventController> logger,
    IMediator mediator
    ) {
      _logger = logger;
      _mediator = mediator;
    }
    /// <summary>
    /// Tests the event handler.
    /// </summary>
    /// <param name="event">The event.</param>
    [HttpPost(DAPR_PUBSUB_WORKFLOWFINISHEDTOPIC)]
    [Topic(DAPR_PUBSUB_NAME, DAPR_PUBSUB_WORKFLOWFINISHEDTOPIC)]
    public async Task WorkflowFinishedEvent([FromBody] WorkflowFinishedEvent @event) {
      var validator = new WorkflowFinishedEventValidator(); //DI TODO
      var validationResult = validator.Validate(@event);
      if (!validationResult.IsValid) {
        _logger.LogError("Event: {EventName} is not valid", typeof(WorkflowFinishedEvent).Name);
        return;
      }
      await _mediator.Publish(@event);
    }
  }


}
