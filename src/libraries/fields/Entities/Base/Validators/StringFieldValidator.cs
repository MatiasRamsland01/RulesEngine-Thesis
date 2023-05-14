using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.FieldsConfiguration;
using fields.Extensions;
using fields.Messages;
using FluentValidation;
using Microsoft.Extensions.Options;
namespace fields.Entities.Base.Validators {
  public class StringFieldValidator : AbstractValidator<IStringField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="StringFieldValidator"/> class.
    /// </summary>
    /// <param name="configuraiton">The configuraiton.</param>
    public StringFieldValidator(IOptions<StringFieldConfigOption> configuraiton) {
      RuleFor(x => x.Value)
      .NotNull()
      .NotEmpty()
      .WithMessage(SystemMessages.Validation.NullOrEmptyMessage)
      .MaximumLength(configuraiton.Value.Max)
      .MinimumLength(configuraiton.Value.Min)
      .WithMessage(SystemMessages.Validation.OutOfBoundary)
      .Must(x => Must.ContainsOnlylegalCharacters(x))
      .WithMessage(SystemMessages.Validation.IllegalCharacters);
    }
  }
}
