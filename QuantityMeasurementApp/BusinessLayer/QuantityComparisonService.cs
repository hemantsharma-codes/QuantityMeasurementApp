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
  }
}