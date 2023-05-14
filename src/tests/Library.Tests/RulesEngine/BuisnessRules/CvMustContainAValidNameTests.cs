using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.RulesEngine.BusinessRules {
  public class EmployeeNameMustBeValidTests {
    [Theory]
    [InlineData("Sophia Garcia")]
    [InlineData("Oliver Johnson Jr.")]
    [InlineData("Maria Martinez de la Cruz")]
    [InlineData("Johan Björk")]
    [InlineData("Hannah O'Reilly")]
    [InlineData("Anne-Sophie Dupont")]
    [InlineData("Ingrid Johansson-Lindström")]
    [InlineData("Cécile Dufour")]
    [InlineData("Alexander Nielsen-Hansen")]
    [InlineData("Isabella d'Angelo")]
    [InlineData("Luis Álvarez")]
    [InlineData("Elina Rantanen")]
    [InlineData("Téo Leclerc")]
    [InlineData("Alexandra Aa. Jansson")]
    [InlineData("Marius B. Jensen-Kristensen")]
    [InlineData("Åse Sagebakken")]
    [InlineData("Øystein")]
    [InlineData("Ægeir")]
    [InlineData("Aa Bb")]
    public async void RuleShouldReturnTrueWhenEmployeeNameIsValid(string nameInput) {
      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvGoodData.json");
      var cv = RulesEngineTestFactory.CreateValidCv(testDataFilePath);
      cv.Navn = nameInput;
      cv.Name = nameInput;
      CvMustContainAValidName businessrule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, businessrule, cv);
      //Assert
      result.errorMsg.Should().BeEmpty();
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
    [Theory]
    [InlineData("John123-Smith")]
    [InlineData("Mary-Sue-Johnson1")]
    [InlineData("Maxime1-Roy")]
    [InlineData("Alice-Martinez@")]
    [InlineData("Juan-Pablo-Lopez!")]
    [InlineData("123 456")]
    [InlineData("John#Doe")]
    [InlineData("Michael@Smith")]
    [InlineData("Anna2Olsen")]
    [InlineData("Mary_White")]
    [InlineData("Nina+Lindgren")]
    [InlineData("Lars|Jensen")]
    [InlineData("Eva=Peterson")]
    [InlineData("Ingrid^Anderson")]
    [InlineData("Erik&Olsson")]

    public async void RuleShouldReturnFalseWhenEmployeeNameIsInvalid(string nameInput) {

      //Arrange
      string basePath = AppDomain.CurrentDomain.BaseDirectory;
      string testDataFilePath = Path.Combine(basePath, "TestData", "ShortTestCvBadData.json");
      var cv = RulesEngineTestFactory.CreateValidCv(testDataFilePath);
      cv.Navn = nameInput;
      cv.Name = nameInput;
      CvMustContainAValidName businessrule = new();
      PipelineResult result = await BouvetRuleEngineServiceTestFactory.ExecuteRuleEngine(testDataFilePath, businessrule, cv);
      //Assert
      result.Should().NotBeNull();
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeFalse();
    }
  }
}
