namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents a feet measurement value.
  /// </summary>
  public class Feet
  {
    // Stores the numeric value of the feet measurement
    private readonly double _value;

    /// <summary>
    /// Creates a new feet measurement with the given value.
    /// </summary>
    /// <param name="value">Measurement value in feet</param>
    public Feet(double value)
    {
      _value = value;
    }

    /// <summary>
    /// Checks whether the current feet object is equal to another object.
    /// </summary>
    /// <param name="feet">Object to compare with</param>
    /// <returns>
    /// True if both objects represent the same feet value; otherwise false.
    /// </returns>
    public override bool Equals(object? feet)
    {
      // If both references point to the same object
      if (ReferenceEquals(this, feet))
      {
        return true;
      }

      // If object is null or not of Feet type
      if (feet == null || GetType() != feet.GetType())
      {
        return false;
      }

      // Compare the actual feet values
      Feet other = (Feet)feet;
      return _value == other._value;
    }

    /// <summary>
    /// Returns the hash code for the current object.
    /// </summary>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
} 