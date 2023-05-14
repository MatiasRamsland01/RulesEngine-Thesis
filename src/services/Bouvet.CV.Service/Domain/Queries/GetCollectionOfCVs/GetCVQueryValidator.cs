using fields.Entities.Base.Fields.Custom;
using FluentValidation;

namespace Bouvet.CV.Service.Domain.Queries.GetCollectionOfCVs {
  public class GetCVQueryValidator : AbstractValidator<GetCollectionOfCVsQuery> {
    public GetCVQueryValidator(IValidator<IEmailField> emailValidator) {
      RuleFor(x => x.Email).SetValidator(emailValidator);
    }
  }
}
