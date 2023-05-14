using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service {
  public record AddRuleCommandContract {
    public BRule Rule { get; set; }
    public AddRuleCommandContract(BRule rule) {
      Rule = rule;
    }
  }
}
