namespace fields.FluentValidation.Rules {
  public static class TryParse {
    /// <summary>
    /// Converts to datetime.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool ToDateTime(string date)
  => DateTime.TryParse(date, out _);
  }
}
