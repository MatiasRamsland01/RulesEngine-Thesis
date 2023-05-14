namespace fields.Messages {
  public static class SystemMessages {
    /// <summary>
    /// Class Exception.
    /// </summary>
    public static class Exception {
      /// <summary>
      /// Invalids the argument.
      /// </summary>
      /// <param name="argument">The argument.</param>
      /// <returns>System.String.</returns>
      public static string InvalidArgument(string argument) {
        return $"Invalid argument Exception. The argument:{argument} is invalid or not found";
      }
    };
    /// <summary>
    /// Class General.
    /// </summary>
    public static class General { }
    /// <summary>
    /// Class Validation.
    /// </summary>
    public static class Validation {
      /// <summary>
      /// Gets the null message.
      /// </summary>
      /// <value>The null message.</value>
      public static string NullMessage => "{The {PropertyName} is null";
      /// <summary>
      /// Gets the null or empty message.
      /// </summary>
      /// <value>The null or empty message.</value>
      public static string NullOrEmptyMessage => "The {PropertyName} is either null or empty";
      /// <summary>
      /// Gets the out of boundary.
      /// </summary>
      /// <value>The out of boundary.</value>
      public static string OutOfBoundary => "The {PropertyName} with {ProperyValue} is failing";
      /// <summary>
      /// Gets the directory not exists.
      /// </summary>
      /// <value>The directory not exists.</value>
      public static string DirectoryNotExists => "The {PropertyName} reference a directory that does not exist";
      /// <summary>
      /// Gets the file not exists.
      /// </summary>
      /// <value>The file not exists.</value>
      public static string FileNotExists => "The {PropertyName} reference to a file that does not exist";
      /// <summary>
      /// Gets the general name field.
      /// </summary>
      /// <value>The general name field.</value>
      public static string GeneralNameField => "The {PropertyName} reference to a file that does not exist";
      /// <summary>
      /// Gets the identifier not valid.
      /// </summary>
      /// <value>The identifier not valid.</value>
      public static string IdNotValid => "Entity object id is not valid. The value was {PropertyValue}";
      /// <summary>
      /// Gets the limit field not valid.
      /// </summary>
      /// <value>The limit field not valid.</value>
      public static string LimitFieldNotValid => "The {PropertyName} with {PropertyValue} was not valid";
      /// <summary>
      /// Gets the name field not valid.
      /// </summary>
      /// <value>The name field not valid.</value>
      public static string NameFieldNotValid => "The {PropertyName} with {PropertyValue} was not valid";
      /// <summary>
      /// Gets the not valid.
      /// </summary>
      /// <value>The not valid.</value>
      public static string NotValid => "The {PropertyName} with {PropertyValue} was not valid";
      /// <summary>
      /// Gets the not valid nuber.
      /// </summary>
      /// <value>The not valid nuber.</value>
      public static string NotValidNuber => "The {PropertyName} with {PropertyValue} was not a valid number";
      /// <summary>
      /// Gets the date range not valid.
      /// </summary>
      /// <value>The date range not valid.</value>
      public static string DateRangeNotValid => "End Date Must Be Later Than Start Date";
      public static string IsUriPath => $"Invalid Uri in httpClient configuration, fails IsUriPath()";
      public static string CreateUriAndCheck => $"Invalid Uri in httpClient configuration, fails CreateUriAndCheck()";
      public static string InvalidHeader(string PropertyName) => $"The {PropertyName} is not a valid HttpHeader. Please rewrite the configuration";
      /// <summary>
      /// Class IntergerRuleSet.
      /// </summary>
      /// 
      public static string CollectionEmpty => "Collection was empty";

      public static string IllegalCharacters => "The {PropertyName} with {PropertyValue} contains illegal characters";

      public class IntergerRuleSet { }
      /// <summary>
      /// Class DateRuleSet.
      /// </summary>
      public class DateRuleSet { }
      /// <summary>
      /// Class CharRuleSet.
      /// </summary>
      public class CharRuleSet { }
    }
    /// <summary>
    /// Class Configuration.
    /// </summary>
    public static class Configuration {
      /// <summary>
      /// Class File.
      /// </summary>
      public static class File {
        /// <summary>
        /// Gets the MSG.
        /// </summary>
        /// <value>The MSG.</value>
        public static string Msg => $"File Configuration Error";
      }
      /// <summary>
      /// Class Template.
      /// </summary>
      public static class Template {
      }
      /// <summary>
      /// Class Field.
      /// </summary>
      public static class Field {
        /// <summary>
        /// Class Configuration.
        /// </summary>
        public static class Configuration {
          /// <summary>
          /// Intervals the error.
          /// </summary>
          /// <param name="min">The minimum.</param>
          /// <param name="max">The maximum.</param>
          /// <returns>System.String.</returns>
          public static string IntervalError(string min, string max) => $"Limit Error. The max:{max} is not really greater than min{min}";
        }
      }
    }
    /// <summary>
    /// Class Exceptions.
    /// </summary>
    public static class Exceptions {
      /// <summary>
      /// Gets the out of range.
      /// </summary>
      /// <value>The out of range.</value>
      public static string OutOfRange => "The value given to field are out of range";
    }
    /// <summary>
    /// Class Rules.
    /// </summary>
    public static class Rules {
      /// <summary>
      /// Class WorkflowGreaterThanOrEqual.
      /// </summary>
      public static class WorkflowGreaterThanOrEqual {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public static string Name { get; set; } = "WorkflowGreaterThanOrEqual";
      }
    }
    /// <summary>
    /// Class Notification.
    /// </summary>
    public static class Notification { }
    /// <summary>
    /// Class CV.
    /// </summary>
    public static class CV { }
    /// <summary>
    /// Class Dapr.
    /// </summary>
    public static class Dapr { }
    /// <summary>
    /// Class FileOperation.
    /// </summary>
    public static class FileOperation {
    }
  }
}
