namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Supported quantity units.
  /// </summary>
  public enum QuantityType
  {
    Feet,
    Inches
  }

  /// <summary>
  /// Provides conversion factors to a base unit (Inches).
  /// </summary>
  public static class QuantityConverter
  {
    // Conversion factors relative to base unit: Inches
    private static readonly Dictionary<QuantityType, double> _conversionUnit = new()
    {
      { QuantityType.Feet, 12.0 },
      { QuantityType.Inches, 1.0 }
    };

    /// <summary>
    /// Returns conversion factor for the given quantity type.
    /// </summary>
    public static double GetConversionFactor(QuantityType type)
    {
      return _conversionUnit[type];
    }
  }
}