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
using Bouvet.Rule.Engine.Service.Domain.Commands;
using libraries.Contracts.Bouvet.Rule.Engine.Service;

namespace tests.RulesEngine.BusinessRules {
  public class CvMustHaveAProfilePictureTests {
    [Fact]
    public async void RuleShouldReturnSuccessWhenCvDoesNotContainAProfilePicture() {
      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");
      CvMustHaveProfilePicture buisnessRule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);
      //Assert
      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
    [Fact]
    public async void RuleShouldReturnFailureWhenCvDoesNotContainAProfilePicture() {
      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      CvMustHaveProfilePicture buisnessRule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, buisnessRule);
      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }

  }
}
