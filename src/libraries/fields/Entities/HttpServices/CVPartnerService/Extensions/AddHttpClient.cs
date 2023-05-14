using fields.Entities.HttpServices.CVPartnerService.Configuration;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;

namespace fields.HttpServices.CVPartnerService.Extensions {
  public static class AddHttpClient {
    public static IServiceCollection AddHttpClientToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddHttpClient<CvPartnerClient>((serviceProvider, client) => {
        var httpRequestHeaderConfigOption = serviceProvider.GetRequiredService<IOptionsMonitor<HttpRequestHeaderConfigOption>>().CurrentValue;
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("Accept", httpRequestHeaderConfigOption.Headers["Accept"].Value);
        client.DefaultRequestHeaders.Add("User-Agent", httpRequestHeaderConfigOption.Headers["UserAgent"].Value);
        client.DefaultRequestHeaders.Add("Cookie", httpRequestHeaderConfigOption.Headers["Cookie"].Value);
        client.DefaultRequestHeaders.Add("XCSRFToken", httpRequestHeaderConfigOption.Headers["XCSRFToken"].Value);
        client.Timeout = TimeSpan.FromSeconds(httpRequestHeaderConfigOption.RequestTimeoutSeconds.Value);
      });
      return services;
    }
  }
}