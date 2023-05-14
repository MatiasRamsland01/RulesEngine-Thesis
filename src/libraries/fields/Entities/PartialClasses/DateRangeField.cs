using fields.Entities.Base.Fields.Primitive;

namespace fields.Entities.Base.Fields.Dates {
  public partial class DateRangeField {
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => $"Value start: {StartDateField}, Value end: {EndDateField}";
    /// <summary>
    /// Numbers the of overlapping months.
    /// </summary>
    /// <returns>IntegerField.</returns>
    public IntegerField NumberOfOverlappingMonths() {
      return new IntegerField(_integerFieldValidator) { Value = (int)Math.Abs(ToDateTime(StartDateField).Subtract(ToDateTime(EndDateField)).Days / (365.2425 / 12)) };
    }
    /// <summary>
    /// Overlaps the range.
    /// </summary>
    /// <param name="dateRangeField">The date range field.</param>
    /// <returns>DateRangeField.</returns>
    /// <exception cref="System.InvalidOperationException">Ranges do not overlap!</exception>
    public DateRangeField OverlapRange(DateRangeField dateRangeField) {
      return !DoOverLapWithOtherDateRange(dateRangeField)
        ? throw new InvalidOperationException("Ranges do not overlap!")
        : ToDateTime(StartDateField) > ToDateTime(dateRangeField.StartDateField) && ToDateTime(EndDateField) < ToDateTime(dateRangeField.EndDateField)
        ? this
        : ToDateTime(StartDateField) < ToDateTime(dateRangeField.StartDateField) && ToDateTime(EndDateField) > ToDateTime(dateRangeField.EndDateField)
        ? dateRangeField
        : ToDateTime(EndDateField) > ToDateTime(dateRangeField.EndDateField)
        ? new DateRangeField(_validator, _dateFieldValidator, _integerFieldValidator) { StartDateField = StartDateField, EndDateField = dateRangeField.EndDateField }
        : new DateRangeField(_validator, _dateFieldValidator, _integerFieldValidator) { StartDateField = dateRangeField.StartDateField, EndDateField = EndDateField };
    }
    /// <summary>
    /// Numbers the of overlapping months.
    /// </summary>
    /// <param name="dateRangeField">The date range field.</param>
    /// <returns>IntegerField.</returns>
    /// <exception cref="System.InvalidOperationException">Ranges do not overlap!</exception>
    public IntegerField NumberOfOverlappingMonths(DateRangeField dateRangeField) {
      return !DoOverLapWithOtherDateRange(dateRangeField)
        ? throw new InvalidOperationException("Ranges do not overlap!")
        : ToDateTime(StartDateField) > ToDateTime(dateRangeField.StartDateField) && ToDateTime(EndDateField) < ToDateTime(dateRangeField.EndDateField)
        ? new IntegerField(_integerFieldValidator) { Value = (int)Math.Abs(ToDateTime(StartDateField).Subtract(ToDateTime(EndDateField)).Days / (365.2425 / 12)) }
        : ToDateTime(StartDateField) < ToDateTime(dateRangeField.StartDateField) && ToDateTime(EndDateField) > ToDateTime(dateRangeField.EndDateField)
        ? new IntegerField(_integerFieldValidator) { Value = (int)Math.Abs(ToDateTime(dateRangeField.StartDateField).Subtract(ToDateTime(dateRangeField.EndDateField)).Days / (365.2425 / 12)) }
        : ToDateTime(EndDateField) > ToDateTime(dateRangeField.EndDateField)
        ? new IntegerField(_integerFieldValidator) { Value = (int)Math.Abs(ToDateTime(StartDateField).Subtract(ToDateTime(dateRangeField.EndDateField)).Days / (365.2425 / 12)) }
        : new IntegerField(_integerFieldValidator) { Value = (int)Math.Abs(ToDateTime(dateRangeField.StartDateField).Subtract(ToDateTime(EndDateField)).Days / (365.2425 / 12)) };
    }
    /// <summary>
    /// Converts to datetime.
    /// </summary>
    /// <param name="dateField">The date field.</param>
    /// <returns>DateTime.</returns>
    private DateTime ToDateTime(IDateField dateField) {
      return DateTime.Parse(dateField.DateTime);
    }
    /// <summary>
    /// Does the over lap with other date range.
    /// </summary>
    /// <param name="dateRangeField">The date range field.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool DoOverLapWithOtherDateRange(DateRangeField dateRangeField) {
      return ToDateTime(StartDateField) <= ToDateTime(dateRangeField.EndDateField)
            && ToDateTime(dateRangeField.StartDateField) <= ToDateTime(EndDateField);
    }
  }
}
