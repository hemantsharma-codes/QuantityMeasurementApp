using System.Runtime.CompilerServices;
using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Handles user interaction for the Quantity Measurement Application.
  /// Display menu options, accepts user input, and delegates
  /// business operations to the service layer.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {

    // Service layer dependency used to perfrom quantity comparisons
    private QuantityComparisonService _service;

    /// <summary>
    /// Initialize the new instance of the <see cref="QuantityMeasurementAppMenu"/> class.
    /// </summary>
    /// <param name="service">
    /// Service responsible for the quantity comparison business logic.
    /// </param>
    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      this._service = service;
    }


    /// <summary>
    /// Starts the application menu loop and process user choices.
    /// </summary>
    public void StartApplication()
    {
      System.Console.WriteLine("========= Quantity Measurement App ==========");

      // Keep the application running until the user chooses to exit
      while (true)
      {
        Console.WriteLine("1. Compare Feet Equality");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            FeetComparisonInput();
            break;
          case "0":
            System.Console.WriteLine("Exiting...");
            return;
          default:
            System.Console.WriteLine("Please enter valid choice");
            break;
        }
      }
    }

    /// <summary>
    /// Accepts user input for feet values, Performs equality comparison,
    /// and display the result.
    /// </summary>
    private void FeetComparisonInput()
    {
      try
      {
        Console.WriteLine("Enter first feet value: ");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second feet value: ");
        double value2 = Convert.ToDouble(Console.ReadLine());

        // Create two Feet value objects
        Feet f1 = new Feet(value1);
        Feet f2 = new Feet(value2);

        bool result = _service.DemonstrateFeetEquality(f1, f2);

        Console.WriteLine(result ? "Both are Equal" : "Both are not Equal");
      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input from user
        Console.WriteLine($"Invalid input: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles any unexpected runtime errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}