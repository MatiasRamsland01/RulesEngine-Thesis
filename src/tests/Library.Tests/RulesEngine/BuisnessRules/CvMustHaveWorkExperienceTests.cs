using System.Dynamic;
using System.Text.Json;
using fields.Factories;
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Converters;
using tests.Factories;

namespace tests.RulesEngine.BusinessRules {
  public class CvMustHaveWorkExperienceTests {
    [Fact]
    public async void RuleShouldReturnTrueWhenEmployeeHaveWorkExperience() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");
      CvMustHaveWorkExperience businessrule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, businessrule);

      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
    [Fact]
    public async void RuleShouldReturnFalseWhenEmployeeDoesNotHaveWorkExperience() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      CvMustHaveWorkExperience businessrule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, businessrule);
      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }
  }
}
