using fields.Entities.Base.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.Communication.Dapr;
using MediatR;
namespace libraries.Contracts.Bouvet.CV.Service.DTOs {
  public record WorkflowDTO {
    public BWorkflow Workflow { get; set; }
    public WorkflowDTO(BWorkflow workflow) {
      Workflow = workflow;
    }
  }
}
