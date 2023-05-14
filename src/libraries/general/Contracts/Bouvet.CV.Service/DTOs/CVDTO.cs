using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
namespace libraries.Contracts.Bouvet.CV.Service.DTOs {
  public record CVDTO {
    public Response CV { get; set; }
    public CVDTO(Response cv) {
      CV = cv;
    }
  }
}
