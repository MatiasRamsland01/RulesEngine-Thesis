using Bouvet.Rule.Engine.Service.BackroundService;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.BackgroundService {
  public class BackgroundModelTests {

    [Fact]
    public void ShouldBePossibleToCreateBackgroundModel() {
      // Arrange & Act
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var model = new BackgroundModel(ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input, FieldsFactory.IdField(Guid.NewGuid()));
      // Assert
      model.Should().NotBeNull();
      model.Workflow.Should().NotBeNull().And.BeOfType<BWorkflow>();
      model.Input.Should().NotBeNull().And.BeOfType<BInputs>();
      model.id.Should().NotBeNull().And.BeOfType<IdField<Guid>>();

    }
  }
}
