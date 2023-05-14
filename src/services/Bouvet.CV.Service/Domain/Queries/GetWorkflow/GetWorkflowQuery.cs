using fields.Entities.Base.Fields.Primitive;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Queries.GetWorkflow {
  public record GetWorkflowQuery(StringField workflowName) : IRequest<OperationResult<WorkflowDTO>>;
}
