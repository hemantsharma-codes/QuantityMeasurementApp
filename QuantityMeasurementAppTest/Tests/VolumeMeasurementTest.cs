using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.Tests
{
  /// <summary>
  /// Tests volume measurement operations such as equality,
  /// conversion and addition.
  /// </summary>
  [TestClass]
  public class VolumeMeasurementTest
  {
    // Used for floating point comparison
    private const double Epsilon = 1e-6;

    /// <summary>
    /// Checks equality when both values are in litre.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToLitre_SameValue_ShouldReturnTrue()
    {
      var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
      var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

      Assert.IsTrue(q1.Equals(q2));
    }

    /// <summary>
    /// 1 Litre should be equal to 1000 Millilitres.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToML_EquivalentValue_ShouldReturnTrue()
    {
      var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
      var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLitre);

      Assert.IsTrue(litre.Equals(ml));
    }

    /// <summary>
    /// Checks equality between litre and gallon.
    /// </summary>
    [TestMethod]
    public void TestEquality_LitreToGallon_EquivalentValue_ShouldReturnTrue()
    {
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);

      Assert.IsTrue(litre.Equals(gallon));
    }

    /// <summary>
    /// Converts litre to millilitre.
    /// </summary>
    [TestMethod]
    public void TestConversion_LitreToML_ShouldReturnCorrectValue()
    {
      var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

      var result = litre.ConvertTo(VolumeUnit.MilliLitre);

      Assert.AreEqual(1000.0, result.Value, Epsilon);
      Assert.AreEqual(VolumeUnit.MilliLitre, result.Unit);
    }

    /// <summary>
    /// Converts gallon to litre.
    /// </summary>
    [TestMethod]
    public void TestConversion_GallonToLitre_ShouldReturnCorrectValue()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);

      var result = gallon.ConvertTo(VolumeUnit.Litre);

      Assert.AreEqual(3.78541, result.Value, Epsilon);
    }

    /// <summary>
    /// Adds litre and millilitre values.
    /// </summary>
    [TestMethod]
    public void TestAddition_LitreAndML_ShouldReturnSumInLitre()
    {
      var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
      var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MilliLitre);

      var result = q1.Add(q2);

      Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Adds litre and gallon and returns result in gallon.
    /// </summary>
    [TestMethod]
    public void TestAddition_LitreAndGallon_ExplicitTarget_ShouldReturnSumInGallon()
    {
      var q1 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
      var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);

      var result = q1.Add(q2, VolumeUnit.Gallon);

      Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Volume should not be equal to length.
    /// </summary>
    [TestMethod]
    public void TestEquality_VolumeVsLength_ShouldReturnFalse()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
      var length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

      Assert.IsFalse(volume.Equals(length));
    }

    /// <summary>
    /// Volume should not be equal to weight.
    /// </summary>
    [TestMethod]
    public void TestEquality_VolumeVsWeight_ShouldReturnFalse()
    {
      var volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
      var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);

      Assert.IsFalse(volume.Equals(weight));
    }

    /// <summary>
    /// Zero value should be equal across all volume units.
    /// </summary>
    [TestMethod]
    public void TestZeroValue_AcrossVolumeUnits_ShouldBeEqual()
    {
      var zeroL = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
      var zeroGal = new Quantity<VolumeUnit>(0.0, VolumeUnit.Gallon);

      Assert.IsTrue(zeroL.Equals(zeroGal));
    }

    /// <summary>
    /// Equality should be symmetric.
    /// </summary>
    [TestMethod]
    public void TestSymmetricEquality_GallonToLitre_ShouldBeSameBothWays()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);

      Assert.IsTrue(gallon.Equals(litre));
      Assert.IsTrue(litre.Equals(gallon));
    }

    /// <summary>
    /// Tests equality across three units.
    /// </summary>
    [TestMethod]
    public void TestTransitiveEquality_GallonToLitreToML()
    {
      var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
      var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
      var ml = new Quantity<VolumeUnit>(3785.41, VolumeUnit.MilliLitre);

      Assert.IsTrue(gallon.Equals(litre));
      Assert.IsTrue(litre.Equals(ml));
      Assert.IsTrue(gallon.Equals(ml));
    }

    /// <summary>
    /// Tests conversion with large volume value.
    /// </summary>
    [TestMethod]
    public void TestLargeVolume_ConversionPrecision()
    {
      var largeLitre = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);

      var result = largeLitre.ConvertTo(VolumeUnit.MilliLitre);

      Assert.AreEqual(1000000000.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Converts very small volume from ml to gallon.
    /// </summary>
    [TestMethod]
    public void TestSmallVolume_MillLitreToGallon()
    {
      var oneMl = new Quantity<VolumeUnit>(1.0, VolumeUnit.MilliLitre);

      var result = oneMl.ConvertTo(VolumeUnit.Gallon);

      Assert.AreEqual(0.000264172, result.Value, Epsilon);
    }

    /// <summary>
    /// Tests addition with negative volume value.
    /// </summary>
    [TestMethod]
    public void TestNegativeVolume_Addition()
    {
      var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
      var q2 = new Quantity<VolumeUnit>(-2.0, VolumeUnit.Litre);

      var result = q1.Add(q2);

      Assert.AreEqual(3.0, result.Value, Epsilon);
    }

    /// <summary>
    /// Adding zero should not change the value.
    /// </summary>
    [TestMethod]
    public void TestAddition_WithZero_ShouldReturnOriginalValue()
    {
      var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Gallon);
      var zeroMl = new Quantity<VolumeUnit>(0.0, VolumeUnit.MilliLitre);

      var result = q1.Add(zeroMl);

      Assert.AreEqual(10.0, result.Value, Epsilon);
      Assert.AreEqual(VolumeUnit.Gallon, result.Unit);
    }

    /// <summary>
    /// Converts litre to gallon and back to litre.
    /// </summary>
    [TestMethod]
    public void TestRoundTripConversion_LitreToGallonToLitre()
    {
      var original = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);

      var toGallon = original.ConvertTo(VolumeUnit.Gallon);
      var backToLitre = toGallon.ConvertTo(VolumeUnit.Litre);

      Assert.AreEqual(10.0, backToLitre.Value, Epsilon);
    }
  }
}