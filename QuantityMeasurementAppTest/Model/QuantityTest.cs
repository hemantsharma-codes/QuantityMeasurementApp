namespace QuantityMeasurementApp.Model
{
  /// <summary>
  /// Unit tests for Quantity equality and hash code behavior.
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
  }
}