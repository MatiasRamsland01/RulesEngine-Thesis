// using Dapr.Client;
// using MediatR;

// namespace Bouvet.Notification.Service.Domain.WorkflowStarted {
//   public class WorkflowStartedEventHandler : INotificationHandler<WorkflowStartedEvent> {
//     private const string DAPR_SERVICE_NAME = "bouvet-rule-engine-service";
//     private readonly ILogger<WorkflowStartedEventHandler> _logger;
//     private readonly DaprClient _daprClient;
//     public WorkflowStartedEventHandler(ILogger<WorkflowStartedEventHandler> logger, DaprClient daprClient) {
//       _logger = logger;
//       _daprClient = daprClient;
//     }
//     public async Task Handle(WorkflowStartedEvent notification, CancellationToken cancellationToken) {
//       _logger.LogCritical("Handling {EventName}, with data {Data}", typeof(WorkflowStartedEvent).Name, notification);
//     }
//   }
// }
