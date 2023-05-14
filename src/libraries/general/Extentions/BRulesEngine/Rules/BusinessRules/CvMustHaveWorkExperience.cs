using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustHaveWorkExperience : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustHaveWorkExperience() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("Cv must contain work experience"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField("Cv must have work experience. Please update your CV to ensure it contains the most recent information."),
        FieldsFactory.StringField("CustomRules.HasWorkExperience(input)"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
