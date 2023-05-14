using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.RulesEngine.BusinessRules {
  public class EmployeeMustCategorizeTheirTechnologiesTests {
    [Fact]
    public async void RuleShouldReturnTrueWhenEmployeeMustCategorizeTheirTechnologies() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");

      CvMustContainOnlyCategorizedTechnologies businessrule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, businessrule);


      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
    [Fact]
    public async void RuleShouldReturnFaliureWhenEmployeeMustCategorizeTheirTechnologies() {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      CvMustContainOnlyCategorizedTechnologies buisnessRule = new();

      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);

      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }
  }
}
