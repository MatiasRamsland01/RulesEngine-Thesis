using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.FieldsConfiguration;
using fields.Messages;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace fields.Entities.Base.Validators {
  public class IntegerFieldValidator : AbstractValidator<IIntegerField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerFieldValidator"/> class.
    /// </summary>
    /// <param name="config">The configuration.</param>
    public IntegerFieldValidator(IOptions<IntegerFieldConfigOption> config) {
      RuleSet("Default", () => {
        RuleFor(m => m.Value)
          .NotNull()
          .WithMessage(SystemMessages.Validation.NullOrEmptyMessage)
          .LessThanOrEqualTo(config.Value.Max)
          .GreaterThanOrEqualTo(config.Value.Min)
          .WithMessage(SystemMessages.Validation.OutOfBoundary);
      });
    }
  }
}
