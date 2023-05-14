using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Primitive {
  public interface IStringField : IField, IAllowedType {
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    string Value { get; set; }
  }
  /// <summary>
  /// Class StringField.
  /// Implements the <see cref="Libraries.Entities.Base.Fields.IStringField" />
  /// </summary>
  /// <seealso cref="Libraries.Entities.Base.Fields.IStringField" />
  public partial class StringField : IStringField {
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IStringField> _validator = FieldsFactory.StringValidator();
    /// <summary>
    /// Initializes a new instance of the <see cref="StringField"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public StringField(string value, IValidator<IStringField> validator) {
      Value = value;
      _validator = validator;
    }
    public StringField() { }
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    public string Value { get; set; } = string.Empty;
    /// <summary>
    /// The method to validate the object
    /// </summary>
    /// <returns>Validation result</returns>
    public static StringField operator +(StringField a, StringField b)
   => FieldsFactory.StringField(a.Value + b.Value);


    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }
    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}
