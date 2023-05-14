using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Primitive;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Commands.DeleteRule {
  public record DeleteRuleCommand(StringField ruleName) : IRequest<OperationResult<DeleteRuleCommand>>;
}
