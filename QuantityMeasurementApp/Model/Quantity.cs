using QuantityMeasurementApp.BusinessLayer;

namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents a measurable quantity with value and unit.
  /// </summary>
  public class Quantity
  {
    public double Value { get; }
    public QuantityType Type { get; }

    public Quantity(double value, QuantityType type)
    {
      Value = value;
      Type = type;
    }

    /// <summary>
    /// Converts the quantity to the base unit (Inches).
    /// </summary>
    public double ConvertToBase()
    {
      return Value * QuantityConverter.GetConversionFactor(Type);
    }

    /// <summary>
    /// Checks equality by comparing values in base unit.
    /// </summary>
    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }

      Quantity other = (Quantity)obj;

      // Tolerance-based comparison for double values
      return Math.Abs(this.ConvertToBase() - other.ConvertToBase()) < 0.0001;
    }

    /// <summary>
    /// Returns hash code based on base unit value.
    /// </summary>
    public override int GetHashCode()
    {
      return ConvertToBase().GetHashCode();
    }
  }
}