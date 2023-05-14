using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Commands.ModifyRule {
  public record ModifyRuleCommand(BRule rule) : IRequest<OperationResult<ModifyRuleCommand>>;
}
