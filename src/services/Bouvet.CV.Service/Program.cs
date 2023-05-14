using Bouvet.CV.Service.Infrastructure;
using HealthChecks.UI.Client;
using libraries.Extentions.DotNetCore.HealthChecks;
using libraries.Extentions.DotNetCore.Logging;
using Prometheus;
using fields.Extensions;
using libraries.Extentions.BRulesEngine.ExtentionMethods;
using Bouvet.CV.Service.ExtenstionMethods;
using libraries.Extentions.ExtenstionMethods;
using Bouvet.CV.Service.Infrastructure.Configuration;
using fields.HttpServices.CVPartnerService.Extensions;

var applicationName = "bouvet-cv-service";
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
await Task.Delay(TimeSpan.FromSeconds(2));
builder.AddCustomConfiguration();
builder.AddFields();
builder.AddHttpConfiguration();
builder.AddCustomBRulesEngineEntitiesValidators();
builder.AddCustomServices();
builder.AddCustomSerilog(applicationName);
builder.AddCustomHelathCheck();
builder.AddDapr();
builder.AddCustomMediator();
builder.AddCustomHttpClient();
builder.AddCustomDatabase();
builder.AddCustomHostedService();
builder.AddRequiredServices();


WebApplication? app = builder.Build();
using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<RuleengineContext>();
  if (context.Database.EnsureCreated()) {
    PopulateDatabase.WithRules(context);
  }
}
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment()) {

  app.UseDeveloperExceptionPage();
}
else {
  app.UseMetricServer();
  app.UseHttpMetrics();
  app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);
  app.MapSubscribeHandler();
  app.UseCloudEvents();

}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapDefaultControllerRoute();
app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();

try {
  app.Logger.LogInformation("Starting web host ({ApplicationName})...", applicationName);
  app.Run();
}
catch (Exception ex) {
  app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", applicationName);
}
finally {
  Serilog.Log.CloseAndFlush();
}
public partial class Program { }
