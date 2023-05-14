using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using fields.Entities.HttpServices.CVPartnerService.Configuration;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using fields.Entities.Base.Fields.Net;
using fields.Entities.Base.Fields;

namespace fields.Extensions {
  /// <summary>
  /// Class FieldsConfigExtensions.
  /// </summary>
  public static class FieldsConfigExtensions {
    /// <summary>
    /// Adds the limit field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddLimitFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<LimitConfigOption>, LimitConfigValidator>();
      services.Configure<LimitConfigOption>(
      configuration.GetSection(LimitConfigOption.LimitConfig))
        .AddOptions<LimitConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<LimitConfigOption>>().Validate(config).IsValid;
        })
        .ValidateOnStart();
      services.AddScoped<IValidator<ILimitField>, LimitFieldValidator>();
      services.AddScoped<ILimitField, LimitField>();
      return services;
    }
    /// <summary>
    /// Adds the string field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddStringFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<StringFieldConfigOption>, StringFieldConfigValidator>();
      services.Configure<StringFieldConfigOption>(
      configuration.GetSection(StringFieldConfigOption.StringFieldConfig))
        .AddOptions<StringFieldConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<StringFieldConfigOption>>().Validate(config).IsValid;
        })
        .ValidateOnStart();
      services.AddScoped<IValidator<IStringField>, StringFieldValidator>();
      services.AddScoped<IStringField, StringField>();
      return services;
    }
    /// <summary>
    /// Adds the integer field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddIntegerFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IntegerFieldConfigOption>, IntegerFieldConfigValidator>();
      services.Configure<IntegerFieldConfigOption>(
      configuration.GetSection(IntegerFieldConfigOption.IntegerFieldConfig))
        .AddOptions<IntegerFieldConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<IntegerFieldConfigOption>>().Validate(config).IsValid;
        })
        .ValidateOnStart();
      services.AddScoped<IValidator<IIntegerField>, IntegerFieldValidator>();
      services.AddScoped<IIntegerField, IntegerField>();
      return services;
    }
    /// <summary>
    /// Adds the bool field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddBoolFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IBoolField>, BoolFieldValidator>();
      services.AddScoped<IBoolField, BoolField>();
      return services;
    }

    /// <summary>
    /// Adds the character field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddCharFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<ICharField>, CharFieldValidator>();
      services.AddScoped<ICharField, CharField>();
      return services;
    }
    /// <summary>
    /// Adds the date field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddDateFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IDateField>, DateFieldValidator>();
      services.AddScoped<IDateField, DateField>();
      return services;
    }
    /// <summary>
    /// Adds the date range field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddDateRangeFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IDateRangeField>, DateRangeFieldValidator>();
      services.AddScoped<IDateRangeField, DateRangeField>();
      return services;
    }
    /// <summary>
    /// Adds the identifier field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddIdFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IIdField<Guid>>, IdFieldValidator<Guid>>();
      services.AddScoped<IIdField<Guid>, IdField<Guid>>();
      return services;
    }

    public static IServiceCollection AddUriFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IUriField>, UriFieldValidator>();
      services.AddScoped<IUriField, UriField>();
      return services;
    }

    public static IServiceCollection AddEmailFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<IEmailField>, EmailFieldValidator>();
      services.AddScoped<IEmailField, EmailField>();
      return services;
    }
    /// <summary>
    /// Adds the name field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddNameFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<NameFieldConfigOption>, NameFieldConfigValidator>();
      services.Configure<NameFieldConfigOption>(
      configuration.GetSection(NameFieldConfigOption.NameFieldConfig))
        .AddOptions<NameFieldConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<NameFieldConfigOption>>().Validate(config).IsValid;
        })
        .ValidateOnStart();
      services.AddScoped<IValidator<INameField>, NameFieldValidator>();
      services.AddScoped<INameField, NameField>();
      return services;
    }
    /// <summary>
    /// Adds the age field to ioc.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddAgeFieldToIOC(this IServiceCollection services, IConfiguration configuration) {
      services.AddScoped<IValidator<AgeFieldConfigOption>, AgeFieldConfigValidator>();
      services.Configure<AgeFieldConfigOption>(
      configuration.GetSection(AgeFieldConfigOption.AgeFieldConfig))
        .AddOptions<AgeFieldConfigOption>()
        .Validate(config => {
          return services.BuildServiceProvider().GetRequiredService<IValidator<AgeFieldConfigOption>>().Validate(config).IsValid;
        })
        .ValidateOnStart();
      services.AddScoped<IValidator<IAgeField>, AgeFieldValidator>();
      services.AddScoped<IAgeField, AgeField>();
      return services;
    }
  }
}
