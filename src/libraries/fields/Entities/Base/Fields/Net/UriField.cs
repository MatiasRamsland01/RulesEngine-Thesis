using fields.Entities.Base.Fields.Base;
using fields.Entities.Base.Fields.Primitive;
using FluentValidation;
using FluentValidation.Results;
using fields.FluentValidation.Rules;
using fields.Factories;

namespace fields.Entities.Base.Fields.Net {
  public interface IUriField : IField, IAllowedType {
    StringField UriValue { get; set; }
    CommunicationComponent? PathType { get; }
  }

  public partial class UriField : IUriField {
    private readonly IValidator<IUriField> _validator = FieldsFactory.UriValidator();
    private StringField _value = FieldsFactory.StringField(string.Empty);
    private CommunicationComponent? _pathType;

    public StringField UriValue {
      get { return _value; }
      set {
        _value = value;
        PathType = new UrlPathDetector().FindType(UriValue);
      }
    }
    public CommunicationComponent? PathType {
      get { return _pathType; }
      set { _pathType = value; }
    }
    public UriField() { }

    public UriField(IValidator<IUriField> validator) {
      _validator = validator;
    }

    private IStringField HttpPathType(IStringField uri) {

      CommunicationComponent type = new UrlPathDetector().FindType(uri);
      bool? isInEnumSet = Enum.IsDefined<CommunicationComponent>(type);
      var name = Enum.GetName<CommunicationComponent>(type);

      if (isInEnumSet.Value && name is not null) {
        return FieldsFactory.StringField(name);
      }
      return FieldsFactory.StringField("Not a valid path");
    }

    public override bool Equals(object? other) {
      IUriField? otherField = other as IUriField;
      if (otherField is not null) {
        if (otherField.UriValue.Value == this.UriValue.Value)
          return true;
      }
      return false;
    }

    public override int GetHashCode() {
      return HashCode.Combine(UriValue, PathType);
    }

    public static UriField operator +(UriField a, UriField b)
       => FieldsFactory.UriField(a.UriValue + b.UriValue);


    public ValidationResult DoValidate() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets());
    }

    public ValidationResult DoValidateAndThrow() {
      return _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    }
  }
}