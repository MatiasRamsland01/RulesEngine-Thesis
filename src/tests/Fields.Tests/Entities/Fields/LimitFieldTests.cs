using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Tests.Entities.Base.Fields {
  /// <summary>
  /// Class LimitFieldTests.
  /// </summary>
  public class LimitFieldTests {
    /// <summary>
    /// The maximum limit
    /// </summary>
    public const int MAXLimit = 100;
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<ILimitField> Validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="LimitFieldTests"/> class.
    /// </summary>
    public LimitFieldTests() {
      IOptions<LimitConfigOption> limitFieldConfig = Options.Create<LimitConfigOption>(new LimitConfigOption() { Value = MAXLimit });
      Validator = new LimitFieldValidator(limitFieldConfig);
    }

    /// <summary>
    /// Defines the test method ShouldBeTrueWhenValueIsWithinLimit.
    /// </summary>
    /// <param name="value">The value.</param>
    [Theory, AutoData]
    public void ShouldBeTrueWhenValueIsWithinLimit([Range(0, MAXLimit)] int value) {
      LimitField limitFieldSut = new(Validator) { Limit = value };
      limitFieldSut.DoValidate().IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Defines the test method ShouldBeFalseWhenValueIsOutsideLimit.
    /// </summary>
    /// <param name="value">The value.</param>
    [Theory, AutoData]
    public void ShouldBeFalseWhenValueIsOutsideLimit([Range(MAXLimit + 1, MAXLimit + 100)] int value) {
      LimitField limitFieldSut = new(Validator) { Limit = value };
      limitFieldSut.DoValidate().IsValid.Should().BeFalse();
    }
  }
}
