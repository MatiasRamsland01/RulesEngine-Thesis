using fields.Entities.Base.Fields.Base;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Primitive {
  public interface ICharField : IField, IAllowedType {
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>This propery is used as the value of the <see cref="CharField" /> class.</value>
    char Value { get; set; }
  }
  /// <summary>
  /// A custom <see cref="CharField" /> class to give added functionalities.
  /// </summary>
  public partial class CharField : ICharField {
    /// <summary>
    /// The initial value
    /// </summary>
    private const char _initialValue = '\0';
    /// <inheritdoc/>
    public char Value { get; set; }
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<ICharField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="CharField" /> class.
    /// </summary>
    /// <param name="validator">FluentValidation validator for <see cref="BoolField" /> class.</param>
    public CharField(
  IValidator<ICharField> validator) {
      Value = _initialValue;
      _validator = validator;
    }
    /// <inheritdoc/>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
