using fields.Entities.Base.Fields.Primitive;


namespace fields.Entities.HttpServices.CVPartnerService.Configuration {
  public interface IHttpClientConfigOption {
    HttpRequestUriConfigOption HttpRequestUriConfigOption { get; set; }
    HttpRequestHeaderConfigOption HttpRequestHeaderConfigOption { get; set; }
    HttpRequestParametersConfigOption HttpRequestParametersConfigOption { get; set; }
    IntegerField RequestTimeoutSeconds { get; set; }
  }

  public class HttpClientConfigOption : IHttpClientConfigOption {
    public const string HttpClientConfig = "HttpClientConfig";
    public HttpRequestUriConfigOption HttpRequestUriConfigOption { get; set; } = new HttpRequestUriConfigOption();
    public HttpRequestHeaderConfigOption HttpRequestHeaderConfigOption { get; set; } = new HttpRequestHeaderConfigOption();
    public HttpRequestParametersConfigOption HttpRequestParametersConfigOption { get; set; } = new HttpRequestParametersConfigOption();
    public IntegerField RequestTimeoutSeconds { get; set; } = new IntegerField();
  }
}