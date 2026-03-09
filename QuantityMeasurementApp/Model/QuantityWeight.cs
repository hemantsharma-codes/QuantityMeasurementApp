namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents a weight value along with its unit.
  /// Provides operations such as unit conversion, addition and comparison.
  /// </summary>
  public class QuantityWeight
  {
    /// <summary>
    /// Gets the numeric value of the weight.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Gets the unit associated with the weight value.
    /// </summary>
    public WeightUnit Unit { get; }

    /// <summary>
    /// Initializes a new instance of QuantityWeight with the specified value and unit.
    /// </summary>
    public QuantityWeight(double value, WeightUnit unit)
    {
      Value = value;
      Unit = unit;
    }

    /// <summary>
    /// Converts the current weight value into the specified target unit.
    /// </summary>
    /// <param name="targetUnit">Unit to which the value should be converted.</param>
    /// <returns>The converted value.</returns>
    public double ConvertTo(WeightUnit targetUnit)
    {
      // Convert the current value to base unit (kilograms)
      double baseValue = WeightUnitConverter.ConvertToBase(this.Unit, this.Value);

      // Convert base value to the requested target unit
      double resultValue = WeightUnitConverter.ConvertFromBase(targetUnit, baseValue);

      return resultValue;
    }

    /// <summary>
    /// Adds another weight to the current weight and returns the result
    /// in the unit of the current object.
    /// </summary>
    public QuantityWeight Add(QuantityWeight other)
    {
      if (other == null)
      {
        throw new ArgumentNullException("The other can't be null");
      }

      return AddAndConvert(other, this.Unit);
    }

    /// <summary>
    /// Adds another weight and returns the result converted to the specified unit.
    /// </summary>
    public QuantityWeight Add(QuantityWeight weight, WeightUnit targetUnit)
    {
      if (weight == null)
      {
        throw new ArgumentNullException("The weight can't be null");
      }

      return AddAndConvert(weight, targetUnit);
    }

    /// <summary>
    /// Performs addition by converting both weights to the base unit
    /// and then converting the result to the requested unit.
    /// </summary>
    private QuantityWeight AddAndConvert(QuantityWeight weight, WeightUnit targetUnit)
    {
      // Convert both quantities to base unit (kilograms)
      double firstValueInBase = WeightUnitConverter.ConvertToBase(this.Unit, this.Value);
      double secondValueInBase = WeightUnitConverter.ConvertToBase(weight.Unit, weight.Value);

      // Add values in base unit
      double sumInBase = firstValueInBase + secondValueInBase;

      // Convert result to target unit
      double finalSum = WeightUnitConverter.ConvertFromBase(targetUnit, sumInBase);

      return new QuantityWeight(finalSum, targetUnit);
    }

    /// <summary>
    /// Checks whether two weight objects represent the same value.
    /// Comparison is performed using the base unit.
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

      QuantityWeight weight = (QuantityWeight)obj;

      return Math.Abs(
        WeightUnitConverter.ConvertToBase(this.Unit, this.Value) -
        WeightUnitConverter.ConvertToBase(weight.Unit, weight.Value)
      ) < 0.001;
    }

    /// <summary>
    /// Generates a hash code based on the base unit value.
    /// </summary>
    public override int GetHashCode()
    {
      return WeightUnitConverter.ConvertToBase(this.Unit, this.Value).GetHashCode();
    }

    /// <summary>
    /// Returns a readable representation of the weight value and its unit.
    /// </summary>
    public override string ToString()
    {
      return $"Value : {Value}\tUnit: {Unit}";
    }
  }
}