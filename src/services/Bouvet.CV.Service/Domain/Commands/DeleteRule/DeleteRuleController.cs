using fields.Entities.Base.Fields;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Commands.DeleteRule {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class DeleteRuleController : ControllerBase {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<DeleteRuleController> logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="AddWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public DeleteRuleController(
    ILogger<DeleteRuleController> logger,
    IMediator mediator) {
      this.logger = logger;
      _mediator = mediator;
    }
    [HttpDelete("deleterule")]
    public async Task<OperationResult<DeleteRuleCommand>> DeleteRule([FromBody] DeleteRuleCommandContract command) {
      var result = await _mediator.Send(new DeleteRuleCommand(command.RuleName));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }


}
