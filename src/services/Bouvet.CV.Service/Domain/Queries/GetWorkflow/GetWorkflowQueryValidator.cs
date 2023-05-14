using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentValidation;

namespace Bouvet.CV.Service.Domain.Queries.GetWorkflow {
  public class GetWorkflowQueryValidator : AbstractValidator<GetWorkflowQuery> {
    public GetWorkflowQueryValidator(IValidator<IStringField> stringValidator) {
      RuleFor(x => x.workflowName).SetValidator(stringValidator);
    }
  }
}
