using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using libraries.Extentions.DotNetCore.Communication.Dapr;

public record CVUpdatedEvent : IEvent<Guid> {
  public IdField<Guid> EventId { get; set; }
  public DateField CreationDate { get; set; }
  public StringField Email { get; set; }
  public CVUpdatedEvent(IdField<Guid> eventId, DateField creationDate, StringField email) {
    EventId = eventId;
    CreationDate = creationDate;
    Email = email;
  }
}
