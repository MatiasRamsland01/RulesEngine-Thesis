using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  public class CvMustHaveProfilePicture : IBouvetRule {
    public IdField<Guid> Id { get; set; }
    public BRule Rule { get; set; }
    public CvMustHaveProfilePicture() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(
          FieldsFactory.StringField("CV must have a profile picture"),
          FieldsFactory.StringField("SuccessEvent-None"),
          FieldsFactory.StringField("Profile picture is missing. Please update your CV to ensure it contains the most recent information."),
          FieldsFactory.StringField("CustomRules.HasProfilePicture(input.image)"),
          FieldsFactory.StringField("AND"),
          FieldsFactory.BoolField(true),
          FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
