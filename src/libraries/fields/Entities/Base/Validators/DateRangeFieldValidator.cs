using fields.Entities.Base.Fields.Dates;
using fields.Messages;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class DateRangeFieldValidator : AbstractValidator<IDateRangeField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="DateRangeFieldValidator"/> class.
    /// </summary>
    public DateRangeFieldValidator() {
      RuleSet("Default", () => {
        RuleFor(m => m.StartDateField).SetValidator(new DateFieldValidator());
        RuleFor(m => m.EndDateField).SetValidator(new DateFieldValidator());
        RuleFor(m => m.EndDateField.DateTime)
            .GreaterThanOrEqualTo(m => m.StartDateField.DateTime)
            .WithMessage(SystemMessages.Validation.DateRangeNotValid);
      });
    }
  }
}
