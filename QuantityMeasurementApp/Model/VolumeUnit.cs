namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents units used for volume measurement.
  /// These units are used with the generic Quantity class.
  /// </summary>
  public enum VolumeUnit
  {
    Litre,
    MilliLitre,
    Gallon
  }

  /// <summary>
  /// Provides helper methods for converting volume units.
  /// All volume values are internally converted using Litre as the base unit.
  /// </summary>
  public static class VolumeUnitConverter
  {
    // Conversion factors to convert any volume unit to the base unit (Litre)
    private static readonly Dictionary<VolumeUnit, double> _toLitreFactor = new()
    {
      {VolumeUnit.Litre, 1.0},
      {VolumeUnit.MilliLitre,0.001},
      {VolumeUnit.Gallon,3.78541}
    };

    /// <summary>
    /// Returns the conversion factor of a unit relative to the base unit (Litre).
    /// </summary>
    public static double GetConversionFactor(VolumeUnit unit)
    {
      return _toLitreFactor[unit];
    }

    /// <summary>
    /// Converts a given volume value into the base unit (Litre).
    /// </summary>
    public static double ConvertToBase(VolumeUnit unit, double value)
    {
      return value * GetConversionFactor(unit);
    }

    /// <summary>
    /// Converts a base unit (Litre) value into the specified volume unit.
    /// </summary>
    public static double ConvertFromBase(VolumeUnit unit, double value)
    {
      return value / GetConversionFactor(unit);
    }

    /// <summary>
    /// Returns the short symbol for a given volume unit.
    /// Example: Litre -> L, MilliLiter -> ML, Gallon -> gal
    /// </summary>
    public static string GetSymbol(VolumeUnit unit)
    {
      switch (unit)
      {
        case VolumeUnit.Litre:
          return "L";
        case VolumeUnit.MilliLitre:
          return "ML";
        case VolumeUnit.Gallon:
          return "gal";
        default:
          return unit.ToString().ToLower();
      }
    }
  }
}