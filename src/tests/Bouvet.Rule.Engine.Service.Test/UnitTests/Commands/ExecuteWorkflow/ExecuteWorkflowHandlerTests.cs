using Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow;
using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using FluentAssertions;
using FluentValidation;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using tests.Factories;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.Command.ExecuteWorkflow {
  public class ExecuteWorkflowHandlerTests {
    [Fact]
    public async Task ShouldBePossibleToHandleAValidExecuteWorkflowCommandAsync() {
      // Arrange
      var mediator = new Mock<IMediator>();
      var logger = new Mock<ILogger<ExecuteWorkflowHandler>>();
      var validator = new Mock<IValidator<IIdField<Guid>>>();
      var queue = new DefaultBackgroundTaskQueue(100);
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var command = new ExecuteWorkflowCommand(FieldsFactory.IdField(Guid.NewGuid()), ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input);
      var handler = new ExecuteWorkflowHandler(queue, logger.Object, validator.Object);
      // Act
      var result = await handler.Handle(command, new CancellationToken());
      // Assert
      result.Should().NotBeNull().And.BeOfType<OperationResult<ExecuteWorkflowDTO>>();
      result.Success.Should().Be(true);
      result.Value.WorkflowExecutionId.Value.Should().NotBeEmpty();
    }
  }
}
