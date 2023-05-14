// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="Program.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Extensions;
using FluentValidation;
using HealthChecks.UI.Client;
using libraries.Extentions.DotNetCore.HealthChecks;
using libraries.Extentions.DotNetCore.Logging;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var applicationName = "cv-partner-service";
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Configuration.AddJsonFile("fieldsconfig.json", optional: false, reloadOnChange: true);
builder.Services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy()).AddCheck<DaprHealthCheck>("dapr");
builder.AddCustomSerilog(applicationName);
builder.AddFields();
builder.Services.AddDaprClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
WebApplication? app = builder.Build();
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCloudEvents();
app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.UseCors("CorsPolicy");
app.MapSubscribeHandler();
app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);


try {
  app.Logger.LogInformation("Startings web host ({ApplicationName})...", applicationName);
  app.Run();
}
catch (Exception ex) {
  app.Logger.LogError(ex, "Host terminated unexpectedly ({ApplicationName})...", applicationName);
}
finally {
  Serilog.Log.CloseAndFlush();
}
