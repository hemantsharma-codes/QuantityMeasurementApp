namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Supported quantity units.
  /// </summary>
  public enum QuantityType
  {
    Feet,
    Inches,
    Yards,
    Centimeters 
  }

  /// <summary>
  /// Provides conversion factors to a base unit (Inches).
  /// </summary>
  public static class QuantityConverter
  {
    // Conversion factors relative to base unit: Inches
    private static readonly Dictionary<QuantityType, double> _conversionUnit = new()
    {
      { QuantityType.Feet, 12.0 }, // Feet (12 inches = 1Feet)
      { QuantityType.Inches, 1.0 }, // Inches
      {QuantityType.Yards, 36}, // (36 Inches = 1 Yard)
      {QuantityType.Centimeters, 0.393701} // (0.393701 = 1 Centimeter)
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