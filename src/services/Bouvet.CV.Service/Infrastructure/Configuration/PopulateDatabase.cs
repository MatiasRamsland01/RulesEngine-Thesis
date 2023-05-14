using Bouvet.CV.Service.Infrastructure;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.BusinessRules;
using libraries.Extentions.BRulesEngine.Rules.Core;
namespace Bouvet.CV.Service.Infrastructure.Configuration;
public static class PopulateDatabase {
  public static void WithRules(RuleengineContext db) {
    var cvMustHaveProfilePicture = new CvMustHaveProfilePicture();
    var cvMustBeUpdatedRegularly = new CvMustBeUpdatedRegularly(90);
    var cvMustContainAValidName = new CvMustContainAValidName();
    var emailMustBeValid = new EmailMustBeValid();
    var cvMustContainOnlyCategorizedTechnologies = new CvMustContainOnlyCategorizedTechnologies();
    var cvMustHaveKeyQualifications = new CvMustHaveKeyQualifications();
    var cvMustHaveWorkExperience = new CvMustHaveWorkExperience();
    var cvMustKeyQualificationsWithGivenLength = new CvMustKeyQualificationsWithGivenLength(500);
    var workflow = new BWorkflow(new List<BRule> { cvMustHaveProfilePicture.Rule, cvMustBeUpdatedRegularly.Rule, cvMustContainAValidName.Rule, emailMustBeValid.Rule, cvMustHaveWorkExperience.Rule, cvMustHaveKeyQualifications.Rule, cvMustContainOnlyCategorizedTechnologies.Rule, cvMustKeyQualificationsWithGivenLength.Rule }, FieldsFactory.StringField("rulesEngine"));
    db.Database.EnsureCreated();
    db.Add(workflow);
    db.SaveChanges();
  }
}
