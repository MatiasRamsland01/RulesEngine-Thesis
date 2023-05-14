using System.Text.Json;
using fields.Factories;
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using Microsoft.Extensions.Logging;
using Moq;
using tests.Factories;

namespace tests.RulesEngine.BusinessRules {
  public class EmailMustBeValidTests {
    [Fact]
    public async void RuleShouldReturnSuccessWhenEmailIsValid() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");
      EmailMustBeValid buisnessRule = new EmailMustBeValid();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);

      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
    [Fact]
    public async void RuleShouldReturnFailureWhenEmailIsInvalid() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      EmailMustBeValid buisnessRule = new EmailMustBeValid();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);
      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }
  }
}
