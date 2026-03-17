namespace QuantityMeasurementApp.PresentationLayer.Interfaces
{
  /// <summary>
  /// Defines the contract for the Quantity Measurement console menu.
  /// Responsible for starting the application and displaying
  /// the available measurement operations to the user.
  /// </summary>
  public interface IQuantityMeasurementAppMenu
  {
    /// <summary>
    /// Starts the quantity measurement application and
    /// displays the main menu to the user.
    /// </summary>
    void StartApplication();
  }
}