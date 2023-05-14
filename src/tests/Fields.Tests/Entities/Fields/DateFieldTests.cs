using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;

namespace Tests.Entities.Fields {
  /// <summary>
  /// Class DateFieldTests.
  /// </summary>
  public class DateFieldTests {
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IDateField> _Validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="DateFieldTests"/> class.
    /// </summary>
    public DateFieldTests() {
      _Validator = new DateFieldValidator();
    }

    /// <summary>
    /// Defines the test method TestForCreatingAValidDateFieldShouldReturnTrue.
    /// </summary>
    /// <param name="date">The date.</param>
    [Theory, AutoData]
    public void TestForCreatingAValidDateFieldShouldReturnTrue(DateTime date) {
      var dateField = new DateField(_Validator) { DateTime = date.ToString() };
      dateField.DoValidate().IsValid.Should().Be(true);
    }

    /// <summary>
    /// Defines the test method TestForCreatingAValidDateFieldShouldReturnFalse.
    /// </summary>
    [Fact]
    public void TestForCreatingAValidDateFieldShouldReturnFalse() {
      string date_string_designed_to_fail = "2023.31.31";
      var dateField = new DateField(_Validator) { DateTime = date_string_designed_to_fail };
      dateField.DoValidate().IsValid.Should().Be(false);
    }
  }
}
