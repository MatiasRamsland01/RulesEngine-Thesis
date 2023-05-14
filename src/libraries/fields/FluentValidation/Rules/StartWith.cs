namespace fields.FluentValidation.Rules {
  public static class StartWith {
    /// <summary>
    /// Starts the with upper case.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool StartWithUpperCase(string value)
  => char.IsUpper(value[0]);
  }
}
