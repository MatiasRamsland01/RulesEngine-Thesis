using Bouvet.Rule.Engine.Service.Domain.Queries;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentAssertions;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.Queries.GetWorkflowResult {
  public class GetWorkflowResultQueryTests {

    [Fact]
    public void ShouldBePossibleToCreateGetWorkflowResultQuery() {
      // Arrange & Act
      var query = new GetWorkflowResultQuery(FieldsFactory.IdField(Guid.NewGuid()));
      // Assert
      query.Should().NotBeNull().And.BeOfType<GetWorkflowResultQuery>();
      query.workflowResultId.Should().NotBeNull().And.BeOfType<IdField<Guid>>();
    }
  }
}
