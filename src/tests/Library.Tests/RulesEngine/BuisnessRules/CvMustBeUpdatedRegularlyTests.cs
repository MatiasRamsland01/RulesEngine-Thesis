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
  public class CvMustBeUpdatedRegularlyTests {
    [Fact]
    public async void RuleShouldReturnSuccessWhenCvIsUpdatedRegularly() {
      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");
      var cv = RulesEngineTestFactory.CreateValidCv(testDataFilePath);
      cv.Owner_updated_at = DateTime.Now.AddDays(-1).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

      CvMustBeUpdatedRegularly buisnessRule = new(90);
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule, cv);

      //Act
      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }

    [Fact]
    public async void RuleShouldReturnFailureWhenCvIsNotUpdatedRegularly() {
      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      CvMustBeUpdatedRegularly buisnessRule = new(90);
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);
      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }
  }
}
