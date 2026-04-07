using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Converters
{
  /// <summary>
  /// Provides helper methods for converting volume units.
  /// All volume values are internally converted using Litre as the base unit.
  /// </summary>
  public class VolumeUnitConverter : IMeasurable
  {
    private readonly VolumeUnit _unit;

    public VolumeUnitConverter(VolumeUnit unit)
    {
      _unit = unit;
    }

    private readonly Dictionary<VolumeUnit, double> _toLitreFactor = new()
    {
      { VolumeUnit.Litre, 1.0 },
      { VolumeUnit.MilliLitre, 0.001 },
      { VolumeUnit.Gallon, 3.78541 }
    };

    private double GetConversionFactor(VolumeUnit unit)
    {
      return _toLitreFactor[unit];
    }

    /// <summary>
    /// Converts a given volume value into the base unit (Litre).
    /// </summary>
    public double ConvertToBase(double value)
    {
      return value * GetConversionFactor(_unit);
    }

    /// <summary>
    /// Converts a base unit (Litre) value into the specified volume unit.
    /// </summary>
    public double ConvertFromBase(double value)
    {
      return value / GetConversionFactor(_unit);
    }

    /// <summary>
    /// Returns the symbol for the volume unit.
    /// </summary>
    public string GetSymbol()
    {
      return _unit switch
      {
        VolumeUnit.Litre => "L",
        VolumeUnit.MilliLitre => "ML",
        VolumeUnit.Gallon => "gal",
        _ => _unit.ToString()
      };
    }
  }
}