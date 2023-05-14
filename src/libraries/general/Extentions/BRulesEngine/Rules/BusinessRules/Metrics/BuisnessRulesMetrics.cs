using Prometheus;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules.Statistics {
  public static class RulesEngineMetrics {
    public static readonly Counter CVMustBeUpdatedRegularlyCounter = Metrics.CreateCounter("cv_must_be_updated_regularly_total", "Total number of times the cv must be updated regularly rule has run");
    public static readonly Counter CVMustBeUpdatedRegularlyFailedCounter = Metrics.CreateCounter("cv_must_be_updated_regularly_failed_total", "Total number of times the cv must be updated regularly rule has failed");
    public static readonly Counter CVMustBeUpdatedRegularlySucceededCounter = Metrics.CreateCounter("cv_must_be_updated_regularly_succeeded_total", "Total number of times the cv must be updated regularly rule has succeeded");
    public static readonly Counter CvMustCategoriesTheirTechnologiesCounter = Metrics.CreateCounter("cv_must_categories_their_technologies_total", "Total number of times the cv must categories their technologies rule has run");
    public static readonly Counter CvMustCategoriesTheirTechnologiesFailedCounter = Metrics.CreateCounter("cv_must_categories_their_technologies_failed_total", "Total number of times the cv must categories their technologies rule has failed");
    public static readonly Counter CvMustCategoriesTheirTechnologiesSucceededCounter = Metrics.CreateCounter("cv_must_categories_their_technologies_succeeded_total", "Total number of times the cv must categories their technologies rule has succeeded");
  }
}
