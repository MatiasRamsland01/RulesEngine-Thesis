using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;

namespace Tests.Entities.Base.Fields {
  /// <summary>
  /// Class CharFieldTests.
  /// </summary>
  public class CharFieldTests {
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<ICharField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="CharFieldTests"/> class.
    /// </summary>
    public CharFieldTests() {
      _validator = new CharFieldValidator();
    }
    /// <summary>
    /// Defines the test method ShouldOnlyBeValidWithACharValue.
    /// </summary>
    /// <param name="input">The input.</param>
    [Theory, AutoData]
    public void ShouldOnlyBeValidWithACharValue(char input) {
      CharField testChar = new(_validator) { Value = input };
      testChar.DoValidate().IsValid.Should().BeTrue();
    }
  }
}
