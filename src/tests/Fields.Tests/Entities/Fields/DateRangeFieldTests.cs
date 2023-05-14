using AutoFixture.Xunit2;
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Tests.Entities {
  /// <summary>
  /// Class DateRangeFieldTest.
  /// </summary>
  public class DateRangeFieldTest {
    /// <summary>
    /// The minimum
    /// </summary>
    public const int MIN = 0;
    /// <summary>
    /// The maximum
    /// </summary>
    public const int MAX = 100;
    /// <summary>
    /// The date range field validator
    /// </summary>
    private readonly IValidator<IDateRangeField> DateRangeFieldValidator;
    /// <summary>
    /// The date field validator
    /// </summary>
    private readonly IValidator<IDateField> DateFieldValidator;
    /// <summary>
    /// The integer field validator
    /// </summary>
    private readonly IValidator<IIntegerField> IntegerFieldValidator;
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRangeFieldTest"/> class.
    /// </summary>
    public DateRangeFieldTest() {
      DateFieldValidator = new DateFieldValidator();
      DateRangeFieldValidator = new DateRangeFieldValidator();
      IOptions<IntegerFieldConfigOption> config = Options.Create<IntegerFieldConfigOption>(new IntegerFieldConfigOption() { Min = MIN, Max = MAX });
      IntegerFieldValidator = new IntegerFieldValidator(config);
    }

    /// <summary>
    /// Defines the test method ShouldBeTrueWhenGivenAValidTimeRange.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    [Theory, AutoData]
    public void ShouldBeTrueWhenGivenAValidTimeRange(DateTime dateTime) {
      DateField startDateField = new(DateFieldValidator) { DateTime = dateTime.ToString() };
      DateField endDateField = new(DateFieldValidator) { DateTime = dateTime.AddYears(2).ToString() };
      DateRangeField dateRange = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = startDateField, EndDateField = endDateField };
      dateRange.DoValidate().IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Defines the test method ShouldBeFalseWhenGivenAInValidTimeRange.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    [Theory, AutoData]
    public void ShouldBeFalseWhenGivenAInValidTimeRange(DateTime dateTime) {
      DateField startDateField = new(DateFieldValidator) { DateTime = dateTime.AddYears(1).ToString() };
      DateField endDateField = new(DateFieldValidator) { DateTime = dateTime.ToString() };
      DateRangeField dateRange = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = startDateField, EndDateField = endDateField };
      dateRange.DoValidate().IsValid.Should().BeFalse();
    }

    /// <summary>
    /// Defines the test method ShouldBeTrueIfDatesOverLap.
    /// </summary>
    /// <param name="date">The date.</param>
    [Theory, AutoData]
    public void ShouldBeTrueIfDatesOverLap(DateTime date) {
      DateField dateFieldOneStart = new(DateFieldValidator) { DateTime = date.ToString() };
      DateField dateFieldOneStop = new(DateFieldValidator) { DateTime = date.AddYears(5).ToString() };
      DateField dateFieldTwoStart = new(DateFieldValidator) { DateTime = date.AddYears(1).ToString() };
      DateField dateFieldTwoStop = new(DateFieldValidator) { DateTime = date.AddYears(3).ToString() };
      DateRangeField dateRangeOne = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldOneStart, EndDateField = dateFieldOneStop };
      DateRangeField dateRangeTwo = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldTwoStart, EndDateField = dateFieldTwoStop };
      bool doOverlap = dateRangeOne.DoOverLapWithOtherDateRange(dateRangeTwo);
      doOverlap.Should().BeTrue();
    }

    /// <summary>
    /// Defines the test method ShouldBeFalseIfDatesDoesNotOverLap.
    /// </summary>
    /// <param name="date">The date.</param>
    [Theory, AutoData]
    public void ShouldBeFalseIfDatesDoesNotOverLap(DateTime date) {
      DateField dateFieldOneStart = new(DateFieldValidator) { DateTime = date.ToString() };
      DateField dateFieldOneStop = new(DateFieldValidator) { DateTime = date.AddYears(5).ToString() };
      DateField dateFieldTwoStart = new(DateFieldValidator) { DateTime = date.AddYears(10).ToString() };
      DateField dateFieldTwoStop = new(DateFieldValidator) { DateTime = date.AddYears(30).ToString() };
      DateRangeField dateRangeOne = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldOneStart, EndDateField = dateFieldOneStop };
      DateRangeField dateRangeTwo = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldTwoStart, EndDateField = dateFieldTwoStop };
      bool doOverlap = dateRangeOne.DoOverLapWithOtherDateRange(dateRangeTwo);
      doOverlap.Should().BeFalse();
    }

    /// <summary>
    /// Defines the test method CorrectNumberOfOverlappingMonths.
    /// </summary>
    [Fact]
    public void CorrectNumberOfOverlappingMonths() {
      var date = new DateTime();
      DateField dateFieldOneStart = new(DateFieldValidator) { DateTime = date.ToString() };
      DateField dateFieldOneStop = new(DateFieldValidator) { DateTime = date.AddYears(2).ToString() };
      DateField dateFieldTwoStart = new(DateFieldValidator) { DateTime = date.AddYears(1).ToString() };
      DateField dateFieldTwoStop = new(DateFieldValidator) { DateTime = date.AddYears(3).AddDays(3).ToString() };
      DateRangeField dateRangeOne = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldOneStart, EndDateField = dateFieldOneStop };
      DateRangeField dateRangeTwo = new(DateRangeFieldValidator, DateFieldValidator, IntegerFieldValidator) { StartDateField = dateFieldTwoStart, EndDateField = dateFieldTwoStop };
      int numberOfOverlappingMonths = dateRangeOne.NumberOfOverlappingMonths(dateRangeTwo).Value;
      numberOfOverlappingMonths.Should().Be(11);
    }

  }
}
