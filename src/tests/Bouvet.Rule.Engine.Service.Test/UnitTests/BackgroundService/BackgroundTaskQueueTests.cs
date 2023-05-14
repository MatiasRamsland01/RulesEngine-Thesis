using Bouvet.Rule.Engine.Service.BackroundService;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using tests.Factories;

namespace tests.services.Bouvet.Rule.Engine.Service.UnitTests.BackgroundService {
  public class BackgroundTaskQueueTests {

    [Fact]
    public void ShouldBePossibleToCreateABackgroundTaskQueue() {
      // Arrange & Act
      var queue = new DefaultBackgroundTaskQueue(100);
      // Assert
      queue.Should().NotBeNull();
    }
    [Fact]
    public async Task ShouldBePossibleToQueueABackgroundTaskQueueAsync() {
      // Arrange
      var queue = new DefaultBackgroundTaskQueue(100);
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var model = new BackgroundModel(ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input, FieldsFactory.IdField(Guid.NewGuid()));
      // Act
      Func<Task> func = async () => await queue.QueueBackgroundWorkItemAsync(model);
      // Assert
      await func.Should().NotThrowAsync();
    }
    [Fact]
    public async Task ShouldNotBePossibleToQueueAInvalidBackgroundTaskQueueAsync() {
      // Arrange
      var queue = new DefaultBackgroundTaskQueue(100);
      // Act
      Func<Task> func = async () => await queue.QueueBackgroundWorkItemAsync(default!);
      // Assert
      await func.Should().ThrowAsync<ArgumentNullException>();
    }
    [Fact]
    public async Task ShouldBePossibleToDequeueABackgroundTaskQueueAsync() {
      // Arrange
      var queue = new DefaultBackgroundTaskQueue(100);
      var ExecuteWorkflowCommandContract = BouvetRuleEngineServiceTestFactory.CreateValidExecuteWorkflowCommandContract();
      var model = new BackgroundModel(ExecuteWorkflowCommandContract.Workflow, ExecuteWorkflowCommandContract.Input, FieldsFactory.IdField(Guid.NewGuid()));
      await queue.QueueBackgroundWorkItemAsync(model);
      // Act
      Func<Task> func = async () => await queue.DequeueAsync(new CancellationToken());
      // Assert
      await func.Should().NotThrowAsync();
    }
    [Fact]
    public async Task ShouldNotBePossibleToDequeAnEmptyBackgroundTaskQueueAsync() {
      // Arrange
      var queue = new DefaultBackgroundTaskQueue(100);
      // Act
      Func<Task> func = async () => await queue.QueueBackgroundWorkItemAsync(default!);
      // Assert
      await func.Should().ThrowAsync<Exception>();
    }

  }
}
