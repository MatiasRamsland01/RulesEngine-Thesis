namespace fields.Entities.Base.Fields.Dates {
  public partial class DateField : IDateField {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => $"Value: {DateTime}";
  }
}
