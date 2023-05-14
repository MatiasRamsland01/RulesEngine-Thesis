using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;
using Prometheus;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustBeUpdatedRegularly : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustBeUpdatedRegularly(int days) {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("CV must be updated regularly"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField("The CV must be updated regularly. Please update your CV to ensure it contains the most recent information."),
        FieldsFactory.StringField(@"CustomRules.IsUpdatedWithinDays(input, " + days + ")"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
