using FluentValidation.Results;
namespace fields.Entities.Base.Fields.Base {
  /// <summary>
  /// Interface IField
  /// </summary>
  public interface IField {
    /// <summary>
    /// A method to convert the class value to string
    /// </summary>
    /// <returns>The class value in string</returns>
    public string ToString();
    /// <summary>
    /// The method to validate the object
    /// </summary>
    /// <returns>Validation result</returns>
    public ValidationResult DoValidate();
    public ValidationResult DoValidateAndThrow();
  }
}
