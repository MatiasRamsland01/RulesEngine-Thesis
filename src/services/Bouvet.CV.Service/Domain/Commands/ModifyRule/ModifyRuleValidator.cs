using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;

namespace Bouvet.CV.Service.Domain.Commands.ModifyRule {
  public class ModifyRuleCommandValidator : AbstractValidator<ModifyRuleCommand> {
    public ModifyRuleCommandValidator(IValidator<IBRule> ruleValidator) {
      RuleFor(x => x.rule).SetValidator(ruleValidator);
    }
  }
}
