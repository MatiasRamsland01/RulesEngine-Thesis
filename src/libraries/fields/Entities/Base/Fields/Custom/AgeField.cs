using fields.Entities.Base.Fields.Base;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Custom {
  public interface IAgeField : IField, IAllowedType {
    /// <summary>
    /// Gets or sets the age.
    /// </summary>
    /// <value>This propery is used as the value of the <see cref="AgeField" /> class.</value>
    int Age { get; set; }
  }
  /// <summary>
  /// A custom <see cref="AgeField" /> class to give added functionalities.
  /// </summary>
  public partial class AgeField : IAgeField {
    /// <summary>
    /// The initial value
    /// </summary>
    private const int _initialValue = -1;
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IAgeField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="AgeField" /> class.
    /// </summary>
    /// <param name="validator">FluentValidation validator for  <see cref="AgeField" /> class</param>
    public AgeField(IValidator<IAgeField> validator) {
      Age = _initialValue;
      _validator = validator;
    }
    /// <inheritdoc/>
    public int Age { get; set; }

    /// <inheritdoc/>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }

    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
