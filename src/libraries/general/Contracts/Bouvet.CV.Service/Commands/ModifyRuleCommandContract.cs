using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service {
  public record ModifyRuleCommandContract {
    public BRule Rule { get; set; }
    public ModifyRuleCommandContract(BRule rule) {
      Rule = rule;
    }
  }
}
