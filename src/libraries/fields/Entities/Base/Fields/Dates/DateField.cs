using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Dates {
  public interface IDateField : IField, IAllowedType {
    /// <summary>
    /// Gets the date time.
    /// </summary>
    /// <value>This propery is used as the value of the <see cref="DateField" /> class.</value>
    public string DateTime { get; }
  }
  /// <summary>
  /// A custom <see cref="DateField" /> class to give added functionalities.
  /// </summary>
  public partial class DateField : IDateField {
    /// <summary>
    /// The initial value
    /// </summary>
    private const string _initialValue = "";
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IDateField> _validator = FieldsFactory.DateValidator();
    /// <summary>
    /// Initializes a new instance of the <see cref="DateField" /> class.
    /// </summary>
    /// <param name="validator">FluentValidation validator for <see cref="DateField" /> class.</param>
    public DateField(IValidator<IDateField> validator) {
      _validator = validator;
    }
    public DateField() { }
    /// <inheritdoc/>
    public string DateTime { get; set; } = "";
    /// <inheritdoc/>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
