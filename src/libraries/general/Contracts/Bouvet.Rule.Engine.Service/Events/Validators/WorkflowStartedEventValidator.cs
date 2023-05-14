using FluentValidation;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service.Events.Validators {
  public class WorkflowStartedEventValidator : AbstractValidator<WorkflowStartedEvent> {
    public WorkflowStartedEventValidator() {
      RuleFor(x => x.EventId).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
      RuleFor(x => x.CreationDate).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
      RuleFor(x => x.WorkflowId).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
    }
  }
}
