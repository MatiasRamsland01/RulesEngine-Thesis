using FluentValidation;
namespace fields.Entities.Base.FieldsConfiguration {
  public class StringFieldConfigOption {
    /// <summary>
    /// The string field configuration
    /// </summary>
    public const string StringFieldConfig = "StringFieldConfig";
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
  /// Class StringFieldConfigValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.StringFieldConfigOption}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.StringFieldConfigOption}" />
  public class StringFieldConfigValidator : AbstractValidator<StringFieldConfigOption> {
    /// <summary>
    /// Initializes a new instance of the <see cref="StringFieldConfigValidator"/> class.
    /// </summary>
    public StringFieldConfigValidator() {
      RuleFor(x => x.Max)
        .NotNull()
        .NotEmpty();
      RuleFor(x => x.Min)
        .NotNull()
        .NotEmpty();
    }
  }
}
