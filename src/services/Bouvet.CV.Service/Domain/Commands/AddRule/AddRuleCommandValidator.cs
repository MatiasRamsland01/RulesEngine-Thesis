using fields.Extensions;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;

namespace Bouvet.CV.Service.Domain.Commands.AddRule {
  public class AddRuleCommandValidator : AbstractValidator<AddRuleCommand> {
    public AddRuleCommandValidator(IValidator<IBRule> ruleValidator) {
      RuleFor(x => x.rule).SetValidator(ruleValidator);
    }
  }
}
