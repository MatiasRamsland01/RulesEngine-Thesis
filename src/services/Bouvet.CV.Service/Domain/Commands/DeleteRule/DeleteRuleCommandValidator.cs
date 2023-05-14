using fields.Entities.Base.Fields.Primitive;
using FluentValidation;

namespace Bouvet.CV.Service.Domain.Commands.DeleteRule {
  public class AddRuleCommandValidator : AbstractValidator<DeleteRuleCommand> {
    public AddRuleCommandValidator(IValidator<IStringField> stringFieldValidator) {
      RuleFor(x => x.ruleName).SetValidator(stringFieldValidator);
    }
  }
}
