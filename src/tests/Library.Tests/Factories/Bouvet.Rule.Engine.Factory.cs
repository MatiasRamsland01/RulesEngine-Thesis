using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using fields.Factories;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace tests.Factories {
  public class BouvetRuleEngineServiceTestFactory {
    public static ExecuteWorkflowCommandContractInformation CreateValidExecuteWorkflowCommandContract() {
      var rule = new CvMustNotBeNull();
      return new ExecuteWorkflowCommandContractInformation(RuleEngineFactory.CreateWorkflow(new List<BRule> { rule.Rule }, "ruleengine"), new InputMessage(FieldsFactory.StringField("Some url"), new { test = "aedwad" }));
    }
    public async static Task<PipelineResult> ExecuteRuleEngine(string testDataFilePath, IBouvetRule rule, Response cv = null!) {
      if (cv == null) {
        cv = RulesEngineTestFactory.CreateValidCv(testDataFilePath);
      }
      var input = new BInputs(cv);
      BRuleEngine engine = RulesEngineTestFactory.CreateBRuleEngine();
      BWorkflow workflow = RulesEngineTestFactory.CreateEmptyWorkflow();
      workflow.AddRule(rule.Rule);
      workflow.WorkflowName = FieldsFactory.StringField("Test RuleShouldReturnFailureWhenCvDoesNotContainAProfilePicture");
      var loggerMock = new Mock<ILogger>();
      var contract = new ExecuteWorkflowCommandContractData(workflow, input);
      var serializedContract = System.Text.Json.JsonSerializer.Serialize(contract);
      var inputProccessed = JsonConvert.DeserializeObject<ExecuteWorkflowCommandContractData>(serializedContract, new ExpandoObjectConverter());
      return await engine.ExecuteWorkflowAsync(inputProccessed!.Workflow, inputProccessed.Input, loggerMock.Object);
    }
    public static BInputs CreateInput(string cv) {
      return new BInputs(RulesEngineTestFactory.CreateValidCv(cv));
    }
    public static BInputs CreateInput(Response cv) {
      return new BInputs(cv);
    }
  }
}
