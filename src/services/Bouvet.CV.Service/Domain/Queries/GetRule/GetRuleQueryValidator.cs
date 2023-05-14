using fields.Entities.Base.Fields.Primitive;
using FluentValidation;

namespace Bouvet.CV.Service.Domain.Queries.GetRule {
  public class GetRuleQueryValidator : AbstractValidator<GetRuleQuery> {
    public GetRuleQueryValidator(IValidator<IStringField> stringFieldValidator) {
      RuleFor(x => x.ruleName).SetValidator(stringFieldValidator);
    }
  }
}
