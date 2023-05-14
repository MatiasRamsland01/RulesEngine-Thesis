using fields.Entities.Base.Fields.Custom;
using FluentValidation;

namespace Bouvet.CV.Service.Domain.Queries.GetCV {
  public class GetCVQueryValidator : AbstractValidator<GetCVQuery> {
    public GetCVQueryValidator(IValidator<IEmailField> emailValidator) {
      RuleFor(x => x.Email).SetValidator(emailValidator);
    }
  }
}
