using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using FluentValidation;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.Rule.Engine.Service.Domain.Queries {
  /// <summary>
  /// Class ExecuteWorkflowController.
  /// Implements the <see cref="ControllerBase" />
  /// </summary>
  /// <seealso cref="ControllerBase" />
  [Route("api/v0/")]
  [ApiController]
  public class GetWorkflowResultController : ControllerBase {
    private readonly IMediator _mediator;

    private readonly ILogger<GetWorkflowResultController> logger;
    private readonly IValidator<IIdField<Guid>> _idValidator;
    public GetWorkflowResultController(ILogger<GetWorkflowResultController> logger, IMediator mediator, IValidator<IIdField<Guid>> idValidator) {
      this.logger = logger;
      _mediator = mediator;
      _idValidator = idValidator;
    }
    [HttpGet("getworkflowresult")]
    public async Task<OperationResult<WorkflowResultDTO>> GetWorkflowResult(Guid workflowExecutionId) {
      return await _mediator.Send(new GetWorkflowResultQuery(FieldsFactory.IdField(workflowExecutionId, _idValidator)));
    }
  }
}
