using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Commands.AddRule {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class AddRuleController : ControllerBase {
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<AddRuleController> logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="AddWorkflowController" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public AddRuleController(
    ILogger<AddRuleController> logger,
    IMediator mediator) {
      this.logger = logger;
      _mediator = mediator;
    }
    [HttpPost("addrule")]
    public async Task<OperationResult<AddRuleCommand>> AddRule([FromBody] AddRuleCommandContract contract) {
      //var rule = new CvMustHaveProfilePicture();
      var result = await _mediator.Send(new AddRuleCommand(contract.Rule));
      Response.StatusCode = result.HttpStatusCode;
      return result;
    }
  }
}
