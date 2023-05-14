using Prometheus;

namespace Bouvet.Notification.Service.Statistic {
  public static class EmployeeNotifcationMetric {
    public static readonly Counter NotifyEmployeeCounter = Metrics.CreateCounter("notify_employee_total", "Total number of times the employee has been notified");
    public static readonly Counter TotalNumberOfUnsatifiedCVs = Metrics.CreateCounter("total_number_of_unsatified_cvs", "Total number of unsatified CVs");
    public static readonly Counter TotalNumberOfSatifiedCVs = Metrics.CreateCounter("total_number_of_satified_cvs", "Total number of satified CVs");
  }
}
