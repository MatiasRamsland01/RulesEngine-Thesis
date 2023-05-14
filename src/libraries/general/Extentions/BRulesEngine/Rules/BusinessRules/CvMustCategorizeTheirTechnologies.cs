using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustContainOnlyCategorizedTechnologies : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustContainOnlyCategorizedTechnologies() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("Technologies must not be uncategorized"),
          FieldsFactory.StringField("SuccessEvent-None"),
          FieldsFactory.StringField("Technologies must be categorized and present in the CV. Please update your CV to ensure it contains the most recent information."),
          FieldsFactory.StringField("CustomRules.AreTechnologiesNotUncategorized(input)"),
          FieldsFactory.StringField("AND"),
          FieldsFactory.BoolField(true),
          FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
