using FluentValidation;
namespace fields.Entities.Base.FieldsConfiguration {
  public class AgeFieldConfigOption {
    /// <summary>
    /// The age field configuration
    /// </summary>
    public const string AgeFieldConfig = "AgeFieldConfig";
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
  /// Class AgeFieldConfigValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.AgeFieldConfigOption}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.AgeFieldConfigOption}" />
  public class AgeFieldConfigValidator : AbstractValidator<AgeFieldConfigOption> {
    /// <summary>
    /// Initializes a new instance of the <see cref="AgeFieldConfigValidator"/> class.
    /// </summary>
    public AgeFieldConfigValidator() {
      RuleFor(x => x.Max)
        .NotNull()
        .NotEmpty();
      RuleFor(x => x.Min)
        .NotEmpty()
        .NotNull();
    }
  }
}
