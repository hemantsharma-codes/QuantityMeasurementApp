using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.BusinessLayer
{
  /// <summary>
  /// Provides business logic for comparing quantity measurements.
  /// Currently supports equality comparison for feet measurements (UC1).
  /// </summary>
  public sealed class QuantityComparisonService
  {
    /// <summary>
    /// Compares two <see cref="Feet"/> objects for equality.
    /// </summary>
    /// <param name="feet1">First feet measurement</param>
    /// <param name="feet2">Second feet measurement</param>
    /// <returns>
    /// True if both measurements are equal or both are null;
    /// otherwise, false.
    /// </returns>
    public bool DemonstrateFeetEquality(Feet feet1, Feet feet2)
    {
      // If both references are null, they are considered equal
      if (feet1 == null && feet2 == null)
        return true;

      // If only one reference is null, values cannot be equal
      if (feet1 == null || feet2 == null)
        return false;

      // Delegate value comparison to the Feet class
      return feet1.Equals(feet2);
    }

    public bool DemonstrateInchesEquality(Inches inch1, Inches inch2)
    {
      // If both references are null, they are considered equal
      if (inch1 == null && inch2 == null)
      {
        return true;
      }

      // If only one reference is null, values cannot be equal
      if (inch1 == null || inch2 == null)
      {
        return false;
      }

      // Delegate value comparison to the Feet class
      return inch1.Equals(inch2);
    }
  }
}