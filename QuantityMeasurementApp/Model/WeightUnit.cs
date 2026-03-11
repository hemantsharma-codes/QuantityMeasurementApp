namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents the supported units for weight measurement.
  /// </summary>
  public enum WeightUnit
  {
    Grams,
    Kilograms,
    Pound
  }

  /// <summary>
  /// Provides helper methods to convert weight values between units.
  /// All conversions are performed using kilograms as the base unit.
  /// </summary>
  public static class WeightUnitConverter
  {
    // Conversion factors relative to the base unit (Kilograms)
    private static readonly Dictionary<WeightUnit, double> _toKgFactors = new()
    {
      {WeightUnit.Grams,0.001},      // 1 gram = 0.001 kg
      {WeightUnit.Kilograms,1.0},    // base unit
      {WeightUnit.Pound,0.453592}    // 1 pound = 0.453592 kg
    };

    /// <summary>
    /// Returns the conversion factor used to convert the specified unit to kilograms.
    /// </summary>
    public static double GetConversionFactor(WeightUnit unit)
    {
      return _toKgFactors[unit];
    }

    /// <summary>
    /// Converts a value from the given unit to the base unit (kilograms).
    /// </summary>
    public static double ConvertToBase(WeightUnit unit, double value)
    {
      return value * GetConversionFactor(unit);
    }

    /// <summary>
    /// Converts a value from the base unit (kilograms) to the specified unit.
    /// </summary>
    public static double ConvertFromBase(WeightUnit unit, double value)
    {
      return value / GetConversionFactor(unit);
    }

    public static string GetSymbol(WeightUnit unit)
    {
      switch (unit)
      {
        case WeightUnit.Kilograms:
          return "Kg";
        case WeightUnit.Grams:
          return "g";
        case WeightUnit.Pound:
          return "lb";
        default:
          return unit.ToString().ToLower();
      }
    }
  }
}