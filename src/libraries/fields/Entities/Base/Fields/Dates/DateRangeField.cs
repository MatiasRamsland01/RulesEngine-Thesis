using fields.Entities.Base.Fields.Base;
using fields.Entities.Base.Fields.Primitive;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Dates {
  public interface IDateRangeField : IField, IAllowedType {
    /// <summary>
    /// Gets or sets the start date field.
    /// </summary>
    /// <value>The start date field.</value>
    DateField StartDateField { get; set; }
    /// <summary>
    /// Gets or sets the end date field.
    /// </summary>
    /// <value>The end date field.</value>
    DateField EndDateField { get; set; }
  }
  /// <summary>
  /// Class DateRangeField.
  /// Implements the <see cref="Fields.Entities.Base.Fields.IDateRangeField" />
  /// </summary>
  /// <seealso cref="Fields.Entities.Base.Fields.IDateRangeField" />
  public partial class DateRangeField : IDateRangeField {
    /// <summary>
    /// The initial value
    /// </summary>
    private const string _initialValue = "";
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IDateRangeField> _validator;
    /// <summary>
    /// The integer field validator
    /// </summary>
    private readonly IValidator<IIntegerField> _integerFieldValidator;
    /// <summary>
    /// The date field validator
    /// </summary>
    private readonly IValidator<IDateField> _dateFieldValidator;
    /// <summary>
    /// Gets or sets the start date field.
    /// </summary>
    /// <value>The start date field.</value>
    public DateField StartDateField { get; set; }
    /// <summary>
    /// Gets or sets the end date field.
    /// </summary>
    /// <value>The end date field.</value>
    public DateField EndDateField { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRangeField"/> class.
    /// </summary>
    /// <param name="dateRangevalidator">The date rangevalidator.</param>
    /// <param name="dateFieldValidator">The date field validator.</param>
    /// <param name="integerFieldValidator">The integer field validator.</param>
    public DateRangeField(IValidator<IDateRangeField> dateRangevalidator, IValidator<IDateField> dateFieldValidator, IValidator<IIntegerField> integerFieldValidator) {
      _validator = dateRangevalidator;
      _integerFieldValidator = integerFieldValidator;
      _dateFieldValidator = dateFieldValidator;
      StartDateField = new DateField(dateFieldValidator) { DateTime = _initialValue };
      EndDateField = new DateField(dateFieldValidator) { DateTime = _initialValue };
    }
    /// <summary>
    /// Does the validate.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
