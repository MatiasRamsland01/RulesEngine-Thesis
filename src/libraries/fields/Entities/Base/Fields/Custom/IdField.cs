using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Custom {
  public interface IIdField<T> : IField, IAllowedType {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    T Value { get; set; }
  }
  /// <summary>
  /// Class IdField.
  /// Implements the <see cref="Fields.Entities.Base.Fields.IIdField" />
  /// </summary>
  /// <seealso cref="Fields.Entities.Base.Fields.IIdField" />
  public partial class IdField<T> : IIdField<T> {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public T Value { get; set; } = default!;
    /// <summary>
    /// The validator
    /// </summary>
    private IValidator<IIdField<T>> _validator = default!;
    /// <summary>
    /// Initializes a new instance of the <see cref="IdField"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public IdField(T value, IValidator<IIdField<T>>? validator = null) {
      validator ??= FieldsFactory.IdValidator<T>();
      _validator = validator;
      Value = value;
    }
    public IdField() { }
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
