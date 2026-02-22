using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.Model
{
  public sealed class InchesTest
  {

    /// <summary>
    /// This method checkes the case when both values are same.
    /// The result should be true
    /// </summary>

    [TestMethod]
    public void TestEquality_SameValue()
    {
      // Arrange
      Inches inch1 = new Inches(5.0);
      Inches inch2 = new Inches(5.0);

      // Act
      bool result = inch1.Equals(inch2);

      // Assert
      Assert.IsTrue(result);
    }

    /// <summary>
    /// This test checkes the case when both values are different.
    /// The result should be false.
    /// </summary>
    [TestMethod]
    public void TestEquality_DifferentValue()
    {
      // Arrange
      Inches inch1 = new Inches(5.0);
      Inches inch2 = new Inches(6.0);

      // Act
      bool result = inch1.Equals(inch2);

      // Assert
      Assert.IsFalse(result);
    }

    /// <summary>
    /// This test checkes the case when one object has the null value.
    /// The result should be false.
    /// </summary>
    [TestMethod]
    public void TestEquality_NullComparison()
    {
      // Arrange
      Inches inch1 = new Inches(5.0);
      Inches inch2 = null;

      // Act
      bool result = inch1.Equals(inch2);

      // Assert
      Assert.IsFalse(result);
    }

    /// <summary>
    /// This test checkes the case when both objects are pointing to same referece
    /// The result should be true
    /// </summary>
    [TestMethod]
    public void TestEquality_SameReference()
    {
      // Arrange
      Inches inch = new Inches(5.0);

      // Act
      bool result = inch.Equals(inch);

      // Assert
      Assert.IsTrue(result);
    }
  }
}