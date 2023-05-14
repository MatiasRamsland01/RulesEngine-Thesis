using Dapr.Client;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Queries.GetCV {
  public class GetCVHandler : IRequestHandler<GetCVQuery, OperationResult<CVDTO>> {
    private const string DAPR_STATESTORE_CV = "ruleengine-statestore-cv";

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetCVHandler> _logger;
    private readonly DaprClient _dapr;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRuleHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="db"></param>
    public GetCVHandler(ILogger<GetCVHandler> logger, DaprClient dapr) {
      _logger = logger;
      _dapr = dapr;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<CVDTO>> Handle(GetCVQuery query, CancellationToken cancellationToken) {
      var cvList = await _dapr.GetStateEntryAsync<List<Response>>(DAPR_STATESTORE_CV, query.Email.Value);
      if (cvList == null || cvList.Value == null || cvList.Value.Count == 0) {
        throw new ArgumentException($"CV not found for email {query.Email.Value}");
      }
      var cv = cvList.Value.Last();
      _logger.LogCritical($"Found CV for email {query.Email.Value}");
      return OperationResult<CVDTO>.CreateSuccess(new CVDTO(cv), $"CV fetched successfully", 200);

    }
  }
}
