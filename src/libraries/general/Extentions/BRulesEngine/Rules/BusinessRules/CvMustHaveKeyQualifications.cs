using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustHaveKeyQualifications : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustHaveKeyQualifications() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("CV must have key qualifications"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField("The CV must have key qualifications. Please update your CV to ensure it contains the most recent information."),
        FieldsFactory.StringField(@"CustomRules.HasKeyQualifications(input)"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
