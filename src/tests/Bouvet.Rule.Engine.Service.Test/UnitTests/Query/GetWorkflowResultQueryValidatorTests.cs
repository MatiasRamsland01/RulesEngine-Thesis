using Bouvet.Rule.Engine.Service.Domain.Queries;
using fields.Factories;
using FluentAssertions;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core;
using Moq;
using fields.Entities.Base.Fields.Custom;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.Queries.GetWorkflowResult {
  public class GetWorkflowResultQueryValidatorTests {
    private const string DAPR_STATESTORE_WORKFLOW = "ruleengine-statestore-result";

    [Fact]
    public void ShouldValidateAndReturnTrueWhenValidatingAValidGetWorkflowResultQuery() {
      // Arrange
      var query = new GetWorkflowResultQuery(FieldsFactory.IdField(Guid.NewGuid()));
      var cancellationToken = new CancellationToken();
      var idValidator = new Mock<IValidator<IIdField<Guid>>>();
      var pipelineResult = new Mock<PipelineResult>();
      var validator = new GetWorkflowResultQueryValidator(idValidator.Object);
      // Act
      var result = validator.ValidateAsync(query, options => options.IncludeAllRuleSets(), cancellationToken).Result;
      // Assert
      result.IsValid.Should().Be(true);
    }
  }
}
