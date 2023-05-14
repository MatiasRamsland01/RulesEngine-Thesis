using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustKeyQualificationsWithGivenLength : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustKeyQualificationsWithGivenLength(int minLength) {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("Cv must contain work experience over given length"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField($"Cv must have key qualifications (summary) above {minLength} characters. Please update your CV to ensure it contains the most recent information."),
        FieldsFactory.StringField("CustomRules.HasKeyQualificationsOverGivenLength(input, " + minLength + ")"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
