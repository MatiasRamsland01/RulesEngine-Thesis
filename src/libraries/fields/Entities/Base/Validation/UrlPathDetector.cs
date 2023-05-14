using fields.Entities.Base.Fields.Primitive;
using FluentAssertions;
using System.Text.RegularExpressions;
namespace fields.FluentValidation.Rules {
  /// <summary>
  /// 
  /// </summary>
  public enum CommunicationComponent { NONE = 0, SCHEME = 1, AUTHORITY = 2, PATH = 3, QUERY = 4, FRAGMENT = 5, ABSOLUTE = 6, RELATIVE = 7 };
  /// <summary>
  /// Find the matching enum type by checking the path against an RegEx
  /// </summary>
  public class UrlPathDetector {
    public CommunicationComponent FindType(IStringField path) {
      Uri uriAddress1 = new Uri(path.Value);
      // int matches2 = uriAddress1.Segments.Count();
      int componentCount = CountUriComponents(uriAddress1);
      return UrlPathDetectorHelpers.MapNumberGroupsToEnum(componentCount);
    }

    public static int CountUriComponents(Uri uri) {
      int count = 0;

      if (!string.IsNullOrEmpty(uri.Scheme)) {
        count++;
      }

      if (!string.IsNullOrEmpty(uri.Authority)) {
        count++;
      }

      if (!string.IsNullOrEmpty(uri.AbsolutePath) && uri.AbsolutePath != "/") {
        count++;
      }

      if (!string.IsNullOrEmpty(uri.Query)) {
        count++;
      }

      if (!string.IsNullOrEmpty(uri.Fragment)) {
        count++;
      }

      return count;
    }

    /// <summary>
    /// Source: 
    /// http://jmrware.com/articles/2009/uri_regexp/URI_regex.html 
    /// GITS:
    /// https://gist.github.com/curtisz/11139b2cfcaef4a261e0
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public static bool IsPathType(string candidate) => Enum.IsDefined(typeof(CommunicationComponent), candidate);
    private Match Analyze(string source) {

      var regex = new Regex(@"^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\\\?([^#]*))?(#(.*))?");
      Match? match = regex.Match(source);
      return match;
    }
    //scheme        : matches[2],
    //authority     : matches[4],
    //path          : matches[5],
    //query         : matches[7],
    //fragment      : matches[9]
  }
}