using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// This is the main entry point for Quantity Measurement App
  /// </summary>
  public class QuantityMeasurementApp
  {

    /// <summary>
    /// Starts the Quantity Measurement Application
    /// by invoking the application menu.
    /// </summary>
    ///
    static void Main(string[] args)
    {
      QuantityComparisonService service = new QuantityComparisonService();
      QuantityMeasurementAppMenu menu = new QuantityMeasurementAppMenu(service);

      menu.StartApplication();
    }
  }
}