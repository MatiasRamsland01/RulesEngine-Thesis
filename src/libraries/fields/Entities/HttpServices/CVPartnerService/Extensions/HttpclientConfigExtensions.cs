using fields.Entities.HttpServices.CVPartnerService.Configuration;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace fields.HttpServices.CVPartnerService.Extensions {
  public static class HttpClientConfigExtensions {

    public static IServiceCollection AddHttpRequestHeaderConfigToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<HttpRequestHeaderConfigOption>, HttpRequestHeaderConfigValidator>();
      services.Configure<HttpRequestHeaderConfigOption>(
      configuration.GetSection(nameof(HttpRequestHeaderConfigOption)))
        .AddOptions<HttpRequestHeaderConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<HttpRequestHeaderConfigOption>>().Validate(config).IsValid;
        })
      .ValidateOnStart();
      return services;
    }

    public static IServiceCollection AddHttpRequestUriConfigToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<HttpRequestUriConfigOption>, HttpRequestUriConfigValidator>();
      services.Configure<HttpRequestUriConfigOption>(
      configuration.GetSection(nameof(HttpRequestUriConfigOption)))
        .AddOptions<HttpRequestUriConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<HttpRequestUriConfigOption>>().Validate(config).IsValid;
        })
      .ValidateOnStart();
      return services;
    }

    public static IServiceCollection AddHttpParameterConfigToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<HttpRequestParametersConfigOption>, HttpRequestParametersConfigValidator>();
      services.Configure<HttpRequestParametersConfigOption>(
      configuration.GetSection(nameof(HttpRequestParametersConfigOption)))
        .AddOptions<HttpRequestParametersConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<HttpRequestParametersConfigOption>>().Validate(config).IsValid;
        })
      .ValidateOnStart();
      return services;
    }
  }
}
