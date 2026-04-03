using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Converters
{
  /// <summary>
  /// Provides helper methods to convert weight values between units.
  /// All conversions are performed using kilograms as the base unit.
  /// </summary>
  public class WeightUnitConverter : IMeasurable
  {
    private readonly WeightUnit _unit;

    public WeightUnitConverter(WeightUnit unit)
    {
      _unit = unit;
    }
    // Conversion factors relative to the base unit (Kilograms)
    private readonly Dictionary<WeightUnit, double> _toKgFactors = new()
    {
      {WeightUnit.Grams,0.001},      // 1 gram = 0.001 kg
      {WeightUnit.Kilograms,1.0},    // base unit
      {WeightUnit.Pound,0.453592}    // 1 pound = 0.453592 kg
    };

    /// <summary>
    /// Returns the conversion factor used to convert the specified unit to kilograms.
    /// </summary>
    private double GetConversionFactor(WeightUnit unit)
    {
      return _toKgFactors[unit];
    }

    /// <summary>
    /// Converts a value from the given unit to the base unit (kilograms).
    /// </summary>
    public double ConvertToBase(double value)
    {
      return value * GetConversionFactor(_unit);
    }

    /// <summary>
    /// Converts a value from the base unit (kilograms) to the specified unit.
    /// </summary>
    public double ConvertFromBase(double value)
    {
      return value / GetConversionFactor(_unit);
    }

    public string GetSymbol()
    {
      switch (_unit)
      {
        case WeightUnit.Kilograms:
          return "Kg";
        case WeightUnit.Grams:
          return "g";
        case WeightUnit.Pound:
          return "lb";
        default:
          return _unit.ToString().ToLower();
      }
    }
  }
}