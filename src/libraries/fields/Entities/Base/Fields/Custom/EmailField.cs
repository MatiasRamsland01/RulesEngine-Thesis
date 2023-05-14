using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Base;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace fields.Entities.Base.Fields.Custom {
  public interface IEmailField : IField {
    string Value { get; set; }
  }

  public partial class EmailField : IEmailField {
    private readonly IValidator<IEmailField> _validator = FieldsFactory.EmailValidator();
    public string Value { get; set; } = string.Empty;

    public EmailField(string value, IValidator<IEmailField> validator) {
      Value = value;
      _validator = validator ?? FieldsFactory.EmailValidator();
    }

    public EmailField() {
    }

    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }

    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
    public override string ToString() => Value;
  }
}
