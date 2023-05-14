using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using libraries.Extentions.DotNetCore.Communication.Dapr;
using MediatR;
namespace libraries.Contracts.Bouvet.Rule.Engine.Service;
public record WorkflowFinishedEvent : INotification, IEvent<Guid> {
  public IdField<Guid> EventId { get; set; }
  public DateField CreationDate { get; set; }
  public IdField<Guid> WorkflowId { get; set; }
  public WorkflowFinishedEvent(IdField<Guid> eventId, DateField creationDate, IdField<Guid> workflowId) {
    EventId = eventId;
    CreationDate = creationDate;
    WorkflowId = workflowId;
  }
}
