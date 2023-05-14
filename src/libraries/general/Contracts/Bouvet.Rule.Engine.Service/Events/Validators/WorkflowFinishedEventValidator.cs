using FluentValidation;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service.Events.Validators {
  public class WorkflowFinishedEventValidator : AbstractValidator<WorkflowFinishedEvent> {
    public WorkflowFinishedEventValidator() {
      RuleFor(x => x.EventId).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
      RuleFor(x => x.CreationDate).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
      RuleFor(x => x.WorkflowId).NotNull().WithMessage($"{nameof(WorkflowFinishedEvent)} is null");
    }
  }
}
