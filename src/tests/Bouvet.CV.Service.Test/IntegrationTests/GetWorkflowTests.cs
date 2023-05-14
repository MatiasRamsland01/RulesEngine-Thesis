using AutoFixture;
using Bouvet.CV.Service.Infrastructure;
using fields.Factories;
using FluentAssertions;
using libraries.Contracts.Bouvet.CV.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Mime;


namespace Bouvet.CV.Service.IntegrationTests {

  public class GetWorkflowTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    public GetWorkflowTests(CustomWebApplicationFactory<Program> factory) {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
        AllowAutoRedirect = false
      });
    }

    [Fact]
    public async Task ShouldBePossibleToGetAWorkflow() {
      using (var scope = _factory.Services.CreateScope()) {
        var fixture = new Fixture();
        BRule rule = RuleEngineFactory.CreateRule(fixture.Create("RuleName"), fixture.Create("SuccessEvent"), fixture.Create("ErrorNullRuleViolated"), "input != null", "AND");
        var workflow = RuleEngineFactory.CreateWorkflow(new List<BRule>() { rule }, "TEST");
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<RuleengineContext>();
        db.Add(workflow);
        db.SaveChanges();
        var result = await _client.GetAsync($"api/getworkflow?workflowName={workflow.WorkflowName.Value}");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await result.Content.ReadAsStringAsync();
        var jsonSerializerOptions = new JsonSerializerOptions {
          PropertyNameCaseInsensitive = true
        };
        var operationResult = JsonSerializer.Deserialize<OperationResult<BWorkflow>>(content, jsonSerializerOptions)!;
        operationResult.Success.Should().BeTrue();
        operationResult.Errors.Should().BeNullOrEmpty();

      }
    }
    [Fact]
    public async Task ShouldNotBePossibleToGetAnInvalidWorkflow() {
      var workflowName = "NotExsistingWorkflow";
      var result = await _client.GetAsync($"api/getworkflow?workflowname={workflowName}");
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
  }
}
