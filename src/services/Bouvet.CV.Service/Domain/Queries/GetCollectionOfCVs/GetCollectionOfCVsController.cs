
using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using FluentValidation;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.CV.Service.Domain.Queries.GetCollectionOfCVs {
  [Route("api/v0/")]
  [ApiController]
  public class GetCollectionOfCVsController : ControllerBase {
    private readonly ILogger<GetCollectionOfCVsController> _logger;
    private readonly IMediator _mediator;
    private readonly IValidator<IEmailField> _emailValidator;
    public GetCollectionOfCVsController(ILogger<GetCollectionOfCVsController> logger, IMediator mediator, IValidator<IEmailField> emailValidator) {
      _logger = logger;
      _mediator = mediator;
      _emailValidator = emailValidator;
    }
    [HttpGet("getcollectionofcvs")]
    public async Task<OperationResult<CollectionOfCVsDTO>> GetCollectionOfCVs([FromQuery] string email) {
      var result = await _mediator.Send(new GetCollectionOfCVsQuery(FieldsFactory.EmailField(email, _emailValidator)));
      return result;
    }
  }
}
