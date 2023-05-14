using fields.Factories;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;

namespace tests.Factories {
  public class BouvetRuleEngineServiceTestFactory {
    public static ExecuteWorkflowCommandContractData CreateValidExecuteWorkflowCommandContract() {
      var rule = new CvMustNotBeNull();
      return new ExecuteWorkflowCommandContractData(RuleEngineFactory.CreateWorkflow(new List<BRule> { rule.Rule }, "ruleengine"), new BInputs(new { name = "dwa" }));
    }
  }
}
