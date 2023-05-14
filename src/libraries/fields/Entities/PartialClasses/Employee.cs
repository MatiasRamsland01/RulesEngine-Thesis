// using FluentValidation;
// namespace fields.Entities {
//   public partial class Employee {
//     /// <summary>
//     /// Ensures the given data.
//     /// </summary>
//     /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException"></exception>
//     /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">The validation results objects is null!</exception>
//     private void EnsureGivenData() {
//       var validationResult = _employeeValidator.Validate(this);
//       if (validationResult != null && validationResult.IsValid) {
//         return;
//       }
//       if (validationResult != null) {
//         throw new ValidationException(validationResult.ToString());
//       }
//       throw new ValidationException("The validation results objects is null!");
//     }
//   }
// }
