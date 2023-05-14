using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace fields.Entities.HttpServices.CVPartnerService.Configuration {
  public interface IHttpRequestParametersConfigOption {
    Dictionary<string, IntegerField> Parameters { get; }
  }

  public class HttpRequestParametersConfigOption : IHttpRequestParametersConfigOption {
    public Dictionary<string, IntegerField> Parameters { get; set; } = new Dictionary<string, IntegerField>();
  }

  public class HttpRequestParametersConfigValidator : AbstractValidator<HttpRequestParametersConfigOption> {
    public HttpRequestParametersConfigValidator(IValidator<IIntegerField> integerFieldValidator) {

      RuleFor(Config => Config.Parameters["Limit"]).SetValidator(integerFieldValidator);
      RuleFor(Config => Config.Parameters["Offset"]).SetValidator(integerFieldValidator);
      RuleFor(Config => Config.Parameters["Limit"].Value)
          .GreaterThan(0)
          .WithMessage("Limit configuration value must be one or greater.");
      RuleFor(Config => Config.Parameters["Offset"].Value)
          .GreaterThanOrEqualTo(0)
          .WithMessage("Offset configuration value must be zero or greater.");
    }
  }
}