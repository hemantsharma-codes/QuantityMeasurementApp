using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.Model
{
  /// <summary>
  /// This test class is used to test the Feet model class.
  /// It checks whether the Equals method works correctly
  /// for different comparison cases.
  /// </summary>
  [TestClass]
  public sealed class FeetTest
  {
    /// <summary>
    /// This test checks when two Feet objects have the same value.
    /// The comparison should return true.
    /// </summary>
    [TestMethod]
    public void TestEquality_SameValue()
    {
      // Arrange: create two Feet objects with same value
      Feet feet1 = new Feet(5.0);
      Feet feet2 = new Feet(5.0);

      // Act: compare both objects
      bool result = feet1.Equals(feet2);

      // Assert: values are same, so result should be true
      Assert.IsTrue(result);
    }

    /// <summary>
    /// This test checks when two Feet objects have different values.
    /// The comparison should return false.
    /// </summary>
    [TestMethod]
    public void TestEqualtiy_DifferentValue()
    {
      // Arrange
      Feet feet1 = new Feet(5.0);
      Feet feet2 = new Feet(6.0);

      // Act
      bool result = feet1.Equals(feet2);

      // Assert
      Assert.IsFalse(result);
    }

    /// <summary>
    /// This test checks the comparison when the second object is null.
    /// The result should be false.
    /// </summary>
    [TestMethod]
    public void TestEquality_NullComparison()
    {
      // Arrange
      Feet feet1 = new Feet(5.0);
      Feet feet2 = null;

      // Act
      bool result = feet1.Equals(feet2);

      // Assert
      Assert.IsFalse(result);
    }

    /// <summary>
    /// This test checks the case when both references point
    /// to the same Feet object.
    /// The result should be true.
    /// </summary>
    [TestMethod]
    public void TestEquality_SameReference()
    {
      // Arrange
      Feet feet = new Feet(5.0);

      // Act
      bool result = feet.Equals(feet);

      // Assert
      Assert.IsTrue(result);
    }
  }
}