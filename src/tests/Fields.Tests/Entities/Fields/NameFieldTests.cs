using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Tests.Entities.Fields {
  /// <summary>
  /// Unit test class to evaluate expected behaviour in <see cref="NameField" /> class.
  /// </summary>
  public class NameFieldTests {
    /// <summary>
    /// The maximum value of a namefield
    /// </summary>
    public const int MAX = 100;
    /// <summary>
    /// The minimum value of a namefield
    /// </summary>
    public const int MIN = 2;
    /// <summary>
    /// The name validator
    /// </summary>
    private readonly IValidator<INameField> nameValidator;
    /// <summary>
    /// Initializes a new instance of the <see cref="NameFieldTests" /> class.
    /// </summary>
    public NameFieldTests() {
      nameValidator = new NameFieldValidator(Options.Create(new NameFieldConfigOption() { Min = 2, Max = 200 }));
    }

    /// <summary>
    /// Defines the test method ShouldBePossibleToSetAgeInValidTimeInterval.
    /// </summary>
    [Theory, AutoData]
    public void ShouldBePossibleToSetAValidName(string Name) {
      NameField firstname = new(nameValidator) { Name = Name };
      FluentValidation.Results.ValidationResult validationResult = firstname.DoValidate();
      validationResult.IsValid.Should().BeTrue();
    }
    [Theory, AutoData]
    public void ShouldBePossibleToSetAInvalidName(string name) {
      NameField firstname = new(nameValidator) { Name = name };
      FluentValidation.Results.ValidationResult validationResult = firstname.DoValidate();
      validationResult.IsValid.Should().BeFalse();
    }
  }
}
