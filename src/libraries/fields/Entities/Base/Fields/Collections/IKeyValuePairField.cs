using fields.Entities.Base.Fields.Primitive;

namespace fields.Entities.Base.Fields.Collections {
  public interface IKeyValuePairField<StringField, T> {

    StringField Key { get; }

    T? Value { get; }
  }
}