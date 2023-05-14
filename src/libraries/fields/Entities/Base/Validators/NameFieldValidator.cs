using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Extensions;
using fields.Messages;
using FluentValidation;
using Microsoft.Extensions.Options;
namespace fields.Entities.Base.Validators {
  public class NameFieldValidator : AbstractValidator<INameField> {
    public NameFieldValidator(IOptions<NameFieldConfigOption> config) {
      RuleSet("Default", () => {
        RuleFor(x => x.Name)
        .NotEmpty()
        .NotNull()
        .MaximumLength(config.Value.Max)
        .MinimumLength(config.Value.Min)
        .WithMessage(SystemMessages.Validation.NameFieldNotValid);
      });
      RuleSet("UpperCaseFirstLetter", () => {
        RuleFor(x => x.Name)
        .Must(x => Must.StartsWithUppercase(x));
      });
      RuleSet("IllegalCharacter", () => {
        RuleFor(x => x.Name)
        .Must(x => Must.ContainsOnlylegalCharacters(x));
      });
    }
  }
}
