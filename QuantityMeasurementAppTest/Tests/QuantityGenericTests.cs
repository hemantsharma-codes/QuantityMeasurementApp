using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementAppTest.Tests
{
    /// <summary>
    /// Test cases to verify behaviour of the generic Quantity class (UC10).
    /// These tests check equality, conversion, addition and validation scenarios.
    /// </summary>
    [TestClass]
    public class QuantityGenericTests
    {

        [TestMethod]
        public void LengthEquality_FeetAndInches_ShouldBeEqual()
        {
            var oneFoot = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var twelveInches = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            Assert.IsTrue(oneFoot.Equals(twelveInches));
        }

        [TestMethod]
        public void WeightEquality_KgAndGrams_ShouldBeEqual()
        {
            var oneKg = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);
            var thousandGrams = new Quantity<WeightUnit>(1000.0, WeightUnit.Grams);

            Assert.IsTrue(oneKg.Equals(thousandGrams));
        }

        // Conversion tests

        [TestMethod]
        public void Convert_Length_FeetToInches()
        {
            var feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            var result = feet.ConvertTo(LengthUnit.Inches);

            Assert.AreEqual(12.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Inches, result.Unit);
        }

        [TestMethod]
        public void Convert_Weight_KgToGrams()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);

            var result = kg.ConvertTo(WeightUnit.Grams);

            Assert.AreEqual(1000.0, result.Value, 1e-6);
        }

        // Addition tests

        [TestMethod]
        public void Add_Length_FeetAndInches()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            var result = q1.Add(q2, LengthUnit.Feet);

            Assert.AreEqual(2.0, result.Value, 1e-6);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void Add_Weight_GramsAndKg()
        {
            var q1 = new Quantity<WeightUnit>(1000.0, WeightUnit.Grams);
            var q2 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);

            var result = q1.Add(q2, WeightUnit.Kilograms);

            Assert.AreEqual(2.0, result.Value, 1e-6);
        }

        // Cross category safety

        [TestMethod]
        public void LengthAndWeightComparison_ShouldReturnFalse()
        {
            var distance = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilograms);

            Assert.IsFalse(distance.Equals(weight));
        }

        // Constructor validation

        [TestMethod]
        public void Constructor_InvalidValue_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));

            Assert.Throws<ArgumentException>(() =>
                new Quantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Inches));
        }

        // Zero value equality

        [TestMethod]
        public void ZeroValue_DifferentUnits_ShouldBeEqual()
        {
            var zeroFeet = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            var zeroInches = new Quantity<LengthUnit>(0.0, LengthUnit.Inches);

            Assert.IsTrue(zeroFeet.Equals(zeroInches));
        }

        // Large value handling

        [TestMethod]
        public void LargeValue_Addition_ShouldRemainAccurate()
        {
            var largeKg = new Quantity<WeightUnit>(1000000.0, WeightUnit.Kilograms);
            var oneGram = new Quantity<WeightUnit>(1.0, WeightUnit.Grams);

            var result = largeKg.Add(oneGram, WeightUnit.Grams);

            Assert.AreEqual(1000000001.0, result.Value, 1e-6);
        }

        // Precision check

        [TestMethod]
        public void SmallDifference_ShouldStillBeEqual()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(1.0000001, LengthUnit.Inches);

            Assert.IsTrue(q1.Equals(q2));
        }

        // Negative values

        [TestMethod]
        public void Addition_WithNegativeValue()
        {
            var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(-5.0, LengthUnit.Inches);

            var result = q1.Add(q2);

            Assert.AreEqual(5.0, result.Value, 1e-6);
        }

        // Reflexive equality

        [TestMethod]
        public void Object_ShouldEqualItself()
        {
            var q1 = new Quantity<WeightUnit>(500.0, WeightUnit.Grams);

            Assert.IsTrue(q1.Equals(q1));
        }

        // Null comparison

        [TestMethod]
        public void Comparison_WithNull_ShouldReturnFalse()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            Assert.IsFalse(q1.Equals(null));
        }

        // HashCode consistency

        [TestMethod]
        public void EqualObjects_ShouldHaveSameHashCode()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
        }
    }
}