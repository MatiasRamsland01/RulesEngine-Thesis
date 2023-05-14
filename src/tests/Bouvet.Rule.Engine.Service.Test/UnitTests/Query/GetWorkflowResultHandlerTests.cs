using Bouvet.Rule.Engine.Service.Domain.Queries;
using Dapr.Client;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using Microsoft.Extensions.Logging;
using Moq;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.Queries.GetWorkflowResult {
  public class GetWorkflowResultHandlerTests {
    private const string DAPR_STATESTORE_WORKFLOW = "ruleengine-statestore-result";


    [Fact]
    public async void ShouldBePossibleToHandleGetWorkflowResultQuery() {
      // Arrange & Act
      var query = new GetWorkflowResultQuery(FieldsFactory.IdField(Guid.NewGuid()));
      var logger = new Mock<ILogger<GetWorkflowResultHandler>>();
      var daprCLient = new Mock<DaprClient>();
      var pipelineResult = new Mock<PipelineResult>();
      daprCLient.Setup(x => x.GetStateAsync<PipelineResult>(DAPR_STATESTORE_WORKFLOW, query.workflowResultId.Value.ToString(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(pipelineResult.Object);
      var handler = new GetWorkflowResultHandler(logger.Object, daprCLient.Object);
      var result = await handler.Handle(query, new CancellationToken());
      // Assert
      result.Should().NotBeNull().And.BeOfType<OperationResult<WorkflowResultDTO>>();
    }
  }
}
