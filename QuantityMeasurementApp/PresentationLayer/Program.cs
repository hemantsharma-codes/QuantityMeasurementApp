using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using QuantityMeasurementApp.Interfaces;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Application entry point for the Quantity Measurement system.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// Creates required dependencies and starts the application menu.
    /// </summary>
    public static void Main(string[] args)
    {
      // Initialize repository layer
      IQuantityMeasurementRepository repository = new QuantityMeasurementRepository();

      // Initialize business service
      IQuantityMeasurement service = new QuantityMeasurementService(repository);

      // Initialize presentation layer menu
      IQuantityMeasurementAppMenu menu = new QuantityMeasurementAppMenu(service);


      // Start user interaction
      menu.StartApplication();
    }
  }
}