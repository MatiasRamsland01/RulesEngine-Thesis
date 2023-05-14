using System.Text.RegularExpressions;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Engine {
  public static class CustomRules {
    public static bool HasKeyQualificationsOverGivenLength(dynamic input, int minLength) {
      IEnumerable<dynamic> keyQualifications = input.key_qualifications;
      if (keyQualifications == null) {
        return false;
      }
      if (!keyQualifications.Any()) {
        return false;
      }
      foreach (dynamic item in keyQualifications) {
        dynamic longDescription = item.long_description;
        string descriptionNo = longDescription.no ?? string.Empty;
        string descriptionInt = longDescription.@int ?? string.Empty;
        string descriptionSe = longDescription.se ?? string.Empty;

        bool validNo = descriptionNo != string.Empty && descriptionNo.Length >= minLength;
        bool validInt = descriptionInt != string.Empty && descriptionInt.Length >= minLength;
        bool validSe = descriptionSe != string.Empty && descriptionSe.Length >= minLength;

        if (validNo || validInt || validSe) {
          return true;
        }
      }
      return false;
    }
    public static bool HasKeyQualifications(dynamic input) {
      var keyQualifications = input.key_qualifications as IEnumerable<dynamic>;
      if (keyQualifications == null) {
        return false;
      }
      if (!keyQualifications.Any()) {
        return false;
      }
      foreach (dynamic item in keyQualifications) {
        var longDescription = item.long_description as dynamic;

        bool validNo = longDescription.no != string.Empty || longDescription.no != null;
        bool validInt = longDescription.@int != string.Empty || longDescription.no != null;
        bool validSe = longDescription.se != string.Empty || longDescription.no != null;

        if (validNo || validInt || validSe) {
          return true;
        }
      }
      return false;
    }
    public static bool AreTechnologiesNotUncategorized(dynamic input) {
      var technologies = input.technologies as IEnumerable<dynamic>;
      if (technologies == null) {
        return false;
      }
      if (!technologies.Any()) {
        return false;
      }
      foreach (dynamic tech in technologies) {
        var technology_skills = tech.technology_skills as IEnumerable<dynamic>;
        var technology_uncategorized = tech.uncategorized;
        if (technology_skills == null || technology_uncategorized == null) {
          continue;
        }
        if (technology_uncategorized == true && technology_skills!.Cast<dynamic>().Any()) {
          return false;
        }
      }
      return true;
    }
    public static bool HasWorkExperience(dynamic workExperiences) {
      var experience = workExperiences.work_experiences as IEnumerable<dynamic>;

      if (experience == null || !experience.Any()) {
        return false;
      }
      return true;
    }
    public static bool IsEmployeeNameValid(dynamic input) {
      string name = input.name;
      if (name == null) {
        return false;
      }
      string namePattern = @"^(?:[A-Za-zÀ-ÖØ-öø-ÿĀ-žÅÆØåæøÄäÖöÜüÁáÉé'-]+(?:\.|\b)\s*)+(?:[A-Za-zÀ-ÖØ-öø-ÿĀ-žÅÆØåæøÄäÖöÜüÁáÉé'-]+\.?)*$";
      return Regex.IsMatch(name, namePattern);
    }
    public static bool IsEmailValid(dynamic input) {
      string email = input.email;
      var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
      return Regex.IsMatch(Convert.ToString(email), emailRegex);
    }
    public static bool IsUpdatedWithinDays(dynamic input, int days) {
      var ownerUpdatedAt = input.owner_updated_at;
      var createdAt = input.created_at;
      if (ownerUpdatedAt == null && createdAt == null) {
        return true;
      }
      if (ownerUpdatedAt == null && createdAt < DateTime.Now.AddDays(-days)) {
        return false;
      }
      return ownerUpdatedAt > DateTime.Now.AddDays(-days);
    }
    public static bool HasProfilePicture(dynamic input) {
      return input.url == "image";
    }
  }

}
