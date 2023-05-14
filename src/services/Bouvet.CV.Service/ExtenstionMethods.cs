using Bouvet.CV.Service.Domain.Services;
using Bouvet.CV.Service.Infrastructure;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using FluentValidation;
using libraries.Extentions.DotNetCore.Mediator.PipelineBehaviours;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.CV.Service.ExtenstionMethods {
  public static class ExtenstionMethods {
    public static void AddCustomConfiguration(this WebApplicationBuilder builder) {
      builder.Configuration.AddJsonFile("fieldsconfig.json", optional: false, reloadOnChange: true);
      if (!builder.Environment.IsDevelopment()) {
        builder.Configuration.AddDaprSecretStore("ruleengine-secretstore", new DaprClientBuilder().Build());

      }
    }
    public static void AddCustomServices(this WebApplicationBuilder builder) {
      builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
      builder.Services.AddScoped<ICVService, CVService>();
    }
    public static void AddCustomMediator(this WebApplicationBuilder builder) {
      builder.Services.AddMediatR(typeof(Program))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
      .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
      .AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionBehaviour<,,>));
    }
    public static void AddCustomHostedService(this WebApplicationBuilder builder) {
      if (!builder.Environment.IsDevelopment()) {
        builder.Services.AddHostedService<CvSchedulerBackgroundService>();
      }
    }
    public static void AddCustomHttpClient(this WebApplicationBuilder builder) {
      if (!builder.Environment.IsDevelopment()) {
        builder.Services.AddHttpClient<CvPartnerClient>();
      }
    }
    public static void AddCustomDatabase(this WebApplicationBuilder builder) {
      if (!builder.Environment.IsDevelopment()) {
        builder.Services.AddDbContext<RuleengineContext>(options =>
          options.UseSqlServer(builder.Configuration["ConnectionStrings:WorkflowDB"]));
      }
      else {
        builder.Services.AddDbContext<RuleengineContext>(options =>
          options.UseSqlite("Data Source=workflow.db;"));
      }
    }

  }
}
