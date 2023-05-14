using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using FluentValidation;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Queries.GetCV {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class GetCVController : ControllerBase {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetCVController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<IEmailField> _emailValidator;
    public GetCVController(ILogger<GetCVController> logger, IMediator mediator, IValidator<IEmailField> emailValidator) {
      _logger = logger;
      _mediator = mediator;
      _emailValidator = emailValidator;
    }
    /// <summary>
    /// Executes the workflow.
    /// </summary>
    /// <returns>ActionResult&lt;PipelineResult&gt;.</returns>
    //TODO
    [HttpGet("getcv")]
    public async Task<OperationResult<CVDTO>> GetCV([FromQuery] string email) {
      var result = await _mediator.Send(new GetCVQuery(FieldsFactory.EmailField(email, _emailValidator)));
      return result;
    }
  }
}
