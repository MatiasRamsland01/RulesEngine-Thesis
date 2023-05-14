using AutoFixture;
using Bouvet.CV.Service.Infrastructure;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;


namespace Bouvet.CV.Service.IntegrationTests {

  public class GetRuleTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    public GetRuleTests(CustomWebApplicationFactory<Program> factory) {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
        AllowAutoRedirect = false
      });
    }

    [Fact]
    public async Task ShouldBePossibleToGetARule() {
      using (var scope = _factory.Services.CreateScope()) {
        var fixture = new Fixture();
        BRule rule = RuleEngineFactory.CreateRule(("RuleName"), fixture.Create("SuccessEvent"), fixture.Create("ErrorNullRuleViolated"), "input != null", "AND");
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<RuleengineContext>();
        await db.AddAsync(rule);
        await db.SaveChangesAsync();
        var result = await _client.GetAsync($"api/getrule?ruleName={rule.Data.RuleName.Value}");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await result.Content.ReadAsStringAsync();

        // Add JsonSerializerOptions with case-insensitive property name matching
        var jsonSerializerOptions = new JsonSerializerOptions {
          PropertyNameCaseInsensitive = true
        };

        // Pass JsonSerializerOptions to Deserialize method
        var operationResult = JsonSerializer.Deserialize<OperationResult<RuleDTO>>(content, jsonSerializerOptions);
        operationResult.Should().NotBeNull();
        operationResult!.Success.Should().BeTrue();
        operationResult.Value.Rule.Data.RuleName.Value.Should().Be(rule.Data.RuleName.Value);
      }
    }
    [Fact]
    public async Task ShouldNotBePossibleToGetARuleThatDoesNotExsist() {
      var ruleName = "NotExsistingRuleName";
      var result = await _client.GetAsync($"api/getrule?ruleName={ruleName}");
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
  }
}
