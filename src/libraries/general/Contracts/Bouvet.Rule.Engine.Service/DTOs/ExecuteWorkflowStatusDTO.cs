using fields.Entities.Base.Fields.Custom;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs {
  public record ExecuteWorkflowDTO {
    public IdField<Guid> WorkflowExecutionId { get; set; }
    public ExecuteWorkflowDTO(IdField<Guid> workflowExecutionId) {
      WorkflowExecutionId = workflowExecutionId;
    }
  }
}
