using ModelLayer.Enums;
using ModelLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Converters;

namespace QuantityMeasurementAppTest.Tests
{
  /// <summary>
  /// Contains unit tests for Temperature measurement functionality.
  /// Tests equality, conversion, arithmetic operations and compatibility
  /// with other measurement types.
  /// </summary>
  [TestClass]
  public class TemperatureMeasurementTests
  {
    /// <summary>
    /// Tolerance used when comparing floating-point values.
    /// </summary>
    private const double PrecisionTolerance = 0.0001;

    /// <summary>
    /// Verifies two temperature quantities with the same Celsius value are equal.
    /// </summary>
    [TestMethod]
    public void Equals_WhenBothTemperaturesAreSameCelsius_ShouldReturnTrue()
    {
      var firstTemperature = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var secondTemperature = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      Assert.IsTrue(firstTemperature.Equals(secondTemperature));
    }

    /// <summary>
    /// Verifies two temperatures with same Fahrenheit value are equal.
    /// </summary>
    [TestMethod]
    public void Equals_WhenBothTemperaturesAreSameFahrenheit_ShouldReturnTrue()
    {
      var firstTemperature = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));
      var secondTemperature = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      Assert.IsTrue(firstTemperature.Equals(secondTemperature));
    }

    /// <summary>
    /// Verifies that 0°C equals 32°F.
    /// </summary>
    [TestMethod]
    public void Equals_WhenComparingCelsiusAndFahrenheitEquivalentValues_ShouldReturnTrue()
    {
      var temperatureInCelsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var temperatureInFahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      Assert.IsTrue(temperatureInCelsius.Equals(temperatureInFahrenheit));
    }

    /// <summary>
    /// Verifies conversion from Celsius to Fahrenheit.
    /// </summary>
    [DataTestMethod]
    [DataRow(50.0, 122.0)]
    [DataRow(-20.0, -4.0)]
    public void ConvertTo_WhenCelsiusConvertedToFahrenheit_ShouldReturnExpectedValue(double celsiusValue, double expectedFahrenheit)
    {
      var temperature = new Quantity<TemperatureUnit>(celsiusValue, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      var convertedTemperature = temperature.ConvertTo(TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      Assert.AreEqual(expectedFahrenheit, convertedTemperature.Value, PrecisionTolerance);
    }

    /// <summary>
    /// Verifies conversion from Fahrenheit to Celsius.
    /// </summary>
    [DataTestMethod]
    [DataRow(122.0, 50.0)]
    [DataRow(-4.0, -20.0)]
    public void ConvertTo_WhenFahrenheitConvertedToCelsius_ShouldReturnExpectedValue(double fahrenheitValue, double expectedCelsius)
    {
      var temperature = new Quantity<TemperatureUnit>(fahrenheitValue, TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      var convertedTemperature = temperature.ConvertTo(TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      Assert.AreEqual(expectedCelsius, convertedTemperature.Value, PrecisionTolerance);
    }

    /// <summary>
    /// Ensures round-trip conversion preserves the original temperature.
    /// </summary>
    [TestMethod]
    public void ConvertTo_WhenConvertedToAnotherUnitAndBack_ShouldPreserveOriginalValue()
    {
      double originalTemperature = 75.5;

      var temperature = new Quantity<TemperatureUnit>(originalTemperature, TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      var convertedToCelsius = temperature.ConvertTo(TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var convertedBackToFahrenheit = convertedToCelsius.ConvertTo(TemperatureUnit.Fahrenheit, new TemperatureUnitConverter(TemperatureUnit.Fahrenheit));

      Assert.AreEqual(originalTemperature, convertedBackToFahrenheit.Value, PrecisionTolerance);
    }

    /// <summary>
    /// Verifies addition operation between two temperature values.
    /// </summary>
    [TestMethod]
    public void Add_WhenTwoTemperaturesAreAdded_ShouldReturnCorrectSum()
    {
      var firstTemperature = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var secondTemperature = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      var resultTemperature = firstTemperature.Add(secondTemperature);

      Assert.AreEqual(150.0, resultTemperature.Value);
      Assert.AreEqual(TemperatureUnit.Celsius, resultTemperature.Unit);
    }

    /// <summary>
    /// Verifies subtraction operation between two temperature values.
    /// </summary>
    [TestMethod]
    public void Subtract_WhenTwoTemperaturesAreSubtracted_ShouldReturnCorrectDifference()
    {
      var firstTemperature = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var secondTemperature = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      var resultTemperature = firstTemperature.Subtract(secondTemperature);

      Assert.AreEqual(50.0, resultTemperature.Value);
    }

    /// <summary>
    /// Verifies division by zero throws exception.
    /// </summary>
    [TestMethod]
    public void Divide_WhenTemperatureDivisionAttempted_ShouldThrowInvalidOperationException()
    {
      var firstTemperature = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var secondTemperature = new Quantity<TemperatureUnit>(0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));

      Assert.Throws<InvalidOperationException>(() =>
          firstTemperature.Divide(secondTemperature));
    }

    /// <summary>
    /// Verifies temperature quantity is not compatible with length measurement.
    /// </summary>
    [TestMethod]
    public void Equals_WhenComparingTemperatureWithLength_ShouldReturnFalse()
    {
      var temperature = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius, new TemperatureUnitConverter(TemperatureUnit.Celsius));
      var length = new Quantity<LengthUnit>(100.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      Assert.IsFalse(temperature.Equals(length));
    }

    /// <summary>
    /// Verifies temperature unit symbols are correct.
    /// </summary>
    [TestMethod]
    public void GetSymbol_WhenTemperatureUnitRequested_ShouldReturnCorrectSymbol()
    {
      Assert.AreEqual("°C", new TemperatureUnitConverter(TemperatureUnit.Celsius).GetSymbol());
      Assert.AreEqual("°F", new TemperatureUnitConverter(TemperatureUnit.Fahrenheit).GetSymbol());
    }
  }
}