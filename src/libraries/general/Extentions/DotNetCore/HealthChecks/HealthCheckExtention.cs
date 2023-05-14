// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="HealthCheckExtention.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace libraries.Extentions.DotNetCore.HealthChecks {
  /// <summary>
  /// Class HealthCheckExtenstion.
  /// </summary>
  public static class HealthCheckExtenstion {
    /// <summary>
    /// Maps the custom health checks.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="healthPattern">The health pattern.</param>
    /// <param name="livenessPattern">The liveness pattern.</param>
    /// <param name="responseWriter">The response writer.</param>
    public static void MapCustomHealthChecks(
    this WebApplication app,
    string healthPattern = "/hc",
    string livenessPattern = "/liveness",
    Func<Microsoft.AspNetCore.Http.HttpContext, HealthReport, Task>? responseWriter = default) {
      app.MapHealthChecks(healthPattern, new HealthCheckOptions() {
        Predicate = _ => true,
        ResponseWriter = responseWriter!,
      });
      app.MapHealthChecks(livenessPattern, new HealthCheckOptions {
        Predicate = r => r.Name.Contains("self")
      });
    }
  }
}
