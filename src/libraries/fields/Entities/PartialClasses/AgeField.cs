namespace fields.Entities.Base.Fields.Custom {
  public partial class AgeField : IAgeField {
    /// <inheritdoc/>
    public override string ToString() {
      return Age.ToString();
    }
  }
}
