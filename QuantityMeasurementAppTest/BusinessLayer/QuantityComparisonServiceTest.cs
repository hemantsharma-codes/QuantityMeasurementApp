using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.BusinessLayer
{
    /// <summary>
    /// This test class is used to test the QuantityComparisonService.
    /// It checks whether the service correctly compares two Feet objects.
    /// </summary>
    [TestClass]
    public sealed class QuantityComparisonServiceTest
    {
        // Service object that will be tested in all test methods
        private QuantityComparisonService _service;

        /// <summary>
        /// This method runs before each test.
        /// It creates a fresh instance of the service so that
        /// every test runs independently.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityComparisonService();
        }

        /// <summary>
        /// This test checks the case when both feet values are same.
        /// The result should be true.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenFeetValuesAreSame()
        {
            // Arrange: create two Feet objects with same values
            Feet feet1 = new Feet(5.0);
            Feet feet2 = new Feet(5.0);

            // Act: compare both values using the service
            bool result = _service.DemonstrateFeetEquality(feet1, feet2);

            // Assert: expect true because values are equal
            Assert.IsTrue(result);
        }

        /// <summary>
        /// This test checks the case when both feet values are different.
        /// The result should be false.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenTwoValuesAreDifferent_ShouldReturnFalse()
        {
            // Arrange
            Feet feet1 = new Feet(5.0);
            Feet feet2 = new Feet(6.0);

            // Act
            bool result = _service.DemonstrateFeetEquality(feet1, feet2);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// This test checks the case when one of the values is null.
        /// The comparison should return false.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenOneValueIsNull_ReturnFalse()
        {
            // Arrange
            Feet feet1 = new Feet(5.0);
            Feet feet2 = null;

            // Act
            bool result = _service.DemonstrateFeetEquality(feet1, feet2);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// This test checks the case when both references point
        /// to the same object.
        /// The result should be true.
        /// </summary>
        [TestMethod]
        public void AreEqual_WhenReferenceIsSame_ShouldReturnTrue()
        {
            // Arrange
            Feet feet = new Feet(5.0);

            // Act
            bool result = _service.DemonstrateFeetEquality(feet, feet);

            // Assert
            Assert.IsTrue(result);
        }
    }
}