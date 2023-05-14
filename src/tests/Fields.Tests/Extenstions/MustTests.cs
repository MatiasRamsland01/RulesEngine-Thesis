using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using fields.Extensions;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;
namespace Tests.Extenstions {
  /// <summary>
  /// Class MustTests.
  /// </summary>
  public class MustTests {
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IStringField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="MustTests"/> class.
    /// </summary>
    public MustTests() {
      _validator = new StringFieldValidator(Options.Create(new StringFieldConfigOption() { Max = 1000, Min = 1 }));
    }
    /// <summary>
    /// Defines the test method MustCompileTest.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("using System;", true)]
    [InlineData("using System", false)]
    [InlineData("This is a very not legal string in c#", false)]
    [InlineData("using System; var ThisIsALegalVariable = 33;", true)]
    public void MustCompileTest(string value, bool expected) {
      Must.Compile(value).Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustBeExpressionTest.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("2 * 2", true)]
    [InlineData("3 34 43 * 0wad", false)]
    [InlineData("(2 + 5) * 10", true)]
    public void MustBeExpressionTest(string value, bool expected) {
      Must.IsExpressionWithuotInputs(value).Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustBeJSON.
    /// </summary>
    /// <param name="json">The json.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""}", true)]
    [InlineData(@"{""name"":""John"",""age"":30,""city"":""New York""", false)]
    public void MustBeJSON(string json, bool expected) {
      bool result = Must.BeJSON(json);
      _ = result.Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustContainOnlyLetters.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("foo", true)]
    [InlineData("Foo", true)]
    [InlineData("Foo1", false)]
    [InlineData("Foo1Bar", false)]
    public void MustContainOnlyLetters(string value, bool expected) {
      bool result = Must.ContainsOnlyLetters(value);
      _ = result.Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustContainOnlyLettersAndNumbers.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("foo", true)]
    [InlineData("Foo", true)]
    [InlineData("Foo1", true)]
    [InlineData("Foo1Bar", true)]
    [InlineData("Foo1Bar!", false)]
    public void MustContainOnlyLettersAndNumbers(string value, bool expected) {
      bool result = Must.ContainsOnlyNumbersOrDigits(value);
      _ = result.Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustContainOnlyDigits.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("foo", false)]
    [InlineData("Foo", false)]
    [InlineData("134543", true)]
    [InlineData("134543.123", false)]
    public void MustContainOnlyDigits(string value, bool expected) {
      bool result = Must.ContainsOnlyDigits(value);
      _ = result.Should().Be(expected);
    }
    /// <summary>
    /// Defines the test method MustContainLegalCharacters.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    [Theory]
    [InlineData("foo", true)]
    [InlineData("<script>Alert(¨Malicious code¨)</script>", false)]
    [InlineData("John's bicycle crashed 500 feet down the road.", true)]
    public void MustContainLegalCharacters(string value, bool expected) {
      bool result = Must.ContainsOnlylegalCharacters(value);
      _ = result.Should().Be(expected);
    }
  }
}
