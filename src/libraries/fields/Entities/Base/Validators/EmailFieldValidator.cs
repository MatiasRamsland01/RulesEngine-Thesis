using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Custom;
using fields.Messages;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class EmailFieldValidator : AbstractValidator<IEmailField> {
    public EmailFieldValidator() {
      RuleFor(x => x.Value)
          .NotEmpty()
          .EmailAddress()
          .WithMessage("Invalid email format.");
    }
  }
}
