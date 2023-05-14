using fields.Entities.Base.Fields.Primitive;
using fields.Messages;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class BoolFieldValidator : AbstractValidator<IBoolField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolFieldValidator"/> class.
    /// </summary>
    public BoolFieldValidator() {
      RuleFor(x => x.Value)
      .NotNull()
      .NotEmpty()
      .Must(x => x == false || x == true)
      .WithMessage(SystemMessages.Validation.NotValid);
    }
  }
}
