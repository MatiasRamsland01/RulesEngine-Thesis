using FluentValidation;
namespace fields.Entities.Base.Fields.Primitive {
  public partial class IntegerField {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() {
      return $"Value:{Value}";
    }
    /// <summary>
    /// Creates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>IIntegerField.</returns>
    public IIntegerField Create(int value) {
      Value = value;
      _ = _validator.ValidateAndThrowAsync(this);
      return this;
    }
  }
}
