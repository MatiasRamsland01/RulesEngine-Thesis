using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using libraries.Extentions.DotNetCore.Communication.Dapr;
using MediatR;

public record WorkflowStartedEvent : INotification, IEvent<Guid> {
  public IdField<Guid> EventId { get; set; }
  public DateField CreationDate { get; set; }
  public IdField<Guid> WorkflowId { get; set; }
  public WorkflowStartedEvent(IdField<Guid> eventId, DateField creationDate, IdField<Guid> workflowId) {
    EventId = eventId;
    CreationDate = creationDate;
    WorkflowId = workflowId;
  }
}
