using fields.Entities.Base.Fields.Custom;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Queries.GetCollectionOfCVs {
  public record GetCollectionOfCVsQuery : IRequest<OperationResult<CollectionOfCVsDTO>> {
    public EmailField Email { get; set; }
    public GetCollectionOfCVsQuery(EmailField email) {
      Email = email;
    }
  }
}
