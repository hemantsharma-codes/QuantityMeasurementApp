using ModelLayer.Enums;
using ModelLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Converters;

namespace QuantityMeasurementAppTest.Tests
{
  /// <summary>
  /// Contains unit tests for subtraction and division operations
  /// in the Quantity class.
  /// </summary>
  [TestClass]
  public class QuantityArithmeticTests
  {
    private const double Tolerance = 1e-6;

    #region Subtraction Tests

    [TestMethod]
    [Description("Subtracts two quantities with same unit.")]
    public void Subtract_SameLengthUnits_ShouldReturnCorrectDifference()
    {
      var firstLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var secondLength = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      var difference = firstLength.Subtract(secondLength);

      Assert.AreEqual(5.0, difference.Value, Tolerance);
      Assert.AreEqual(LengthUnit.Feet, difference.Unit);
    }

    [TestMethod]
    [Description("Subtracts quantities with different length units.")]
    public void Subtract_CrossLengthUnits_ShouldReturnCorrectFeet()
    {
      var totalLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var subtractLength = new Quantity<LengthUnit>(6.0, LengthUnit.Inches, new LengthUnitConverter(LengthUnit.Inches));

      var result = totalLength.Subtract(subtractLength);

      Assert.AreEqual(9.5, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Subtracts volume quantities and converts result.")]
    public void Subtract_VolumeToDifferentUnit_ShouldReturnMilliLitres()
    {
      var initialVolume = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var subtractVolume = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var volumeDifference = initialVolume.Subtract(subtractVolume, VolumeUnit.MilliLitre, new VolumeUnitConverter(VolumeUnit.MilliLitre));

      Assert.AreEqual(3000.0, volumeDifference.Value, Tolerance);
      Assert.AreEqual(VolumeUnit.MilliLitre, volumeDifference.Unit);
    }

    [TestMethod]
    [Description("Subtracts weight quantities producing negative result.")]
    public void Subtract_WeightUnits_ResultingNegative_ShouldReturnCorrectValue()
    {
      var originalWeight = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms, new WeightUnitConverter(WeightUnit.Kilograms));
      var heavierWeight = new Quantity<WeightUnit>(5.0, WeightUnit.Kilograms, new WeightUnitConverter(WeightUnit.Kilograms));

      var result = originalWeight.Subtract(heavierWeight);

      Assert.AreEqual(-3.0, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Subtraction order should affect result.")]
    public void Subtract_OrderMatters_ShouldProduceDifferentResults()
    {
      var quantityA = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var quantityB = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      var diffAB = quantityA.Subtract(quantityB).Value;
      var diffBA = quantityB.Subtract(quantityA).Value;

      Assert.AreNotEqual(diffAB, diffBA);
    }

    [TestMethod]
    [Description("Subtracting zero should not change value.")]
    public void Subtract_ZeroOperand_ShouldReturnOriginalValue()
    {
      var quantity = new Quantity<LengthUnit>(5.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var zeroLength = new Quantity<LengthUnit>(0.0, LengthUnit.Inches, new LengthUnitConverter(LengthUnit.Inches));

      var result = quantity.Subtract(zeroLength);

      Assert.AreEqual(5.0, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Original quantity should remain unchanged after subtraction.")]
    public void Subtract_ShouldNotChangeOriginalQuantity()
    {
      var originalLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var subLength = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      originalLength.Subtract(subLength);

      Assert.AreEqual(10.0, originalLength.Value, Tolerance);
    }

    [TestMethod]
    [Description("Addition followed by subtraction should return original value.")]
    public void Arithmetic_AddThenSubtract_ShouldReturnOriginalValue()
    {
      var baseLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));
      var addedLength = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      var finalResult = baseLength.Add(addedLength).Subtract(addedLength);

      Assert.AreEqual(10.0, finalResult.Value, Tolerance);
    }

    #endregion

    #region Division Tests

    [TestMethod]
    [Description("Divides quantities with same unit.")]
    public void Divide_SameWeightUnits_ShouldReturnCorrectRatio()
    {
      var totalWeight = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms, new WeightUnitConverter(WeightUnit.Kilograms));
      var unitWeight = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms, new WeightUnitConverter(WeightUnit.Kilograms));

      var ratio = totalWeight.Divide(unitWeight);

      Assert.AreEqual(5.0, ratio, Tolerance);
    }

    [TestMethod]
    [Description("Divides quantities with different units.")]
    public void Divide_LengthCrossUnits_ShouldReturnNormalizedRatio()
    {
      var lengthInches = new Quantity<LengthUnit>(24.0, LengthUnit.Inches, new LengthUnitConverter(LengthUnit.Inches));
      var lengthFeet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet, new LengthUnitConverter(LengthUnit.Feet));

      var ratio = lengthInches.Divide(lengthFeet);

      Assert.AreEqual(1.0, ratio, Tolerance);
    }

    [TestMethod]
    [Description("Dividing by zero quantity should throw exception.")]
    public void Divide_ByZeroQuantity_ShouldThrowException()
    {
      var volume1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));
      var zeroVolume = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre, new VolumeUnitConverter(VolumeUnit.Litre));

      var ex = Assert.Throws<InvalidOperationException>(() => volume1.Divide(zeroVolume));

      StringAssert.Contains(ex.Message, "Cannot divide by zero");
    }

    #endregion
  }
}