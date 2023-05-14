using fields.Entities.Base.Fields.Custom;
using FluentValidation;
namespace fields.Entities.Base.Validators {
  public class IdFieldValidator<T> : AbstractValidator<IIdField<T>> {
    /// <summary>
    /// Initializes a new instance of the <see cref="IdFieldValidator"/> class.
    /// </summary>
    public IdFieldValidator() : base() {
      RuleSet("Default", () => {
        RuleFor(x => x.Value)
        .NotEmpty()
        .NotNull()
        .WithMessage("IdFieldNotValid");
      });
    }
  }
}
