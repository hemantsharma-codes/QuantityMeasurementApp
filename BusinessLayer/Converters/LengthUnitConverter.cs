using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Converters
{
  /// <summary>
  /// Handles unit conversion by using Inches as the base unit.
  /// </summary>
  public class LengthUnitConverter : IMeasurable
  {
    private readonly LengthUnit _unit;

    public LengthUnitConverter(LengthUnit unit)
    {
      _unit = unit;
    }
    // Conversion factors relative to base unit (Inches)
    private readonly Dictionary<LengthUnit, double> _conversionUnit = new()
    {
      { LengthUnit.Feet, 12.0 },        // 1 Foot = 12 Inches
      { LengthUnit.Inches, 1.0 },       // Base unit
      { LengthUnit.Yards, 36.0 },       // 1 Yard = 36 Inches
      { LengthUnit.Centimeters, 0.393701 } // 1 Centimeter = 0.393701 Inches
    };

    /// <summary>
    /// Returns the conversion factor for the given unit.
    /// </summary>
    private double GetConversionFactor(LengthUnit type)
    {
      return _conversionUnit[type];
    }

    /// <summary>
    /// Converts the given value to base unit (Inches).
    /// </summary>
    public double ConvertToBase(double value)
    {
      return value * GetConversionFactor(_unit);
    }

    /// <summary>
    /// Converts a base unit value (Inches) to the given unit.
    /// </summary>
    public double ConvertFromBase(double value)
    {
      return value / GetConversionFactor(_unit);
    }


    // Returns the  symbol for the unit for UI display.
    public string GetSymbol()
    {
      return _unit switch
      {
        LengthUnit.Feet => "ft",
        LengthUnit.Inches => "in",
        LengthUnit.Yards => "yd",
        LengthUnit.Centimeters => "cm",
        _ => _unit.ToString()
      };
    }
  }
}