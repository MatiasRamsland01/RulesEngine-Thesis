using fields.Entities.Base.Fields.Primitive;
using fields.Messages;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class CharFieldValidator : AbstractValidator<ICharField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="CharFieldValidator"/> class.
    /// </summary>
    public CharFieldValidator() {
      RuleSet("Default", () => {
        RuleFor(c => c.Value.ToString())
        .NotEmpty()
        .NotNull()
        .WithMessage(SystemMessages.Validation.NullOrEmptyMessage)
        .Length(1).WithMessage(SystemMessages.Validation.OutOfBoundary);
      });
    }
  }
}
