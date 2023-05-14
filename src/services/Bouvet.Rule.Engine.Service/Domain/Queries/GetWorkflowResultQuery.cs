using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using fields.Entities.Base.Fields.Custom;

namespace Bouvet.Rule.Engine.Service.Domain.Queries {
  public record GetWorkflowResultQuery(IdField<Guid> workflowResultId) : IRequest<OperationResult<WorkflowResultDTO>>;
}
