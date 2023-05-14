using FluentAssertions;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using fields.Factories;
using Moq;
using MediatR;

namespace Bouvet.Notification.Servicw.Test.IntegrationTests {

  public class WorkflowFinishedEventTests {
    public WorkflowFinishedEventTests() {
    }

    [Fact]
    public async Task ShouldBePossibleToHandleWorkflowFinishedEvnet() {
      // Arrange
      var mediatr = new Mock<IMediator>();
      await using var application = new WebApplicationFactory<Program>().WithWebHostBuilder(b => b.ConfigureServices(services => {
        services.Remove(services.Single(d => d.ServiceType == typeof(IMediator)));
        services.AddSingleton<IMediator>(mediatr.Object);
      }));
      mediatr.Setup(m => m.Publish(It.IsAny<WorkflowFinishedEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
      using var client = application.CreateClient();
      var @event = new WorkflowFinishedEvent(FieldsFactory.IdField(Guid.NewGuid()), FieldsFactory.DateField(DateTime.Now.ToShortDateString()), FieldsFactory.IdField(Guid.NewGuid()));
      // Act
      var result = await client.PostAsync("api/WorkflowFinishedEvent", new StringContent(JsonConvert.SerializeObject(@event), Encoding.UTF8, "application/json"));
      // Assert
      mediatr.Verify(m => m.Publish(It.IsAny<WorkflowFinishedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
      result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
  }
}
