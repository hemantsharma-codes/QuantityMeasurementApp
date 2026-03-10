// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using QuantityMeasurementApp.Model;
// using QuantityMeasurementApp.BusinessLayer;

// namespace QuantityMeasurementAppTest
// {
//   /// <summary>
//   /// Contains unit tests for weight comparison, conversion, and addition.
//   /// </summary>
//   [TestClass]
//   public class QuantityWeightTest
//   {
//     private QuantityWeightComparisonService _service;

//     /// <summary>
//     /// Creates the service object before each test runs.
//     /// </summary>
//     [TestInitialize]
//     public void Setup()
//     {
//       _service = new QuantityWeightComparisonService();
//     }

//     /// <summary>
//     /// Verifies that weights with the same value and unit are equal.
//     /// </summary>
//     [TestMethod]
//     public void GivenSameWeights_WhenCompared_ShouldReturnTrue()
//     {
//       QuantityWeight w1 = new QuantityWeight(1, WeightUnit.Kilograms);
//       QuantityWeight w2 = new QuantityWeight(1, WeightUnit.Kilograms);

//       bool result = _service.Compare(w1, w2);

//       Assert.IsTrue(result);
//     }

//     /// <summary>
//     /// Verifies that equivalent weights in different units are treated as equal.
//     /// </summary>
//     [TestMethod]
//     public void GivenEquivalentWeightsInDifferentUnits_WhenCompared_ShouldReturnTrue()
//     {
//       QuantityWeight w1 = new QuantityWeight(1000, WeightUnit.Grams);
//       QuantityWeight w2 = new QuantityWeight(1, WeightUnit.Kilograms);

//       bool result = _service.Compare(w1, w2);

//       Assert.IsTrue(result);
//     }

//     /// <summary>
//     /// Verifies that weight conversion returns the expected result.
//     /// </summary>
//     [TestMethod]
//     public void GivenWeight_WhenConverted_ShouldReturnConvertedValue()
//     {
//       double result = _service.DemonstrateQuantityConversion(1000, WeightUnit.Grams, WeightUnit.Kilograms);

//       Assert.AreEqual(1, result);
//     }

//     /// <summary>
//     /// Verifies that two weights can be added when they use the same unit.
//     /// </summary>
//     [TestMethod]
//     public void GivenTwoWeightsWithSameUnit_WhenAdded_ShouldReturnCorrectSum()
//     {
//       QuantityWeight w1 = new QuantityWeight(2, WeightUnit.Kilograms);
//       QuantityWeight w2 = new QuantityWeight(3, WeightUnit.Kilograms);

//       QuantityWeight result = _service.DemonstrateQuantityWeightAddition(w1, w2);

//       Assert.AreEqual(5, result.Value);
//       Assert.AreEqual(WeightUnit.Kilograms, result.Unit);
//     }

//     /// <summary>
//     /// Verifies that weights with different units can be added correctly.
//     /// </summary>
//     [TestMethod]
//     public void GivenWeightsWithDifferentUnits_WhenAdded_ShouldReturnCorrectResult()
//     {
//       QuantityWeight w1 = new QuantityWeight(1, WeightUnit.Kilograms);
//       QuantityWeight w2 = new QuantityWeight(500, WeightUnit.Grams);

//       QuantityWeight result = _service.DemonstrateQuantityWeightAddition(w1, w2);

//       Assert.AreEqual(1.5, result.Value);
//       Assert.AreEqual(WeightUnit.Kilograms, result.Unit);
//     }

//     /// <summary>
//     /// Verifies that weights can be added and returned in a specific target unit.
//     /// </summary>
//     [TestMethod]
//     public void GivenWeights_WhenAddedWithTargetUnit_ShouldReturnResultInTargetUnit()
//     {
//       QuantityWeight w1 = new QuantityWeight(1, WeightUnit.Kilograms);
//       QuantityWeight w2 = new QuantityWeight(500, WeightUnit.Grams);

//       QuantityWeight result = _service.DemonstrateQuantityWeightAddition(w1, w2, WeightUnit.Grams);

//       Assert.AreEqual(1500, result.Value);
//       Assert.AreEqual(WeightUnit.Grams, result.Unit);
//     }

//     /// <summary>
//     /// Verifies that passing null to Add throws an exception.
//     /// </summary>
//     [TestMethod]
//     public void GivenNullWeight_WhenAdded_ShouldThrowException()
//     {
//       QuantityWeight w1 = new QuantityWeight(1, WeightUnit.Kilograms);

//       Assert.Throws<ArgumentNullException>(() =>
//       {
//         _service.DemonstrateQuantityWeightAddition(w1, null);
//       });
//     }
//   }
// }