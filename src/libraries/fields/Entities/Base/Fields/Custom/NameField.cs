using fields.Entities.Base.Fields.Base;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Custom {
  public interface INameField : IField {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    string Name { get; set; }
  }
  /// <summary>
  /// Class NameField.
  /// Implements the <see cref="Fields.Entities.Base.Fields.INameField" />
  /// </summary>
  /// <seealso cref="Fields.Entities.Base.Fields.INameField" />
  public partial class NameField : INameField, IAllowedType {
    /// <summary>
    /// The initial value
    /// </summary>
    private const string _initialValue = "";
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<INameField> _validator;
    /// <summary>
    /// Initializes a new instance of the <see cref="NameField"/> class.
    /// </summary>
    /// <param name="nameValidator">The name validator.</param>
    public NameField(IValidator<INameField> nameValidator) {
      Name = _initialValue;
      _validator = nameValidator;
    }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }
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
