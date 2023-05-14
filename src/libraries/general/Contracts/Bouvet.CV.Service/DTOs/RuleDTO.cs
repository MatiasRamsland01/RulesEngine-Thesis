using fields.Entities.Base.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.Communication.Dapr;
using MediatR;
namespace libraries.Contracts.Bouvet.CV.Service.DTOs {
  public record RuleDTO {
    public BRule Rule { get; set; }
    public RuleDTO(BRule rule) {
      Rule = rule;
    }
  }
}
