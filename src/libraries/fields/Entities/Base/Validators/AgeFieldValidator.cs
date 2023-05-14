using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Messages;
using FluentValidation;
using Microsoft.Extensions.Options;
namespace fields.Entities.Base.Validators {
  public class AgeFieldValidator : AbstractValidator<IAgeField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="AgeFieldValidator"/> class.
    /// </summary>
    /// <param name="config">The configuration.</param>
    public AgeFieldValidator(IOptions<AgeFieldConfigOption> config) {
      RuleSet("Default", () => {
        RuleFor(x => x.Age)
        .NotNull()
        .NotEmpty()
        .WithMessage(SystemMessages.Validation.NullOrEmptyMessage)
        .GreaterThanOrEqualTo(config.Value.Min)
        .LessThanOrEqualTo(config.Value.Max)
        .WithMessage(SystemMessages.Validation.OutOfBoundary);
      });
    }
  }
}
