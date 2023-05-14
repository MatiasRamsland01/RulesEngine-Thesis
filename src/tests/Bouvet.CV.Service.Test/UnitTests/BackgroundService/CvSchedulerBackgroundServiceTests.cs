using FluentAssertions;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using Dapr.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bouvet.CV.Service.Test.UnitTests.BackgroundService {
  public class CvSchedulerBackgroundServiceTests {
    [Theory]
    [InlineData(1)]
    public void ShouldBePossibleToCalculateCorrectlyTimeToWaitAccoordingTo1ExecutionPerDay(int dayToNestExecution) {
      var logger = new Mock<ILogger<CvSchedulerBackgroundService>>();
      var scope = new Mock<IServiceScopeFactory>();
      var service = new CvSchedulerBackgroundService(logger.Object, scope.Object);
      var result = service.TimeToWait();
      result.Should().BeLessThanOrEqualTo(TimeSpan.FromDays(dayToNestExecution)).And.BeGreaterThanOrEqualTo(TimeSpan.MinValue);
    }
    [Theory]
    [InlineData("test@bouvet.no", "test@bouvet.no", true)]
    [InlineData("test@bouvet.no", "1234@bouvet.no", false)]
    public void ShouldBePossibleToCheckIfCvNotHasBeenUpdated(string val1, string val2, bool expected) {
      var logger = new Mock<ILogger<CvSchedulerBackgroundService>>();
      var scope = new Mock<IServiceScopeFactory>();
      var dictionary = new Mock<IReadOnlyDictionary<string, IEnumerable<string>>>();
      var service = new CvSchedulerBackgroundService(logger.Object, scope.Object);
      service._daprClient = new Mock<DaprClient>().Object;
      var oldCv = new List<Response>() { new Response() { Email = val1 } };
      var newCv = new Response() { Email = val2 };

      var newCvResponse = new SwaggerResponse<Response>(200, dictionary.Object, newCv);

      var result = service.CVHasNotBeenUpdated(oldCv, newCvResponse);
      result.Should().Be(expected);
    }

  }
}
