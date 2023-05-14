using Bouvet.Notification.Service.Domain.WorkflowFinished;
using Dapr.Client;
using fields.Factories;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Bouvet.Notification.Servicw.Test.UnitTests {

  public class WorkflowFinishedEventHandlerTests {
    public WorkflowFinishedEventHandlerTests() {
    }

    [Fact]
    public async Task ShouldBePossibleToHandleWorkflowFinishedEvent() {
      // Arrange
      var daprClient = new Mock<DaprClient>();
      var logger = new Mock<ILogger<WorkflowFinishedEventHandler>>();
      var handler = new WorkflowFinishedEventHandler(logger.Object, daprClient.Object);
      var @event = new WorkflowFinishedEvent(FieldsFactory.IdField(Guid.NewGuid()), FieldsFactory.DateField(DateTime.Now.ToShortDateString()), FieldsFactory.IdField(Guid.NewGuid()));

      // Create the PipelineResult and serialize it only once
      var pipelineResult = new Mock<PipelineResult>();
      var dto = new WorkflowResultDTO(pipelineResult.Object);

      // Create the OperationResult using the serialized PipelineResult
      var result = OperationResult<WorkflowResultDTO>.CreateSuccess(dto);
      daprClient.Setup(m => m.InvokeMethodAsync<OperationResult<WorkflowResultDTO>>(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(result));

      // Act
      await handler.Handle(@event, CancellationToken.None);
      // Assert
      daprClient.Verify(m => m.InvokeMethodAsync<OperationResult<WorkflowResultDTO>>(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()), Times.Once);
    }
  }
}
