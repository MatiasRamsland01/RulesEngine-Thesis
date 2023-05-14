namespace fields.Entities.Base.Fields.Primitive {
  public partial class CharField : ICharField {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => $"Value: {Value}";
  }
}
