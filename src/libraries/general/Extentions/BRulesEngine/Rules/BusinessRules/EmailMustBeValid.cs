using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class EmailMustBeValid : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public EmailMustBeValid() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("Email must be valid"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField("Email is not in valid format. Please update your CV to ensure it contains the most recent information."),
        FieldsFactory.StringField("CustomRules.IsEmailValid(input)"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
