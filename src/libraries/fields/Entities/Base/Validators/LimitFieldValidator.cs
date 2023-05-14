using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Messages;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace fields.Entities.Base.Validators {
  public class LimitFieldValidator : AbstractValidator<ILimitField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="LimitFieldValidator"/> class.
    /// </summary>
    /// <param name="limitConfig">The limit configuration.</param>
    public LimitFieldValidator(IOptions<LimitConfigOption> limitConfig) {
      RuleSet("Default", () => {
        RuleFor(x => x.Limit)
        .NotEmpty()
        .NotNull()
        .LessThanOrEqualTo(limitConfig.Value.Value)
        .WithMessage(SystemMessages.Validation.LimitFieldNotValid);
      });
    }
  }
}
