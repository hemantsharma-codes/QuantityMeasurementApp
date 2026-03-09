namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents supported unit types for length measurement.
  /// </summary>
  public enum QuantityType
  {
    Feet,
    Inches,
    Yards,
    Centimeters
  }

  /// <summary>
  /// Handles unit conversion by using Inches as the base unit.
  /// </summary>
  public static class QuantityConverter
  {
    // Conversion factors relative to base unit (Inches)
    private static readonly Dictionary<QuantityType, double> _conversionUnit = new()
    {
      { QuantityType.Feet, 12.0 },        // 1 Foot = 12 Inches
      { QuantityType.Inches, 1.0 },       // Base unit
      { QuantityType.Yards, 36.0 },       // 1 Yard = 36 Inches
      { QuantityType.Centimeters, 0.393701 } // 1 Centimeter = 0.393701 Inches
    };

    /// <summary>
    /// Returns the conversion factor for the given unit.
    /// </summary>
    public static double GetConversionFactor(QuantityType type)
    {
      return _conversionUnit[type];
    }

    /// <summary>
    /// Converts the given value to base unit (Inches).
    /// </summary>
    public static double ConvertToBase(QuantityType type, double value)
    {
      return value * GetConversionFactor(type);
    }

    /// <summary>
    /// Converts a base unit value (Inches) to the given unit.
    /// </summary>
    public static double ConvertFromBase(QuantityType type, double value)
    {
      return value / GetConversionFactor(type);
    }
  }
}