using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.Tests
{
  /// <summary>
  /// Contains unit tests for subtraction and division operations
  /// in the Quantity class across Length, Weight, and Volume units.
  /// </summary>
  [TestClass]
  public class QuantityArithmeticTests
  {
    private const double Tolerance = 1e-6;

    #region Subtraction Tests

    [TestMethod]
    [Description("Subtracts two quantities of the same unit and verifies the difference.")]
    public void Subtract_SameLengthUnits_ShouldReturnCorrectDifference()
    {
      // Arrange
      var firstLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
      var secondLength = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);

      // Act
      var difference = firstLength.Subtract(secondLength);

      // Assert
      Assert.AreEqual(5.0, difference.Value, Tolerance);
      Assert.AreEqual(LengthUnit.Feet, difference.Unit);
    }

    [TestMethod]
    [Description("Subtracts two quantities of different length units and returns result in base unit.")]
    public void Subtract_CrossLengthUnits_ShouldReturnCorrectFeet()
    {
      // Arrange
      var totalLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
      var subtractLength = new Quantity<LengthUnit>(6.0, LengthUnit.Inches);

      // Act
      var result = totalLength.Subtract(subtractLength);

      // Assert
      Assert.AreEqual(9.5, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Subtracts two volume quantities and converts the result to milliliters.")]
    public void Subtract_VolumeToDifferentUnit_ShouldReturnMilliLitres()
    {
      // Arrange
      var initialVolume = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
      var subtractVolume = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);

      // Act
      var volumeDifference = initialVolume.Subtract(subtractVolume, VolumeUnit.MilliLitre);

      // Assert
      Assert.AreEqual(3000.0, volumeDifference.Value, Tolerance);
      Assert.AreEqual(VolumeUnit.MilliLitre, volumeDifference.Unit);
    }

    [TestMethod]
    [Description("Subtracts two weight quantities where result is negative.")]
    public void Subtract_WeightUnits_ResultingNegative_ShouldReturnCorrectValue()
    {
      // Arrange
      var originalWeight = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms);
      var heavierWeight = new Quantity<WeightUnit>(5.0, WeightUnit.Kilograms);

      // Act
      var result = originalWeight.Subtract(heavierWeight);

      // Assert
      Assert.AreEqual(-3.0, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Verifies that subtraction is not commutative for quantity objects.")]
    public void Subtract_OrderMatters_ShouldProduceDifferentResults()
    {
      // Arrange
      var quantityA = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
      var quantityB = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);

      // Act
      var diffAB = quantityA.Subtract(quantityB).Value;
      var diffBA = quantityB.Subtract(quantityA).Value;

      // Assert
      Assert.AreNotEqual(diffAB, diffBA);
    }

    [TestMethod]
    [Description("Subtracting zero from a quantity should leave it unchanged.")]
    public void Subtract_ZeroOperand_ShouldReturnOriginalValue()
    {
      // Arrange
      var quantity = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
      var zeroLength = new Quantity<LengthUnit>(0.0, LengthUnit.Inches);

      // Act
      var result = quantity.Subtract(zeroLength);

      // Assert
      Assert.AreEqual(5.0, result.Value, Tolerance);
    }

    [TestMethod]
    [Description("Ensures original quantity remains unchanged after subtraction (immutability).")]
    public void Subtract_ShouldNotChangeOriginalQuantity()
    {
      // Arrange
      var originalLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
      var subLength = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

      // Act
      originalLength.Subtract(subLength);

      // Assert
      Assert.AreEqual(10.0, originalLength.Value, Tolerance);
    }

    [TestMethod]
    [Description("Checks addition followed by subtraction returns original value.")]
    public void Arithmetic_AddThenSubtract_ShouldReturnOriginalValue()
    {
      // Arrange
      var baseLength = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
      var addedLength = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

      // Act
      var finalResult = baseLength.Add(addedLength).Subtract(addedLength);

      // Assert
      Assert.AreEqual(10.0, finalResult.Value, Tolerance);
    }

    #endregion

    #region Division Tests

    [TestMethod]
    [Description("Divides two quantities of the same unit and returns scalar ratio.")]
    public void Divide_SameWeightUnits_ShouldReturnCorrectRatio()
    {
      // Arrange
      var totalWeight = new Quantity<WeightUnit>(10.0, WeightUnit.Kilograms);
      var unitWeight = new Quantity<WeightUnit>(2.0, WeightUnit.Kilograms);

      // Act
      var ratio = totalWeight.Divide(unitWeight);

      // Assert
      Assert.AreEqual(5.0, ratio, Tolerance);
    }

    [TestMethod]
    [Description("Divides quantities of different units and normalizes result correctly.")]
    public void Divide_LengthCrossUnits_ShouldReturnNormalizedRatio()
    {
      // Arrange
      var lengthInches = new Quantity<LengthUnit>(24.0, LengthUnit.Inches);
      var lengthFeet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

      // Act
      var ratio = lengthInches.Divide(lengthFeet);

      // Assert
      Assert.AreEqual(1.0, ratio, Tolerance);
    }

    [TestMethod]
    [Description("Dividing by a zero-value quantity should throw an ArithmeticException.")]
    public void Divide_ByZeroQuantity_ShouldThrowException()
    {
      // Arrange
      var volume1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
      var zeroVolume = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);

      // Act & Assert
      var ex = Assert.Throws<InvalidOperationException>(() => volume1.Divide(zeroVolume));

      // Optional: Verify exception message
      StringAssert.Contains(ex.Message, "Cannot divide by zero");
    }

    #endregion
  }
}