using Dapr.Client;
using Dapr.Extensions.Configuration;
using FluentValidation;
using libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours;
using MediatR;
using MediatR.Pipeline;

namespace Bouvet.Notification.Service.ExtenstionMethods {
  public static class ExtenstionMethods {
    public static void AddCustomConfiguration(this WebApplicationBuilder builder) {
      builder.Configuration.AddJsonFile("fieldsconfig.json", optional: false, reloadOnChange: true);
      if (!builder.Environment.IsDevelopment()) {
        builder.Configuration.AddDaprSecretStore("ruleengine-secretstore", new DaprClientBuilder().Build());

      }
    }
    public static void AddCustomServices(this WebApplicationBuilder builder) {
      builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
    public static void AddCustomMediator(this WebApplicationBuilder builder) {
      builder.Services.AddMediatR(typeof(Program))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
      .AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionBehaviour<,,>));
    }
  }
}
