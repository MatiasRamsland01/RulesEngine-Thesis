using FluentValidation;
namespace fields.Entities.Base.FieldsConfiguration {
  public class LimitConfigOption {
    /// <summary>
    /// The limit configuration
    /// </summary>
    public const string LimitConfig = "LimitConfig";
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; set; }
  }
  /// <summary>
  /// Class LimitConfigValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.LimitConfigOption}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{Fields.Entities.Base.FieldsConfiguration.LimitConfigOption}" />
  public class LimitConfigValidator : AbstractValidator<LimitConfigOption> {
    /// <summary>
    /// Initializes a new instance of the <see cref="LimitConfigValidator"/> class.
    /// </summary>
    public LimitConfigValidator() {
      RuleFor(x => x.Value)
        .NotNull()
        .NotEmpty()
        .GreaterThan(0);
    }
  }
}
