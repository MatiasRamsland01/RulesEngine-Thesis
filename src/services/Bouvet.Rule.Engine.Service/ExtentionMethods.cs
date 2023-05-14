using Dapr.Client;
using Dapr.Extensions.Configuration;
using FluentValidation;
using libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours;
using MediatR;
using MediatR.Pipeline;
using Newtonsoft.Json.Converters;

namespace Bouvet.Rule.Engine.Service.ExtenstionMethods {
  public static class ExtenstionMethods {
    public static void AddCustomConfiguration(this WebApplicationBuilder builder) {
      builder.Configuration.AddJsonFile("fieldsconfig.json", optional: false, reloadOnChange: true);
      if (!builder.Environment.IsDevelopment()) {
        builder.Configuration.AddDaprSecretStore("ruleengine-secretstore", new DaprClientBuilder().Build());


      }
    }
    public static void AddCustomServices(this WebApplicationBuilder builder) {
      builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
      builder.Services.AddSingleton<IBackgroundTaskQueue>(ctx => {
        return new DefaultBackgroundTaskQueue(100000);
      });
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
      builder.Services.AddControllers().AddNewtonsoftJson(options => {
        options.SerializerSettings.Converters.Add(new ExpandoObjectConverter());
      });
    }
    public static void AddCustomMediator(this WebApplicationBuilder builder) {
      builder.Services.AddMediatR(typeof(Program))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
      .AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionBehaviour<,,>));
    }
    public static void AddCustomHostedService(this WebApplicationBuilder builder) {
      builder.Services.AddHostedService<QueuedHostedService>();
    }
  }
}
