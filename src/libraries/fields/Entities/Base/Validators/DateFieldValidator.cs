using fields.Entities.Base.Fields.Dates;
using fields.FluentValidation.Rules;
using fields.Messages;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class DateFieldValidator : AbstractValidator<IDateField> {
    public DateFieldValidator() {
      RuleSet("Default", () => {
        RuleFor(m => m.DateTime)
        .NotNull()
        .NotEmpty()
        .Must(TryParse.ToDateTime)
        .WithMessage(SystemMessages.Validation.NullOrEmptyMessage);
      });
    }
  }

}

