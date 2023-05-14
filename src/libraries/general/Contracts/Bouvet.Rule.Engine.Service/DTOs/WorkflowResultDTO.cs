using fields.Entities.Base.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core;
namespace libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs {
  public record WorkflowResultDTO {
    public PipelineResult WorkflowResult { get; set; }
    public WorkflowResultDTO(PipelineResult workflowResult) {
      WorkflowResult = workflowResult;
    }
  }
}
