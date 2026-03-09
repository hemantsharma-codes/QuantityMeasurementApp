using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Application entry point for the Quantity Measurement system.
  /// </summary>
  public class QuantityMeasurementApp
  {
    /// <summary>
    /// Creates required dependencies and starts the application menu.
    /// </summary>
    public static void Main(string[] args)
    {
      // Initialize business service
      QuantityComparisonService service = new QuantityComparisonService();
      QuantityWeightComparisonService weightService = new QuantityWeightComparisonService();

      // Initialize presentation layer menu
      QuantityMeasurementAppMenu menu = new QuantityMeasurementAppMenu(service, weightService);


      // Start user interaction
      menu.StartApplication();
    }
  }
}