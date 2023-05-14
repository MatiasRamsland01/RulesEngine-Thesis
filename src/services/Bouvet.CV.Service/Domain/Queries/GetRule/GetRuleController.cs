using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentValidation;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Queries.GetRule {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class GetRuleController : ControllerBase {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetRuleController> logger;
    private readonly IValidator<IStringField> _stringValidator;
    /// <summary>
    /// Initializes a new instance of the <see cref="AddWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public GetRuleController(ILogger<GetRuleController> logger, IMediator mediator, IValidator<IStringField> stringValidator) {
      this.logger = logger;
      _mediator = mediator;
      _stringValidator = stringValidator;
    }
    /// <summary>
    /// Executes the workflow.
    /// </summary>
    /// <returns>ActionResult&lt;PipelineResult&gt;.</returns>
    [HttpGet("getrule")]
    public async Task<OperationResult<RuleDTO>> GetRule(string ruleName) {
      var result = await _mediator.Send(new GetRuleQuery(FieldsFactory.StringField(ruleName, _stringValidator)));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }
}
