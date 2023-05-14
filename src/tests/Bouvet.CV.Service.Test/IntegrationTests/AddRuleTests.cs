using Bouvet.CV.Service.Infrastructure;
using FluentAssertions;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoFixture;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using System.Text.Json;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace Bouvet.CV.Service.IntegrationTests {

  public class AddRuleTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    public AddRuleTests(CustomWebApplicationFactory<Program> factory) {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
        AllowAutoRedirect = false
      });
    }

    [Fact]
    public async Task ShouldBePossibleToAddARule() {
      var fixture = new Fixture();
      var rule = RuleEngineFactory.CreateRule(fixture.Create("RuleName"), fixture.Create("SuccessEvent"), fixture.Create("ErrorNullRuleViolated"), "input != null", "AND");
      var contract = new AddRuleCommandContract(rule);
      var result = await _client.PostAsync("api/addrule", new StringContent(JsonSerializer.Serialize(contract), Encoding.UTF8, "application/json"));
      // Arrange
      using (var scope = _factory.Services.CreateScope()) {
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<RuleengineContext>();
        db.Rules.Where(r => r.Id == rule.Id).Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
      }
    }
    [Fact]
    public async Task ShouldNotBePossibleToAddAnInvalidRule() {
      var fixture = new Fixture();
      var rule = RuleEngineFactory.CreateRule(fixture.Create("RuleName"), fixture.Create("SuccessEvent"), fixture.Create("ErrorNullRuleViolated"), "input != null", "AND");
      rule.Data.RuleName.Value = "#¤%&/()";
      var contract = new AddRuleCommandContract(rule);
      var result = await _client.PostAsync("api/addrule", new StringContent(JsonSerializer.Serialize(contract), Encoding.UTF8, "application/json"));
      // Arrange
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      var content = await result.Content.ReadAsStringAsync();
      var operationResult = JsonSerializer.Deserialize<OperationResult<BRule>>(content);
      operationResult!.Success.Should().BeFalse();
      operationResult.Errors.Should().NotBeEmpty();
    }
  }
}
