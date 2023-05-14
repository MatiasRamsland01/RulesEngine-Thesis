using fields.Entities.Base.Fields;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;
using libraries.Extentions.BRulesEngine.Rules.Core.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace libraries.Extentions.BRulesEngine.ExtentionMethods {
  /// <summary>
  /// Class FieldsConfigExtensions.
  /// </summary>
  public static class RuleEngineEntityExtensions {
    /// <summary>
    /// Adds the limit field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddBRulesEngineEntitiesValidatorsToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddTransient<IValidator<IRuleDataField>, RuleDataFieldValidator>();
      services.AddTransient<IValidator<IBRule>, BRuleValidator>();
      services.AddTransient<IValidator<IInputDataField>, InputDataFieldValidator>();
      services.AddTransient<IValidator<IBInputs>, BInputsValidator>();
      services.AddTransient<IValidator<IBWorkflow>, BWorkflowValidator>();
      return services;
    }
  }
}
