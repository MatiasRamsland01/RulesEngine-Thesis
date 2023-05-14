using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using fields.Entities.Base.Fields.Primitive;

namespace Bouvet.CV.Service.Domain.Queries.GetRule {
  public record GetRuleQuery(StringField ruleName) : IRequest<OperationResult<RuleDTO>>;
}
