// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="AddSerilog.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace libraries.Extentions.DotNetCore.Logging {
  /// <summary>
  /// Class SerilogExtentions.
  /// </summary>
  public static class SerilogExtentions {
    /// <summary>
    /// Adds the serilog.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="applicationName">Name of the application.</param>
    public static void AddCustomSerilog(this WebApplicationBuilder builder, string applicationName) {
      var seqServerUrl = builder.Configuration["SeqServerUrl"];

      Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .WriteTo.Console()
          .WriteTo.Seq(seqServerUrl!)
          .Enrich.WithProperty("ApplicationName", applicationName)
          .CreateLogger();
      builder.Host.UseSerilog();
    }
  }
}
