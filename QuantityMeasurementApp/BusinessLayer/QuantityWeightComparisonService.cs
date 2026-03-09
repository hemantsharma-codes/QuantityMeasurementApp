using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.BusinessLayer
{
  /// <summary>
  /// Provides operations for comparing, converting, and adding weight quantities.
  /// Acts as the business layer for weight-related functionality.
  /// </summary>
  public class QuantityWeightComparisonService
  {
    /// <summary>
    /// Compares two weight quantities and checks if they represent the same value.
    /// </summary>
    public bool Compare(QuantityWeight weight1, QuantityWeight weight2)
    {
      return weight1.Equals(weight2);
    }

    /// <summary>
    /// Converts a weight value from one unit to another.
    /// </summary>
    public double DemonstrateQuantityConversion(double value, WeightUnit sourceUnit, WeightUnit targetUnit)
    {
      // Create a weight object using the provided value and unit
      QuantityWeight weight = new QuantityWeight(value, sourceUnit);

      // Convert the weight to the requested unit
      return weight.ConvertTo(targetUnit);
    }

    /// <summary>
    /// Adds two weight quantities and returns the result.
    /// </summary>
    public QuantityWeight DemonstrateQuantityWeightAddition(QuantityWeight w1, QuantityWeight w2)
    {
      return w1.Add(w2);
    }

    /// <summary>
    /// Adds two weight quantities and returns the result converted to a specific unit.
    /// </summary>
    public QuantityWeight DemonstrateQuantityWeightAddition(QuantityWeight w1, QuantityWeight w2, WeightUnit targetUnit)
    {
      return w1.Add(w2, targetUnit);
    }
  }
}