﻿namespace fields.Entities.Base.Fields.Custom {
  public partial class LimitField {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() {
      return $"Value:{Limit}";
    }
  }
}
