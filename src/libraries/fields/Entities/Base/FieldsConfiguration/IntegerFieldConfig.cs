using FluentValidation;
namespace fields.Entities.Base.FieldsConfiguration {
  public class IntegerFieldConfigOption {
    /// <summary>
    /// The integer field configuration
    /// </summary>
    public const string IntegerFieldConfig = "IntegerFieldConfig";
    /// <summary>
    /// Determines the maximum of the parameters.
    /// </summary>
    /// <value>The maximum.</value>
    public int Max { get; set; }
    /// <summary>
    /// Determines the minimum of the parameters.
    /// </summary>
    /// <value>The minimum.</value>
    public int Min { get; set; }
  }
  /// <summary>
  /// Class IntegerFieldConfigValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.IntegerFieldConfigOption}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.IntegerFieldConfigOption}" />
  public class IntegerFieldConfigValidator : AbstractValidator<IntegerFieldConfigOption> {
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerFieldConfigValidator"/> class.
    /// </summary>
    public IntegerFieldConfigValidator() {
      RuleFor(x => x.Max)
        .NotNull()
        .NotEmpty();
      RuleFor(x => x.Min)
        .NotEmpty()
        .NotNull();
    }
  }
}
