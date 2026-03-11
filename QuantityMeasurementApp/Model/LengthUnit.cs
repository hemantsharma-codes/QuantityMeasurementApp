namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents supported unit types for length measurement.
  /// </summary>
  public enum LengthUnit
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
    private static readonly Dictionary<LengthUnit, double> _conversionUnit = new()
    {
      { LengthUnit.Feet, 12.0 },        // 1 Foot = 12 Inches
      { LengthUnit.Inches, 1.0 },       // Base unit
      { LengthUnit.Yards, 36.0 },       // 1 Yard = 36 Inches
      { LengthUnit.Centimeters, 0.393701 } // 1 Centimeter = 0.393701 Inches
    };

    /// <summary>
    /// Returns the conversion factor for the given unit.
    /// </summary>
    public static double GetConversionFactor(LengthUnit type)
    {
      return _conversionUnit[type];
    }

    /// <summary>
    /// Converts the given value to base unit (Inches).
    /// </summary>
    public static double ConvertToBase(LengthUnit type, double value)
    {
      return value * GetConversionFactor(type);
    }

    /// <summary>
    /// Converts a base unit value (Inches) to the given unit.
    /// </summary>
    public static double ConvertFromBase(LengthUnit type, double value)
    {
      return value / GetConversionFactor(type);
    }


    // Returns the  symbol for the unit for UI display.
    public static string GetSymbol(LengthUnit unit)
    {
      switch (unit)
      {
        case LengthUnit.Feet:
          return "ft";
        case LengthUnit.Inches:
          return "in";
        case LengthUnit.Yards:
          return "yd";
        case LengthUnit.Centimeters:
          return "cm";
        default:
          return unit.ToString().ToLower();
      }
    }
  }
}