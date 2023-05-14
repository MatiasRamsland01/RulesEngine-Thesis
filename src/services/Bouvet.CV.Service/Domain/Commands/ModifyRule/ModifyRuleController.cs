using fields.Entities.Base.Fields;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Commands.ModifyRule {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class ModifyRuleController : ControllerBase {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ModifyRuleController> logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="AddWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public ModifyRuleController(
    ILogger<ModifyRuleController> logger,
    IMediator mediator) {
      this.logger = logger;
      _mediator = mediator;
    }
    [HttpPut("modifyrule")]
    public async Task<OperationResult<ModifyRuleCommand>> ModifyRule([FromBody] ModifyRuleCommandContract command) {
      var result = await _mediator.Send(new ModifyRuleCommand(command.Rule));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }


}
