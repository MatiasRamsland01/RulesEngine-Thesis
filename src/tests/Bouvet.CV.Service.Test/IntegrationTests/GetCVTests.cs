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
using fields.Factories;

namespace Bouvet.CV.Service.IntegrationTests {

  public class GetCVTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    public GetCVTests(CustomWebApplicationFactory<Program> factory) {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
        AllowAutoRedirect = false
      });
    }

    [Fact]
    public async Task ShouldBePossibleToGetACV() {
      var cvId = "test@test.com";
      var result = await _client.GetAsync($"api/getcv?email={cvId}");
      result.Should().NotBeNull();
    }
  }
}
