using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.CV.Service.Domain.Commands.AddRule {
  public record AddRuleCommand(BRule rule) : IRequest<OperationResult<AddRuleCommand>>;
}
