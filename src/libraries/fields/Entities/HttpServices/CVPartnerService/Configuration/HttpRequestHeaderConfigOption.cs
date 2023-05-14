using fields.Entities.Base.Fields.Primitive;
using FluentValidation;
using FluentValidation.Results;

namespace fields.Entities.HttpServices.CVPartnerService.Configuration {
  public interface IHttpRequestHeaderConfigOption {
    Dictionary<string, StringField> Headers { get; }
    public IntegerField RequestTimeoutSeconds { get; set; }
  }

  public class HttpRequestHeaderConfigOption : IHttpRequestHeaderConfigOption {
    public Dictionary<string, StringField> Headers { get; set; } = new Dictionary<string, StringField>();
    public IntegerField RequestTimeoutSeconds { get; set; } = new IntegerField();
  }

  public class HttpRequestHeaderConfigValidator : AbstractValidator<HttpRequestHeaderConfigOption> {
    public HttpRequestHeaderConfigValidator(IValidator<IStringField> stringFieldValidator, IValidator<IIntegerField> integerFieldValidator) {
      RuleFor(Config => Config.Headers["Accept"]).SetValidator(stringFieldValidator);
      RuleFor(Config => Config.Headers["UserAgent"]).SetValidator(stringFieldValidator);
      RuleFor(Config => Config.Headers["Cookie"]).SetValidator(stringFieldValidator);
      RuleFor(Config => Config.Headers["XCSRFToken"]).SetValidator(stringFieldValidator);
      RuleFor(Config => Config.RequestTimeoutSeconds).SetValidator(integerFieldValidator);
    }
  }
}