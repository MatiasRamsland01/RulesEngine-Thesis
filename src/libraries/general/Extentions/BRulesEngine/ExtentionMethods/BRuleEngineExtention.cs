using Microsoft.AspNetCore.Builder;

namespace libraries.Extentions.BRulesEngine.ExtentionMethods {
  public static class BRuleEngineExtention {
    public static void AddCustomBRulesEngineEntitiesValidators(this WebApplicationBuilder builder) {
      //Adds Fields to IOC with given validator and with config in config file
      builder.Services.AddBRulesEngineEntitiesValidatorsToIOC(builder.Configuration);
    }
  }
}
