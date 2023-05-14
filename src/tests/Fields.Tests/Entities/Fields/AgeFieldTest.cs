using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Tests.Entities.Fields {
  /// <summary>
  /// Class AgeFieldTest.
  /// </summary>
  public class AgeFieldTest {
    public const int MAX = 100;
    public const int MIN = 10;
    private IValidator<IAgeField> validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="AgeFieldTest"/> class.
    /// </summary>
    public AgeFieldTest() {
      IOptions<AgeFieldConfigOption> config = Options.Create<AgeFieldConfigOption>(new AgeFieldConfigOption() { Min = MIN, Max = MAX });
      validator = new AgeFieldValidator(config);
    }

    /// <summary>
    /// Defines the test method ShouldBePossibleToSetAgeInValidTimeInterval.
    /// </summary>
    /// <param name="age">The age.</param>
    [Theory, AutoData]
    public void ShouldBePossibleToSetAgeInValidTimeInterval([Range(MIN, MAX)] int age) {
      var ageField = new AgeField(validator) { Age = age };
      ageField.DoValidate().IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Defines the test method ShouldNotBePossibleToSetAgeHigherThanMax.
    /// </summary>
    /// <param name="age">The age.</param>
    [Theory, AutoData]
    public void ShouldNotBePossibleToSetAgeHigherThanMax([Range(MAX + 1, MAX + 100)] int age) {
      var ageField = new AgeField(validator) { Age = age };
      ageField.DoValidate().IsValid.Should().BeFalse();
    }

    /// <summary>
    /// Defines the test method ShouldNotBePossibleToSetAgeLowerThanMin.
    /// </summary>
    /// <param name="age">The age.</param>
    [Theory, AutoData]
    public void ShouldNotBePossibleToSetAgeLowerThanMin([Range(MIN - 100, MIN - 1)] int age) {
      var ageField = new AgeField(validator) { Age = age };
      ageField.DoValidate().IsValid.Should().BeFalse();
    }
  }
}
