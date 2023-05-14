using HealthChecks.UI.Client;
using libraries.Extentions.DotNetCore.HealthChecks;
using libraries.Extentions.DotNetCore.Logging;
using Prometheus;
using fields.Extensions;
using libraries.Extentions.ExtenstionMethods;
using Bouvet.Notification.Service.ExtenstionMethods;
using fields.HttpServices.CVPartnerService.Extensions;

var applicationName = "bouvet-notification-service";
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();
builder.AddFields();
builder.AddCustomSerilog(applicationName);
builder.AddDapr();
builder.AddRequiredServices();
builder.AddCustomMediator();
builder.AddCustomHelathCheck();

WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
else {
  app.UseMetricServer();
  app.UseHttpMetrics();
  app.MapSubscribeHandler();
  app.UseCloudEvents();
  app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);
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
