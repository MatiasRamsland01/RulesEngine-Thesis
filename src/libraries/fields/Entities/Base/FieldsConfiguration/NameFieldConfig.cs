using FluentValidation;
namespace fields.Entities.Base.FieldsConfiguration {
  public class NameFieldConfigOption {
    /// <summary>
    /// The name field configuration
    /// </summary>
    public const string NameFieldConfig = "NameFieldConfig";
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
  /// Class NameFieldConfigValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.NameFieldConfigOption}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.NameFieldConfigOption}" />
  public class NameFieldConfigValidator : AbstractValidator<NameFieldConfigOption> {
    /// <summary>
    /// Initializes a new instance of the <see cref="NameFieldConfigValidator"/> class.
    /// </summary>
    public NameFieldConfigValidator() {
      RuleFor(x => x.Max)
        .NotNull()
        .NotEmpty();
      RuleFor(x => x.Min)
        .NotEmpty()
        .NotNull();
    }
  }
}
