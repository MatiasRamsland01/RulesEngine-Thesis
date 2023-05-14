using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Primitive {
  public interface IIntegerField : IField, IAllowedType {
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; set; }
  }
  /// <summary>
  /// Class IntegerField.
  /// Implements the <see cref="Libraries.Entities.Base.Fields.IIntegerField" />
  /// </summary>
  /// <seealso cref="Libraries.Entities.Base.Fields.IIntegerField" />
  public partial class IntegerField : IIntegerField {
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IIntegerField> _validator = FieldsFactory.IntegerValidator();
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerField"/> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public IntegerField(IValidator<IIntegerField> validator) {
      _validator = validator;
    }
    public IntegerField() { }
    /// <summary>
    /// Gets or sets the value.<<<<<<<<<s
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; set; } = 0;

    public static IntegerField operator +(IntegerField a, IntegerField b)
   => FieldsFactory.IntegerField(a.Value + b.Value);

    public static IntegerField operator -(IntegerField a, IntegerField b)
    => FieldsFactory.IntegerField(a.Value - b.Value);

    public static IntegerField operator *(IntegerField a, IntegerField b)
    => FieldsFactory.IntegerField(a.Value * b.Value);

    public static IntegerField operator /(IntegerField a, IntegerField b)
    => FieldsFactory.IntegerField(a.Value / b.Value);

    public static IntegerField operator %(IntegerField a, IntegerField b)
    => FieldsFactory.IntegerField(a.Value % b.Value);

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
//
