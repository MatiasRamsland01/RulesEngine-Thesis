using libraries.Extentions.BRulesEngine.Rules.Core;
using fields.Entities.Base.Fields.Primitive;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service {
  public record ExecuteWorkflowCommandContractInformation {
    public BWorkflow Workflow { get; set; }
    public InputMessage Input { get; set; }
    public ExecuteWorkflowCommandContractInformation(BWorkflow workflow, InputMessage input) {
      Workflow = workflow;
      Input = input;
    }
  }

  public record InputMessage {
    public StringField URLToFetchData { get; set; }
    public dynamic Contract { get; set; }
    public InputMessage(StringField urlToFetchData, dynamic contract) {
      URLToFetchData = urlToFetchData;
      Contract = contract;
    }

  }
  public record ExecuteWorkflowCommandContractData {
    public BWorkflow Workflow { get; set; }
    public BInputs Input { get; set; }
    public ExecuteWorkflowCommandContractData(BWorkflow workflow, BInputs input) {
      Workflow = workflow;
      Input = input;
    }
  }
}
