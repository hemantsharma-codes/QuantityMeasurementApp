using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace BusinessLayer.Converters
{
  /// <summary>
  /// Provides helper methods to convert temperature values
  /// between different temperature units.
  /// All conversions internally use Celsius as the base unit.
  /// </summary>
  public class TemperatureUnitConverter : IMeasurable
  {
    private readonly TemperatureUnit _unit;

    public TemperatureUnitConverter(TemperatureUnit unit)
    {
      _unit = unit;
    }

    /// <summary>
    /// Converts the value to base unit (Celsius).
    /// </summary>
    public double ConvertToBase(double value)
    {
      return _unit switch
      {
        TemperatureUnit.Celsius => value,
        TemperatureUnit.Fahrenheit => (value - 32) * 5.0 / 9.0,
        TemperatureUnit.Kelvin => value - 273.15,
        _ => throw new ArgumentException("Invalid temperature unit")
      };
    }

    /// <summary>
    /// Converts from base unit (Celsius) to target unit.
    /// </summary>
    public double ConvertFromBase(double value)
    {
      return _unit switch
      {
        TemperatureUnit.Celsius => value,
        TemperatureUnit.Fahrenheit => (value * 9.0 / 5.0) + 32,
        TemperatureUnit.Kelvin => value + 273.15,
        _ => throw new ArgumentException("Invalid temperature unit")
      };
    }

    /// <summary>
    /// Returns symbol for temperature unit.
    /// </summary>
    public string GetSymbol()
    {
      return _unit switch
      {
        TemperatureUnit.Celsius => "°C",
        TemperatureUnit.Fahrenheit => "°F",
        TemperatureUnit.Kelvin => "K",
        _ => _unit.ToString()
      };
    }
  }
}