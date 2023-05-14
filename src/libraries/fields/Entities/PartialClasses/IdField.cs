namespace fields.Entities.Base.Fields.Custom {
  public partial class IdField<T> : IIdField<T> {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() {
      return $"Id:{Value}";
    }
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode() {
      return System.HashCode.Combine(Value, 500).GetHashCode(); // TODO
    }
  }
}
