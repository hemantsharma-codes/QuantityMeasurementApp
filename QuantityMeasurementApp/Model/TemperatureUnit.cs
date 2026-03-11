using System.ComponentModel;

namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Represents the units used for temperature measurement.
  /// Celsius is treated as the base unit for conversion.
  /// </summary>
  public enum TemperatureUnit
  {
    Celsius,      // Base Unit
    Fahrenheit,
    Kelvin
  }

  /// <summary>
  /// Provides helper methods to convert temperature values
  /// between different temperature units.
  /// All conversions internally use Celsius as the base unit.
  /// </summary>
  public static class TemperatureUnitConverter
  {
    /// <summary>
    /// Converts a given temperature value from the specified unit
    /// into the base unit (Celsius).
    /// </summary>
    /// <param name="unit">The unit of the input temperature value.</param>
    /// <param name="value">The temperature value to convert.</param>
    /// <returns>Temperature value converted to Celsius.</returns>
    public static double ConvertToBase(TemperatureUnit unit, double value)
    {
      // Convert the given unit into the base unit (Celsius)
      return unit switch
      {
        TemperatureUnit.Celsius => value,
        TemperatureUnit.Fahrenheit => (value - 32) * 5.0 / 9.0,
        TemperatureUnit.Kelvin => value - 273.15,
        _ => throw new ArgumentException("Invalid temperature unit")
      };
    }

    /// <summary>
    /// Converts a temperature value from the base unit (Celsius)
    /// into the specified target temperature unit.
    /// </summary>
    /// <param name="unit">Target temperature unit.</param>
    /// <param name="value">Temperature value in Celsius.</param>
    /// <returns>Temperature value converted to the specified unit.</returns>
    public static double ConvertFromBase(TemperatureUnit unit, double value)
    {
      // Convert the base value (Celsius) to the desired target unit
      return unit switch
      {
        TemperatureUnit.Celsius => value,
        TemperatureUnit.Fahrenheit => (value * 9.0 / 5.0) + 32.0,
        TemperatureUnit.Kelvin => value + 273.15,
        _ => throw new ArgumentException("Invalid temperature unit")
      };
    }

    /// <summary>
    /// Returns the display symbol associated with the given temperature unit.
    /// </summary>
    /// <param name="unit">Temperature unit.</param>
    /// <returns>Symbol representing the temperature unit (°C, °F, °K).</returns>
    public static string GetSymbol(TemperatureUnit unit)
    {
      // Provide the standard symbol used for each temperature unit
      return unit switch
      {
        TemperatureUnit.Celsius => "°C",
        TemperatureUnit.Fahrenheit => "°F",
        TemperatureUnit.Kelvin => "K",
        _ => unit.ToString()
      };
    }
  }
}