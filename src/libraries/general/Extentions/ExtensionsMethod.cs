using libraries.Extentions.DotNetCore.HealthChecks;
using libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace libraries.Extentions.ExtenstionMethods {
  public static class ExtenstionMethods {
    public static void AddRequiredServices(this WebApplicationBuilder builder) {
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

    }
    public static void AddCustomHelathCheck(this WebApplicationBuilder builder) {
      if (!builder.Environment.IsDevelopment()) {
        builder.Services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy()).AddCheck<DaprHealthCheck>("dapr");
      }
    }
    public static void AddDapr(this WebApplicationBuilder builder) {
      builder.Services.AddDaprClient();
    }


  }
}
