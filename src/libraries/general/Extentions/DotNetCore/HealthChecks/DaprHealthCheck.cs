// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="DaprHealthCheck.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace libraries.Extentions.DotNetCore.HealthChecks {
  /// <summary>
  /// Class DaprHealthCheck.
  /// Implements the <see cref="IHealthCheck" />
  /// </summary>
  /// <seealso cref="IHealthCheck" />
  public class DaprHealthCheck : IHealthCheck {
    /// <summary>
    /// The dapr client
    /// </summary>
    private readonly DaprClient _daprClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="DaprHealthCheck"/> class.
    /// </summary>
    /// <param name="daprClient">The dapr client.</param>
    public DaprHealthCheck(DaprClient daprClient) {
      _daprClient = daprClient;
    }

    /// <summary>
    /// Check health as an asynchronous operation.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A Task&lt;HealthCheckResult&gt; representing the asynchronous operation.</returns>
    public async Task<HealthCheckResult> CheckHealthAsync(
    HealthCheckContext context,
    CancellationToken cancellationToken = default) {
      var healthy = await _daprClient.CheckHealthAsync(cancellationToken);

      if (healthy) {
        return HealthCheckResult.Healthy("Dapr sidecar is healthy.");
      }
      return HealthCheckResult.Unhealthy("Dapr sidecar is Unhealthy.");
    }
  }
}
