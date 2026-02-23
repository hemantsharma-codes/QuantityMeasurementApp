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
        public void AreEqual_WhenGivenTwoInchesHaveDifferent_ShouldReturnFalse()
        {
            Quantity q1 = new Quantity(11.0, QuantityType.Inches);
            Quantity q2 = new Quantity(10.0, QuantityType.Inches);

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
    }
}