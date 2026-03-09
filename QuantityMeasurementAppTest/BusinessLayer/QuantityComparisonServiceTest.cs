using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.BusinessLayer
{
    /// <summary>
    /// Unit tests for QuantityComparisonService.
    /// </summary>
    [TestClass]
    public sealed class QuantityComparisonServiceTest
    {
        private QuantityComparisonService _service;

        /// <summary>
        /// Initializes service instance before each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityComparisonService();
        }

        /// <summary>
        /// Verifies equality when both quantities are feet with same value.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoFeetHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(12.0, QuantityType.Feet);
            Quantity q2 = new Quantity(12.0, QuantityType.Feet);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality when feet values are different.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoFeetHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(12.0, QuantityType.Feet);
            Quantity q2 = new Quantity(10.0, QuantityType.Feet);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies equality when both quantities are inches with same value.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoInchesHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(12.0, QuantityType.Inches);
            Quantity q2 = new Quantity(12.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality when inch values are different.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoInchesHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Inches);
            Quantity q2 = new Quantity(10.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }


        /// <summary>
        /// Verifies equality when Yards values are different.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoYardsHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Yards);
            Quantity q2 = new Quantity(11.0, QuantityType.Yards);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies equality when Centimeters values are same.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoCentimetersHaveSameValue_ShoudlReturnTrue()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Centimeters);
            Quantity q2 = new Quantity(11.0, QuantityType.Centimeters);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality when Centimeters values are different.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoCentimetersHaveDifferentValue_ShoudlReturnFalse()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Centimeters);
            Quantity q2 = new Quantity(10.0, QuantityType.Centimeters);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }
        /// <summary>
        /// Verifies inequality when Yards values are same.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenTwoYardsHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Yards);
            Quantity q2 = new Quantity(10.0, QuantityType.Yards);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }
        /// <summary>
        /// Verifies equality between feet and inches after conversion.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenFeetAndInchesHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(2, QuantityType.Feet);
            Quantity q2 = new Quantity(24.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality when feet and inches represent different values.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenFeetAndInchesHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(2.0, QuantityType.Feet);
            Quantity q2 = new Quantity(10.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies equality between yards and inches after conversion
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenYardsAndInchesHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(1.0, QuantityType.Yards);     // 36 inches
            Quantity q2 = new Quantity(36.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality between yards and inches after conversion
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenYardsAndInchesHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(1.0, QuantityType.Yards);     // 36 inches
            Quantity q2 = new Quantity(30.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies equality between centimeters and inches after conversion
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenCentimetersAndInchesHaveSameValue_ShouldReturnTrue()
        {
            Quantity q1 = new Quantity(2.54, QuantityType.Centimeters);
            Quantity q2 = new Quantity(1.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality between centimeters and inches after conversion
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenGivenCentimetersAndInchesHaveDifferentValue_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(5.0, QuantityType.Centimeters);
            Quantity q2 = new Quantity(1.0, QuantityType.Inches);

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies equality when both quantities are null.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenBothQuantitiesAreNull_ShouldReturnTrue()
        {
            Quantity q1 = null;
            Quantity q2 = null;

            bool result = _service.AreEqual(q1, q2);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifies inequality when one quantity is null.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenAnyQuantityIsNull_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(12.0, QuantityType.Inches);
            Quantity q2 = null;

            bool result = _service.AreEqual(q1, q2);

            Assert.IsFalse(result);
        }


        /// <summary>
        /// Verifies that 1 Foot converts to 12 Inches.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_FeetToInches_Returns12()
        {
            double result = _service.DemonstrateQuantityConversion(1, QuantityType.Feet, QuantityType.Inches);

            // 1 foot = 12 inches
            Assert.AreEqual(12, result, 0.0001);
        }

        /// <summary>
        /// Verifies that 12 Inches converts to 1 Foot.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_InchesToFeet_Returns1()
        {
            double result = _service.DemonstrateQuantityConversion(12, QuantityType.Inches, QuantityType.Feet);

            Assert.AreEqual(1, result, 0.0001);
        }

        /// <summary>
        /// Verifies that 1 Yard converts to 36 Inches.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_YardsToInches_Returns36()
        {
            double result = _service.DemonstrateQuantityConversion(1, QuantityType.Yards, QuantityType.Inches);

            Assert.AreEqual(36, result, 0.0001);
        }

        /// <summary>
        /// Verifies that 36 Inches converts to 1 Yard.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_InchesToYards_Returns1()
        {
            double result = _service.DemonstrateQuantityConversion(36, QuantityType.Inches, QuantityType.Yards);

            Assert.AreEqual(1, result, 0.0001);
        }

        /// <summary>
        /// Verifies that 2.54 Centimeters converts to 1 Inch.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_CentimetersToInches_Returns1()
        {
            double result = _service.DemonstrateQuantityConversion(2.54, QuantityType.Centimeters, QuantityType.Inches);

            Assert.AreEqual(1, result, 0.0001);
        }

        /// <summary>
        /// Verifies that converting a unit to the same unit returns the same value.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityConversion_SameUnit_ReturnsSameValue()
        {
            double result = _service.DemonstrateQuantityConversion(10, QuantityType.Feet, QuantityType.Feet);

            Assert.AreEqual(10, result, 0.0001);
        }

        /// <summary>
        /// Verifies that two quantities with the same unit can be added correctly.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityAddition_FeetWithFeet_ReturnsCorrectSum()
        {
            Quantity q1 = new Quantity(2, QuantityType.Feet);
            Quantity q2 = new Quantity(3, QuantityType.Feet);

            Quantity result = _service.DemonstrateQuantityAddition(q1, q2);

            // 2 feet + 3 feet = 5 feet
            Assert.AreEqual(5, result.Value, 0.0001);
            Assert.AreEqual(QuantityType.Feet, result.Type);
        }

        /// <summary>
        /// Verifies that quantities with different units are added correctly after conversion.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityAddition_FeetAndInches_ReturnsCorrectSum()
        {
            Quantity q1 = new Quantity(1, QuantityType.Feet);     // 12 inches
            Quantity q2 = new Quantity(12, QuantityType.Inches);  // 12 inches

            Quantity result = _service.DemonstrateQuantityAddition(q1, q2);

            // 12 inches + 12 inches = 24 inches -> 2 feet
            Assert.AreEqual(2, result.Value, 0.0001);
            Assert.AreEqual(QuantityType.Feet, result.Type);
        }

        /// <summary>
        /// Verifies that quantities are added correctly when units require conversion.
        /// </summary>
        [TestMethod]
        public void DemonstrateQuantityAddition_YardsAndFeet_ReturnsCorrectSum()
        {
            Quantity q1 = new Quantity(1, QuantityType.Yards); // 36 inches
            Quantity q2 = new Quantity(3, QuantityType.Feet);  // 36 inches

            Quantity result = _service.DemonstrateQuantityAddition(q1, q2);

            // 36 inches + 36 inches = 72 inches -> 2 yards
            Assert.AreEqual(2, result.Value, 0.0001);
            Assert.AreEqual(QuantityType.Yards, result.Type);
        }
    }
}