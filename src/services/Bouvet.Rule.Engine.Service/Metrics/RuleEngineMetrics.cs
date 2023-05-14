using Prometheus;

namespace Bouvet.Rule.Engine.Service.Statistics {
  public static class RulesEngineMetrics {
    public static readonly Counter RuleEngineExecutionCounter = Metrics.CreateCounter("rules_engine_executed_total", "Total number of times the rules engine has run");
    public static readonly Counter RuleEngineExecutionFailedCounter = Metrics.CreateCounter("rules_engine_executed_failed_total", "Total number of times the rules engine has failed");
    public static readonly Counter RuleEngineExecutionSucceededCounter = Metrics.CreateCounter("rules_engine_executed_succeeded_total", "Total number of times the rules engine has succeeded");
    public static readonly Histogram RuleEngineExecutionTime = Metrics.CreateHistogram("rules_engine_execution_time", "Total number of milliseconds the rules engine used running a workflow");
  }
}
