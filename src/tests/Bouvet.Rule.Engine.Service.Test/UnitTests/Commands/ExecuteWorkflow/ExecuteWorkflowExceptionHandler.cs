using Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow;
using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using tests.Factories;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.ExecuteWorkflow {
  public class ExecuteWorkflowExceptionHandlerTests {

    [Fact]
    public async Task ShouldBePossibleToHandleAExceptionInExecuteWorkflowHandlerAsync() {
      var mediator = new Mock<IMediator>();
      var logger = new Mock<ILogger<ExecuteWorkflowHandler>>();
      var validator = new Mock<IValidator<IIdField<Guid>>>();
      var queue = new DefaultBackgroundTaskQueue(100);
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var command = new ExecuteWorkflowCommand(FieldsFactory.IdField(Guid.NewGuid()), ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input);
      var handler = new ExecuteWorkflowHandler(queue, logger.Object, validator.Object);
      Func<Task> func = async () => await handler.Handle(command, new CancellationToken());
      await func.Should().NotThrowAsync();
    }
  }
}
