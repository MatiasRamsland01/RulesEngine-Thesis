using fields.FluentValidation.Rules;
using FluentAssertions;
using fields.Entities.Base.Fields.Net;

namespace tests.Entities.Fields {
  public class UriFieldTests {
    private UriField BaseUri;
    private UriField RelativeUri;
    public UriFieldTests() {
      BaseUri = new UriField();
      RelativeUri = new UriField();
    }

    [Theory]
    [InlineData("https://bouvet.cvpartner.com/api", "/v1/users", CommunicationComponent.PATH, true)]
    [InlineData("http://www.api.com", "/api/search?query=id=1", CommunicationComponent.QUERY, true)]

    //[InlineData("http://www.api.com","/api?id=12",CommunicationComponent.PATH, true)]
    public void ShouldGiveCorrectUrlType(string uri, string relative, CommunicationComponent type, bool expected) {

      BaseUri.UriValue.Value = uri;
      RelativeUri.UriValue.Value = relative;
      UriField? Uri = BaseUri + RelativeUri;
      var validationResult = Uri.DoValidate();
      // Uri.PathType.Should().Be(type);
      validationResult.Errors.Should().BeEmpty();
      validationResult.IsValid.Should().Be(expected);
    }
  }
}