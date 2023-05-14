using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentValidation;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Queries.GetWorkflow {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class GetWorkflowController : ControllerBase {
    private const string WORKFLOWNAME = "rulesEngine";
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetWorkflowController> logger;
    private readonly IValidator<IStringField> _stringValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public GetWorkflowController(ILogger<GetWorkflowController> logger, IMediator mediator, IValidator<IStringField> stringValidator) {
      this.logger = logger;
      _mediator = mediator;
      _stringValidator = stringValidator;
    }
    /// <summary>
    /// Executes the workflow.
    /// </summary>
    /// <returns>ActionResult&lt;PipelineResult&gt;.</returns>
    [HttpGet("getworkflow")]
    public async Task<OperationResult<WorkflowDTO>> GetWorkflow(string workflowName) {
      var result = await _mediator.Send(new GetWorkflowQuery(FieldsFactory.StringField(workflowName, _stringValidator)));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }
}
