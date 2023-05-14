using fields.Entities.Base.Fields.Custom;
using FluentValidation;

namespace Bouvet.Rule.Engine.Service.Domain.Queries {
  public class GetWorkflowResultQueryValidator : AbstractValidator<GetWorkflowResultQuery> {
    public GetWorkflowResultQueryValidator(IValidator<IIdField<Guid>> idFieldValidator) {
      RuleFor(x => x.workflowResultId).SetValidator(idFieldValidator);
    }

  }
}
