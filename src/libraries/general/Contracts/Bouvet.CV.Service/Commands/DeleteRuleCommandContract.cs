using fields.Entities.Base.Fields.Primitive;

namespace libraries.Contracts.Bouvet.Rule.Engine.Service {

  public record DeleteRuleCommandContract {
    public StringField RuleName { get; set; }
    public DeleteRuleCommandContract(StringField ruleName) {
      RuleName = ruleName;
    }
  }
}
