using FluentValidation.Results;

namespace fields.Entities.Base.Validation {
  public interface Validate {
    public ValidationResult DoValidate();
    public ValidationResult DoValidateAndThrow();
  }
}
