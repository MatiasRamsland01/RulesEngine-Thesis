using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
namespace libraries.Contracts.Bouvet.CV.Service.DTOs {
  public record CollectionOfCVsDTO {
    public ICollection<Response> CVs { get; set; }
    public CollectionOfCVsDTO(ICollection<Response> cvs) {
      CVs = cvs;
    }
  }
}
