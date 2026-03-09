using QuantityMeasurementApp.BusinessLayer;

namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents a measurable quantity with its numeric value and unit type.
  /// </summary>
  public class Quantity
  {
    /// <summary>
    /// Actual numeric value of the quantity.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Unit type of the quantity (Feet, Inches, etc.).
    /// </summary>
    public QuantityType Type { get; }

    /// <summary>
    /// Creates a quantity with value and unit type.
    /// </summary>
    public Quantity(double value, QuantityType type)
    {
      Value = value;
      Type = type;
    }

    /// <summary>
    /// Converts the current quantity to the specified target unit.
    /// </summary>
    public double ConvertTo(QuantityType targetUnit)
    {
      // Convert the current value to base unit first
      double baseSourceValue = QuantityConverter.ConvertToBase(this.Type, this.Value);

      // Convert base value to target unit
      return QuantityConverter.ConvertFromBase(targetUnit, baseSourceValue);
    }

    /// <summary>
    /// Adds another quantity to the current one and returns
    /// the result in the unit of the current object.
    /// </summary>
    public Quantity Add(Quantity other)
    {
      if (other == null)
      {
        throw new ArgumentNullException("The other quantity can't be null");
      }

      return AddAndConvert(other, this.Type);
    }

    /// <summary>
    /// Adds another quantity and returns the result in the specified unit.
    /// </summary>
    public Quantity Add(Quantity quantity, QuantityType targetUnit)
    {
      if (quantity == null)
      {
        throw new ArgumentNullException("quantity can't be null");
      }

      return AddAndConvert(quantity, targetUnit);
    }

    /// <summary>
    /// Internal helper method that performs addition using base unit conversion.
    /// </summary>
    private Quantity AddAndConvert(Quantity q, QuantityType targetUnit)
    {
      // Convert both quantities to base unit (inches)
      double firstValueInBase = QuantityConverter.ConvertToBase(this.Type, this.Value);
      double secondValueInBase = QuantityConverter.ConvertToBase(q.Type, q.Value);

      // Add values in base unit
      double sumInBase = firstValueInBase + secondValueInBase;

      // Convert result to requested unit
      double finalSum = QuantityConverter.ConvertFromBase(targetUnit, sumInBase);

      return new Quantity(finalSum, targetUnit);
    }

    /// <summary>
    /// Compares two quantities by converting them to base unit.
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

      // Compare values after converting both to base unit
      return Math.Abs(
        QuantityConverter.ConvertToBase(this.Type, this.Value) -
        QuantityConverter.ConvertToBase(other.Type, other.Value)
      ) < 0.0001;
    }

    /// <summary>
    /// Generates hash code using value converted to base unit.
    /// </summary>
    public override int GetHashCode()
    {
      return QuantityConverter.ConvertToBase(this.Type, this.Value).GetHashCode();
    }

    /// <summary>
    /// Returns a readable string representation of the quantity.
    /// </summary>
    public override string ToString()
    {
      return $"Value : {Value}\t Quantity Type : {Type}";
    }
  }
}