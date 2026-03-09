using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.BusinessLayer
{
  /// <summary>
  /// Compares two Quantity objects for equality.
  /// </summary>
  public class QuantityComparisonService
  {
    /// <summary>
    /// Returns true if both quantities represent the same measurement.
    /// </summary>
    public bool AreEqual(Quantity q1, Quantity q2)
    {
      // Both null means equal
      if (q1 == null && q2 == null)
      {
        return true;
      }

      // If one is null, they are not equal
      if (q1 == null || q2 == null)
      {
        return false;
      }

      // Delegate comparison to Quantity
      return q1.Equals(q2);
    }

    /// <summary>
    /// Converts a given quantity value from a source unit to a target unit.
    /// </summary>
    /// <param name="value">The numeric value of the quantity to convert.</param>
    /// <param name="sourceUnit">The unit type of the provided value.</param>
    /// <param name="targetUnit">The unit type to which the value should be converted.</param>
    /// <returns>
    /// The converted value in the specified target unit.
    /// </returns>
    public double DemonstrateQuantityConversion(double value, QuantityType sourceUnit, QuantityType targetUnit)
    {
      // Create a quantity object with the provided value and source unit
      Quantity quantity = new Quantity(value, sourceUnit);

      // Convert the quantity to the requested target unit
      return quantity.ConvertTo(targetUnit);
    }

    /// <summary>
    /// Calls the Add method to combine two quantities and returns the result.
    /// </summary>
    /// <param name="q1">The first quantity.</param>
    /// <param name="q2">The second quantity to be added to the first.</param>
    /// <returns>A Quantity object representing the total of both quantities.</returns>
    public Quantity DemonstrateQuantityAddition(Quantity q1, Quantity q2)
    {
      // Delegate the addition operation to the Quantity model
      return q1.Add(q2);
    }
    
    /// <summary>
    /// Calls the Add method to combine two quantities, converts into target unit
    /// and return result
    /// </summary>
    /// <param name="q1">The first quantity</param>
    /// <param name="q2">The second quantity to be added to the first</param>
    /// <param name="targetUnit">after adding quantities convert into target unit </param>
    /// <returns>A Quantity object representing the total of both quantities.</returns>
    public Quantity DemonstrateQuantityAddition(Quantity q1, Quantity q2, QuantityType targetUnit)
    {
      return q1.Add(q2, targetUnit);
    }
  }
}