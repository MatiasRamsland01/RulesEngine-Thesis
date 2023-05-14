using Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.Command.ExecuteWorkflow {
  public class ExecuteWorkflowCommandTests {

    [Fact]
    public void ShouldBePossibleToCreateExecuteWorkflowCommand() {
      // Arrange & Act
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var command = new ExecuteWorkflowCommand(FieldsFactory.IdField(Guid.NewGuid()), ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input);
      // Assert
      command.Should().NotBeNull();
      command.workflow.Should().NotBeNull().And.BeOfType<BWorkflow>();
      command.input.Should().NotBeNull().And.BeOfType<BInputs>();
      command.workflowId.Should().NotBeNull().And.BeOfType<IdField<Guid>>();

    }
  }
}
