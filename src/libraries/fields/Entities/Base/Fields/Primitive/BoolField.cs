using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Primitive {
  public interface IBoolField : IField, IAllowedType {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IBoolField"/> is value.
    /// </summary>
    /// <value>This propery is used as the value of the <see cref="BoolField" /> class.</value>
    bool Value { get; set; }
  }
  /// <summary>
  /// A custom <see cref="BoolField" /> class to give added functionalities.
  /// </summary>
  public partial class BoolField : IBoolField {
    /// <summary>
    /// The initial value
    /// </summary>
    private const bool _initialValue = false;
    /// <inheritdoc/>
    public bool Value { get; set; }
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IBoolField> _validator = FieldsFactory.BoolValidator();
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolField" /> class.
    /// </summary>
    /// <param name="validator">FluentValidation validator for <see cref="BoolField" /> class.</param>
    public BoolField(IValidator<IBoolField> validator) {
      _validator = validator;
      Value = _initialValue;
    }
    public BoolField() { }
    /// <inheritdoc/>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }

  }
}
