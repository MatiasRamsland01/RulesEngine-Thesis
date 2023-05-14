using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace fields.Extensions {

  /// <summary>
  /// A static class to provide additional custom validation for fluent validation validators.
  /// </summary>
  public class Must {

    /// <summary>
    /// A static method to check if a string have a first uppercase letters.
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Result of the validation check</returns>
    public static bool StartsWithUppercase(string value) {
      Debug.Assert(value != null && value is string);
      return char.IsUpper(value[0]);
    }

    /// <summary>
    /// A static method to check if a value is greater than the min value.
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <param name="minLength">Lowerbound value</param>
    /// <returns>Result of the validation</returns>
    public static bool MinLength(string value, int minLength) {
      Debug.Assert(value is not null &&
                    value is string);
      {
        return value.Length > minLength;
      }
    }

    /// <summary>
    /// A method to check if a value is less than the max value.
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <param name="maxLength">Upperbound value</param>
    /// <returns>Result of the validation</returns>
    public static bool MaxLength(string value, int maxLength) {
      Debug.Assert(value is not null &&
                    value is string);
      {
        return value.Length > maxLength;
      }
    }

    /// <summary>
    /// A method to check if a string is compileable
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Result of the validation</returns>
    public static bool Compile(string value) {
      SyntaxTree tree = CSharpSyntaxTree.ParseText(value);
      IEnumerable<Diagnostic> result = tree.GetDiagnostics();
      return result.Count() == 0;
    }

    /// <summary>
    /// A method to check if a string is a expression without user inputs
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool IsExpressionWithuotInputs(string value) {
      try {
        Expression expression_tree = DynamicExpressionParser.ParseLambda(new ParameterExpression[] { }, null, value);
        return true;
      }
      catch (ParseException) {
        return false;
      }
    }

    /// <summary>
    /// A method to check if a string is a valid JSON object
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool BeJSON(string value) {
      try {
        var jsonObject = JsonSerializer.Deserialize<object>(value);
        return true;
      }
      catch {
        return false;
      }
    }

    /// <summary>
    /// A method to check that a string only contains letters.
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool ContainsOnlyLetters(string value) => value.All(char.IsLetter);

    /// <summary>
    /// A method to check that a string only contains digits
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool ContainsOnlyDigits(string value) => value.All(char.IsDigit);

    /// <summary>
    /// A method to check that a string is only numbers and digits
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool ContainsOnlyNumbersOrDigits(string value) => value.All(char.IsLetterOrDigit);

    /// <summary>
    /// A method to check that a string only contains legal characters. This includes letters, digits and the most common non dangerous symbols
    /// </summary>
    /// <param name="value">String to be evaluated</param>
    /// <returns>Validation result</returns>
    public static bool ContainsOnlylegalCharacters(string value) {
      Regex rx = new(@"^[ ÆØÅæøåa-zA-Z,.!\/\=+;():'_@\-\\s\d]+$");
      Match result = rx.Match(value);
      return result.Success;
    }
    // This will need some discussion before creating a validator. Shall we use global or local params as in https://microsoft.github.io/RulesEngine/
    //public static bool IsExpressionWithInputs(IStringType value, dynamic inputs)
    //{
    //}
  }
}
