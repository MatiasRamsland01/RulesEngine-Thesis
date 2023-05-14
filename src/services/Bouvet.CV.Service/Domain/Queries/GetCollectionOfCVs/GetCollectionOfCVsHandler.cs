using Dapr.Client;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Queries.GetCollectionOfCVs {
  public class GetCollectionOfCVsControllerHandler : IRequestHandler<GetCollectionOfCVsQuery, OperationResult<CollectionOfCVsDTO>> {
    private const string DAPR_STATESTORE_CV = "ruleengine-statestore-cv";
    private readonly ILogger<GetCollectionOfCVsControllerHandler> _logger;
    private readonly DaprClient _dapr;
    public GetCollectionOfCVsControllerHandler(ILogger<GetCollectionOfCVsControllerHandler> logger, DaprClient dapr) {
      _logger = logger;
      _dapr = dapr;
    }
    public async Task<OperationResult<CollectionOfCVsDTO>> Handle(GetCollectionOfCVsQuery query, CancellationToken cancellationToken) {
      var cvList = await _dapr.GetStateEntryAsync<List<Response>>(DAPR_STATESTORE_CV, query.Email.Value);
      if (cvList == null || cvList.Value == null || cvList.Value.Count == 0) {
        throw new ArgumentException($"CV not found for email {query.Email.Value}");
      }
      return OperationResult<CollectionOfCVsDTO>.CreateSuccess(new CollectionOfCVsDTO(cvList.Value), $"CV fetched successfully", 200);
    }
  }
}
