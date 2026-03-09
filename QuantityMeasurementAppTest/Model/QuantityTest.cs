namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Unit tests for Quantity equality and hash code behavior.
  /// Unit tests for the ConvertTo method of the Quantity model.
  /// Verifies correct unit conversion between supported units.
  /// </summary>
  [TestClass]
  public sealed class QuantityTest
  {
    /// <summary>
    /// Verifies equality when two feet quantities have the same value.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenTwoFeetHaveSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Feet);
      Quantity q2 = new Quantity(12.0, QuantityType.Feet);

      bool result = q1.Equals(q2);

      Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies inequality when two feet quantities have different values.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenTwoFeetHaveDifferentValue_ShouldReturnFalse()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Feet);
      Quantity q2 = new Quantity(10.0, QuantityType.Feet);

      bool result = q1.Equals(q2);

      Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies equality when two inch quantities have the same value.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenTwoInchesHaveSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Inches);
      Quantity q2 = new Quantity(12.0, QuantityType.Inches);

      bool result = q1.Equals(q2);

      Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies inequality when two inch quantities have different values.
    /// </summary>
    [TestMethod]
    public void TestEqulity_GivenTwoInchesHaveDifferentValue_ShouldReturnFalse()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Inches);
      Quantity q2 = new Quantity(10.0, QuantityType.Inches);

      bool result = q1.Equals(q2);

      Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies equality when two yards quantites have same values.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenTwoYardsHaveSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(2.0, QuantityType.Yards);
      Quantity q2 = new Quantity(2.0, QuantityType.Yards);

      Assert.IsTrue(q1.Equals(q2));
    }

    /// <summary>
    /// Verifies equality betwen yards and inches after conversion
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenYardsAndInchesHaveSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(1.0, QuantityType.Yards);
      Quantity q2 = new Quantity(36.0, QuantityType.Inches);

      Assert.IsTrue(q1.Equals(q2));
    }


    /// <summary>
    /// Verifies equality between feet and inches after unit conversion.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenFeetAndInchesWithSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(1.0, QuantityType.Feet);
      Quantity q2 = new Quantity(12.0, QuantityType.Inches);

      bool result = q1.Equals(q2);

      Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies equality between centimeters and inches after conversion
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenCentimetersAndInchesHaveSameValue_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(2.54, QuantityType.Centimeters);
      Quantity q2 = new Quantity(1.0, QuantityType.Inches);

      Assert.IsTrue(q1.Equals(q2));
    }

    /// <summary>
    /// Verifies inequality when feet and inches represent different values.
    /// </summary>
    [TestMethod]
    public void TestEquality_GivenFeetAndInchesWithDifferentValue_ShouldReturnFalse()
    {
      Quantity q1 = new Quantity(1.0, QuantityType.Feet);   // 12 inches
      Quantity q2 = new Quantity(10.0, QuantityType.Inches);

      bool result = q1.Equals(q2);

      Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies that comparison with null returns false.
    /// </summary>
    [TestMethod]
    public void TestEquality_WhenComparedWithNull_ShouldReturnFalse()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Feet);

      bool result = q1.Equals(null);

      Assert.IsFalse(result);
    }

    /// <summary>
    /// Verifies equality when the same object reference is compared.
    /// </summary>
    [TestMethod]
    public void TestEquality_WhenSameReferenceIsCompared_ShouldReturnTrue()
    {
      Quantity q1 = new Quantity(12.0, QuantityType.Feet);

      bool result = q1.Equals(q1);

      Assert.IsTrue(result);
    }

    /// <summary>
    /// Verifies equal quantities produce the same hash code.
    /// </summary>
    [TestMethod]
    public void GetHashCode_WhenTwoQuantitiesAreEqual_ShouldReturnSameHashCode()
    {
      Quantity q1 = new Quantity(1.0, QuantityType.Feet);
      Quantity q2 = new Quantity(12.0, QuantityType.Inches);

      int hash1 = q1.GetHashCode();
      int hash2 = q2.GetHashCode();

      Assert.AreEqual(hash1, hash2);
    }
    /// <summary>
    /// Verifies that feet are correctly converted to inches.
    /// </summary>
    [TestMethod]
    public void ConvertTo_FeetToInches_ReturnsCorrectValue()
    {
      Quantity quantity = new Quantity(1, QuantityType.Feet);

      double result = quantity.ConvertTo(QuantityType.Inches);

      // 1 foot = 12 inches
      Assert.AreEqual(12, result, 0.0001);
    }

    /// <summary>
    /// Verifies that inches are correctly converted to feet.
    /// </summary>
    [TestMethod]
    public void ConvertTo_InchesToFeet_ReturnsCorrectValue()
    {
      Quantity quantity = new Quantity(24, QuantityType.Inches);

      double result = quantity.ConvertTo(QuantityType.Feet);

      // 24 inches = 2 feet
      Assert.AreEqual(2, result, 0.0001);
    }

    /// <summary>
    /// Verifies that yards are correctly converted to inches.
    /// </summary>
    [TestMethod]
    public void ConvertTo_YardsToInches_ReturnsCorrectValue()
    {
      Quantity quantity = new Quantity(1, QuantityType.Yards);

      double result = quantity.ConvertTo(QuantityType.Inches);

      // 1 yard = 36 inches
      Assert.AreEqual(36, result, 0.0001);
    }

    /// <summary>
    /// Verifies that centimeters are correctly converted to inches.
    /// </summary>
    [TestMethod]
    public void ConvertTo_CentimetersToInches_ReturnsCorrectValue()
    {
      Quantity quantity = new Quantity(2.54, QuantityType.Centimeters);

      double result = quantity.ConvertTo(QuantityType.Inches);

      // 2.54 cm = 1 inch
      Assert.AreEqual(1, result, 0.0001);
    }

    /// <summary>
    /// Verifies that converting a unit to the same unit returns the same value.
    /// </summary>
    [TestMethod]
    public void ConvertTo_SameUnit_ReturnsSameValue()
    {
      Quantity quantity = new Quantity(10, QuantityType.Feet);

      double result = quantity.ConvertTo(QuantityType.Feet);

      Assert.AreEqual(10, result, 0.0001);
    }

    /// <summary>
    /// Verifies that two quantities with the same unit are added correctly.
    /// </summary>
    [TestMethod]
    public void Add_GivenTwoFeetQuantities_ShouldReturnCorrectSum()
    {
      Quantity q1 = new Quantity(2, QuantityType.Feet);
      Quantity q2 = new Quantity(3, QuantityType.Feet);

      Quantity result = q1.Add(q2);

      Assert.AreEqual(5, result.Value, 0.0001);
      Assert.AreEqual(QuantityType.Feet, result.Type);
    }

    /// <summary>
    /// Verifies that feet and inches are added correctly after conversion.
    /// </summary>
    [TestMethod]
    public void Add_GivenFeetAndInches_ShouldReturnCorrectSum()
    {
      Quantity q1 = new Quantity(1, QuantityType.Feet);   // 12 inches
      Quantity q2 = new Quantity(12, QuantityType.Inches); // 12 inches

      Quantity result = q1.Add(q2);

      Assert.AreEqual(2, result.Value, 0.0001);
      Assert.AreEqual(QuantityType.Feet, result.Type);
    }

    /// <summary>
    /// Verifies that yards and feet are added correctly after conversion.
    /// </summary>
    [TestMethod]
    public void Add_GivenYardsAndFeet_ShouldReturnCorrectSum()
    {
      Quantity q1 = new Quantity(1, QuantityType.Yards); // 3 feet
      Quantity q2 = new Quantity(3, QuantityType.Feet);

      Quantity result = q1.Add(q2);

      Assert.AreEqual(2, result.Value, 0.0001);
      Assert.AreEqual(QuantityType.Yards, result.Type);
    }

    /// <summary>
    /// Verifies that passing a null quantity throws an exception.
    /// </summary>
    [TestMethod]
    public void Add_WhenOtherQuantityIsNull_ShouldThrowException()
    {
      Quantity q1 = new Quantity(2, QuantityType.Feet);

      Assert.Throws<ArgumentNullException>(() => q1.Add(null));
    }

    /// <summary>
    /// Verifies that quantities can be added and converted to a target unit.
    /// </summary>
    [TestMethod]
    public void Add_GivenFeetAndInches_WithTargetUnitInches_ShouldReturnCorrectSum()
    {
      Quantity q1 = new Quantity(1, QuantityType.Feet);   // 12 inches
      Quantity q2 = new Quantity(12, QuantityType.Inches);

      Quantity result = q1.Add(q2, QuantityType.Inches);

      Assert.AreEqual(24, result.Value, 0.0001);
      Assert.AreEqual(QuantityType.Inches, result.Type);
    }

    /// <summary>
    /// Verifies that ConvertToBase converts the quantity to inches.
    /// </summary>
    [TestMethod]
    public void ConvertToBase_Feet_ShouldReturnInchesValue()
    {
      Quantity quantity = new Quantity(2, QuantityType.Feet);

      double result = quantity.ConvertToBase();

      // 2 feet = 24 inches
      Assert.AreEqual(24, result, 0.0001);
    }
  }
}