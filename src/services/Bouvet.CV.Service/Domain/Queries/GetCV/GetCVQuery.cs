using fields.Entities.Base.Fields.Custom;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Queries.GetCV {
  public record GetCVQuery : IRequest<OperationResult<CVDTO>> {
    public EmailField Email { get; set; }
    public GetCVQuery(EmailField email) {
      Email = email;
    }
  }
}
