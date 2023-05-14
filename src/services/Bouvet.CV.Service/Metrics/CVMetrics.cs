using Prometheus;

namespace Bouvet.CV.Service.MetricQueries {
  public static class CVMetrics {
    public static readonly Counter CVUpdatedCounter = Metrics.CreateCounter("cv_updated_total", "Total number of times CVs has been updated");
    public static readonly Counter CVFetchingProcessTime = Metrics.CreateCounter("cv_fetching_time", "Total number of seconds the CV fetching process takes");
    public static readonly Counter CVFetchCounter = Metrics.CreateCounter("cv_fetch_total", "Total number of times CVs has been fetched from CV partner");
    public static readonly Counter RulesExecution = Metrics.CreateCounter(
    "rule_runs",
    "Counts the number of successful runs of the rules",
    new CounterConfiguration {
      LabelNames = new[] { "rule_name", "status" }
    }
);

  }
}
