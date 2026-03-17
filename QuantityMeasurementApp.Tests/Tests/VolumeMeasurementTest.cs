using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer.Enums;
using ModelLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Converters;

namespace QuantityMeasurementAppTest.Tests
{
  /// <summary>
  /// Contains unit tests for validating volume measurement behavior,
  /// including equality checks, unit conversions, and arithmetic operations
  /// across supported volume units.
  /// </summary>
  [TestClass]
  public class VolumeMeasurementTest
  {
    /// <summary>
    /// Tolerance value used for floating-point comparisons to avoid
    /// precision issues during assertions.
    /// </summary>
    private const double Epsilon = 1e-6;

    /// <summary>
    /// Verifies that two quantities with the same value and unit are equal.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToLitre_SameValue_ShouldReturnTrue()
    {
      var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      Assert.IsTrue(q1.Equals(q2));
    }

    /// <summary>
    /// Ensures that equivalent values expressed in litres and millilitres
    /// are treated as equal.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToML_EquivalentValue_ShouldReturnTrue()
    {
      var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      Assert.IsTrue(litre.Equals(ml));
    }

    /// <summary>
    /// Validates equality between litre and gallon quantities representing
    /// the same physical volume.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToGallon_EquivalentValue_ShouldReturnTrue()
    {
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      Assert.IsTrue(litre.Equals(gallon));
    }

    /// <summary>
    /// Verifies correct conversion from litres to millilitres.
    /// </summary>
    [TestMethod]
    public void TestConversion_LitreToML_ShouldReturnCorrectValue()
    {
      var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var result = litre.ConvertTo(VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      Assert.AreEqual(1000.0, result.Value, Epsilon);
      Assert.AreEqual(VolumeUnit.MilliLitre, result.Unit);
    }

    /// <summary>
    /// Verifies correct conversion from gallon to litre.
    /// </summary>
    [TestMethod]
    public void TestConversion_GallonToLitre_ShouldReturnCorrectValue()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      var result = gallon.ConvertTo(VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      Assert.AreEqual(3.78541, result.Value, Epsilon);
    }

    /// <summary>
    /// Ensures that addition across different volume units produces
    /// the correct result in the unit of the first operand.
    /// </summary>
    [TestMethod]
    public void TestAddition_LitreAndML_ShouldReturnSumInLitre()
    {
      var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      var result = q1.Add(q2);

      Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Verifies addition when the result is explicitly requested
    /// in a specific target unit.
    /// </summary>
    [TestMethod]
    public void TestAddition_LitreAndGallon_ExplicitTarget_ShouldReturnSumInGallon()
    {
      var q1 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      var result = q1.Add(q2, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Ensures equality comparison fails when comparing
    /// different measurement categories.
    /// </summary>
    [TestMethod]
    public void TestEquality_VolumeVsLength_ShouldReturnFalse()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      Assert.IsFalse(volume.Equals(length));
    }

    /// <summary>
    /// Ensures volume and weight quantities are never considered equal.
    /// </summary>
    [TestMethod]
    public void TestEquality_VolumeVsWeight_ShouldReturnFalse()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms, new WeightUnitConverter(WeightUnit.Kilograms));

      Assert.IsFalse(volume.Equals(weight));
    }

    /// <summary>
    /// Confirms that zero values remain equal regardless of the unit used.
    /// </summary>
    [TestMethod]
    public void TestZeroValue_AcrossVolumeUnits_ShouldBeEqual()
    {
      var zeroL = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var zeroGal = new Quantity<VolumeUnit>(0.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      Assert.IsTrue(zeroL.Equals(zeroGal));
    }

    /// <summary>
    /// Validates that equality is symmetric between equivalent units.
    /// </summary>
    [TestMethod]
    public void TestSymmetricEquality_GallonToLitre_ShouldBeSameBothWays()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      Assert.IsTrue(gallon.Equals(litre));
      Assert.IsTrue(litre.Equals(gallon));
    }

    /// <summary>
    /// Validates the transitive property of equality across
    /// multiple equivalent units.
    /// </summary>
    [TestMethod]
    public void TestTransitiveEquality_GallonToLitreToML()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var ml = new Quantity<VolumeUnit>(3785.41, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      Assert.IsTrue(gallon.Equals(litre));
      Assert.IsTrue(litre.Equals(ml));
      Assert.IsTrue(gallon.Equals(ml));
    }

    /// <summary>
    /// Ensures conversions remain accurate for very large values.
    /// </summary>
    [TestMethod]
    public void TestLargeVolume_ConversionPrecision()
    {
      var largeLitre = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var result = largeLitre.ConvertTo(VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      Assert.AreEqual(1000000000.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Verifies conversion of very small quantities between units.
    /// </summary>
    [TestMethod]
    public void TestSmallVolume_MillLitreToGallon()
    {
      var oneMl = new Quantity<VolumeUnit>(1.0, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      var result = oneMl.ConvertTo(VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));

      Assert.AreEqual(0.000264172, result.Value, Epsilon);
    }

    /// <summary>
    /// Validates addition when one of the quantities is negative.
    /// </summary>
    [TestMethod]
    public void TestNegativeVolume_Addition()
    {
      var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var q2 = new Quantity<VolumeUnit>(-2.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var result = q1.Add(q2);

      Assert.AreEqual(3.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Ensures that adding zero does not change the original quantity.
    /// </summary>
    [TestMethod]
    public void TestAddition_WithZero_ShouldReturnOriginalValue()
    {
      var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));
      var zeroMl = new Quantity<VolumeUnit>(0.0, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      var result = q1.Add(zeroMl);

      Assert.AreEqual(10.0, result.Value, Epsilon);
      Assert.AreEqual(VolumeUnit.Gallon, result.Unit);
    }

    /// <summary>
    /// Verifies that converting a value to another unit and back again
    /// preserves the original value within the defined tolerance.
    /// </summary>
    [TestMethod]
    public void TestRoundTripConversion_LitreToGallonToLitre()
    {
      var original = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var toGallon = original.ConvertTo(VolumeUnit.Gallon, new VolumeUnitConverter(VolumeUnit.Gallon));
      var backToLitre = toGallon.ConvertTo(VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      Assert.AreEqual(10.0, backToLitre.Value, Epsilon);
    }
  }
}