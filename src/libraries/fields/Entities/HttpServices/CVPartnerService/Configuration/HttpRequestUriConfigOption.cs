using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Fields.Net;
using FluentValidation;
using FluentValidation.Results;

namespace fields.Entities.HttpServices.CVPartnerService.Configuration {
  public interface IHttpRequestUriConfigOption {
    Dictionary<string, UriField> Uris { get; }
    public StringField RequestMethod { get; set; }
  }

  public class HttpRequestUriConfigOption : IHttpRequestUriConfigOption {
    public Dictionary<string, UriField> Uris { get; set; } = new Dictionary<string, UriField>();
    public StringField RequestMethod { get; set; } = new StringField();
  }

  public class HttpRequestUriConfigValidator : AbstractValidator<HttpRequestUriConfigOption> {
    public HttpRequestUriConfigValidator(IValidator<IStringField> stringFieldValidator, IValidator<IUriField> uriFieldValidator) {
      RuleFor(Config => Config.Uris["BaseAddress"]).SetValidator(uriFieldValidator);
      RuleFor(Config => Config.Uris["GetUsersUrl"]).SetValidator(uriFieldValidator);
      RuleFor(Config => Config.Uris["GetCvUrl"]).SetValidator(uriFieldValidator);
      RuleFor(Config => Config.RequestMethod).SetValidator(stringFieldValidator);
    }
  }
}