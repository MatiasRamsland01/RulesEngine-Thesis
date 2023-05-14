using fields.Entities.Base.Fields.Base;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Custom {
  public interface ILimitField : IField {
    /// <summary>
    /// Gets or sets the limit.
    /// </summary>
    /// <value>The limit.</value>
    public int Limit { get; set; }
  }
  /// <summary>
  /// Class LimitField.
  /// Implements the <see cref="Libraries.Entities.Base.Fields.ILimitField" />
  /// </summary>
  /// <seealso cref="Libraries.Entities.Base.Fields.ILimitField" />
  public partial class LimitField : ILimitField, IAllowedType {
    /// <summary>
    /// The initial value
    /// </summary>
    private const int _initialValue = -1;
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<LimitField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="LimitField"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public LimitField(IValidator<ILimitField> validator) {
      _validator = validator;
      Limit = _initialValue;
    }
    /// <summary>
    /// Gets or sets the limit.
    /// </summary>
    /// <value>The limit.</value>
    public int Limit { get; set; }
    /// <summary>
    /// The method to validate the object
    /// </summary>
    /// <returns>Validation result</returns>
    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
