using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using FluentValidation;
using Microsoft.Extensions.Options;
using fields.Entities.Base.Fields.Net;

namespace fields.Factories {
  public class FieldsFactory {
    #region Fields
    public static IValidator<IIdField<T>> IdValidator<T>() {
      return new IdFieldValidator<T>();
    }
    public static StringField StringField(string value, IValidator<IStringField>? validator = null) {
      validator ??= StringValidator();
      var field = new StringField(value, validator);
      return field;
    }
    public static IntegerField IntegerField(int value, IValidator<IIntegerField>? validator = null) {
      validator ??= IntegerValidator();
      var field = new IntegerField(validator) { Value = value };
      field.DoValidateAndThrow();
      return field;
    }
    public static DateField DateField(string value, IValidator<IDateField>? validator = null) {
      validator ??= DateValidator();
      var field = new DateField(validator) { DateTime = value };
      field.DoValidateAndThrow();
      return field;
    }
    public static IdField<Guid> IdField(Guid value, IValidator<IIdField<Guid>>? validator = null) {
      validator ??= IdValidatorGuid();
      var field = new IdField<Guid>(value, validator);
      // field.DoValidateAndThrow();
      return field;
    }
    public static IdField<string> IdField(string value, IValidator<IIdField<string>>? validator = null) {
      validator ??= IdValidatorString();
      var field = new IdField<string>(value, validator);
      field.DoValidateAndThrow();
      return field;
    }
    public static BoolField BoolField(bool value, IValidator<IBoolField>? validator = null) {
      validator ??= BoolValidator();
      var field = new BoolField(validator) { Value = value };
      return field;
    }
    public static UriField UriField(StringField value, IValidator<IUriField>? validator = null) {
      validator ??= UriValidator();
      var field = new UriField(validator) { UriValue = value };
      // field.DoValidateAndThrow();
      return field;
    }
    public static EmailField EmailField(string value, IValidator<IEmailField>? validator = null) {
      validator ??= EmailValidator();
      var field = new EmailField(value, validator);
      field.DoValidateAndThrow();
      return field;
    }
    #endregion
    #region Validators
    public static IValidator<IStringField> StringValidator(IOptions<StringFieldConfigOption>? config = null) {
      config ??= Options.Create(new StringFieldConfigOption() { Max = 100000, Min = 0 });
      return new StringFieldValidator(config);
    }
    public static IValidator<IIntegerField> IntegerValidator(IOptions<IntegerFieldConfigOption>? config = null) {
      config ??= Options.Create(new IntegerFieldConfigOption() { Max = int.MaxValue, Min = 0 });
      return new IntegerFieldValidator(config);
    }
    public static IValidator<IDateField> DateValidator() {
      return new DateFieldValidator();
    }
    public static IValidator<IIdField<Guid>> IdValidatorGuid() {
      return new IdFieldValidator<Guid>();
    }
    public static IValidator<IIdField<string>> IdValidatorString() {
      return new IdFieldValidator<string>();
    }
    public static IValidator<IBoolField> BoolValidator() {
      return new BoolFieldValidator();
    }
    public static IValidator<IUriField> UriValidator() {
      return new UriFieldValidator();
    }
    public static IValidator<IEmailField> EmailValidator() {
      return new EmailFieldValidator();
    }

    #endregion
  }
}
