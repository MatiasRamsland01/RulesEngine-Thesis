using fields.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace fields.HttpServices.CVPartnerService.Extensions {
  public static class HttpClientExtention {
    public static void AddHttpConfiguration(this WebApplicationBuilder builder) {
      if (!builder.Environment.IsDevelopment()) {
        //Adds Fields to IOC with given validator and with config in config file
        builder.Services.AddHttpRequestHeaderConfigToIOC(builder.Configuration);
        builder.Services.AddHttpRequestUriConfigToIOC(builder.Configuration);
        builder.Services.AddHttpParameterConfigToIOC(builder.Configuration);
        builder.Services.AddHttpClientToIOC(builder.Configuration);
      }
    }
  }
}
