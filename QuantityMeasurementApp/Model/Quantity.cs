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
    /// Converts value from source unit to target unit
    /// </summary>
    public double ConvertTo(QuantityType targetUnit)
    {
      double baseSourceUnit = this.Value * QuantityConverter.GetConversionFactor(this.Type);
      return baseSourceUnit / QuantityConverter.GetConversionFactor(targetUnit);
    }


    /// <summary>
    /// Converts the quantity to the base unit (Inches).
    /// </summary>
    public double ConvertToBase()
    {
      return Value * QuantityConverter.GetConversionFactor(Type);
    }

    /// <summary>
    /// Adds the given quantity to the current quantity and returns the result
    /// in the unit type of the current object.
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
    /// Adds the given quantity to the current quantity and returns the result
    /// converted to the specified target unit.
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
    /// private utility method to perform addition conversion on base unit value.
    /// </summary>
    private Quantity AddAndConvert(Quantity q, QuantityType targetUnit)
    {
      // Convert both quantities to the base unit for accurate addition
      double firstValueInBase = this.Value * QuantityConverter.GetConversionFactor(this.Type);
      double secondValueInBase = q.Value * QuantityConverter.GetConversionFactor(q.Type);

      // Add the values in base unit
      double sumInBase = firstValueInBase + secondValueInBase;

      // Convert the result back to the unit type of target Unit
      double finalSum = sumInBase / QuantityConverter.GetConversionFactor(targetUnit);

      // Return the resulting quantity
      return new Quantity(finalSum, targetUnit);
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

    public override string ToString()
    {
      return $"Value : {Value}\t Quantity Type : {Type}";
    }
  }
}