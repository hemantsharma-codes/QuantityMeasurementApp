namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents an inches measurement value.
  /// This class is used to compare inch values based on their actual measurement.
  /// </summary>
  public class Inches
  {
    // Stores the numeric value of the inches measurement
    private readonly double _value;

    /// <summary>
    /// Creates a new Inches object with the given value.
    /// </summary>
    /// <param name="value">Measurement value in inches</param>
    public Inches(double value)
    {
      _value = value;
    }

    /// <summary>
    /// Checks whether the current Inches object is equal to another object.
    /// Equality is based on the measurement value, not the memory reference.
    /// </summary>
    /// <param name="obj">Object to compare with</param>
    /// <returns>
    /// True if both objects represent the same inches value; otherwise false.
    /// </returns>
    public override bool Equals(object? obj)
    {
      // If both references point to the same object
      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      // If the object is null or not of Inches type
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }

      // Compare the actual inches values
      Inches other = (Inches)obj;
      return _value == other._value;
    }
  }
}