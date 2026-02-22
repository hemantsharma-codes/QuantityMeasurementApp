using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Handles user interaction for the Quantity Measurement Application.
  /// Displays menu options, takes user input, and calls
  /// the service layer to perform comparisons.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {
    // Service layer dependency used to perform quantity comparisons
    private readonly QuantityComparisonService _service;

    /// <summary>
    /// Creates a new instance of the application menu.
    /// </summary>
    /// <param name="service">
    /// Service that contains the business logic for quantity comparison.
    /// </param>
    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      _service = service;
    }

    /// <summary>
    /// Starts the application menu and keeps running
    /// until the user chooses to exit.
    /// </summary>
    public void StartApplication()
    {
      Console.WriteLine("========= Quantity Measurement App ==========");

      // Run the menu continuously until exit option is selected
      while (true)
      {
        Console.WriteLine("1. Compare Feet Equality");
        Console.WriteLine("2. Compare Inches Equality");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? string.Empty;

        switch (choice)
        {
          case "1":
            FeetComparisonInput();
            break;

          case "2":
            InchesComparisonInput();
            break;

          case "0":
            Console.WriteLine("Exiting...");
            return;

          default:
            Console.WriteLine("Please enter a valid choice.");
            break;
        }
      }
    }

    /// <summary>
    /// Takes user input for feet values,
    /// compares them using the service layer,
    /// and displays the result.
    /// </summary>
    private void FeetComparisonInput()
    {
      try
      {
        Console.WriteLine("Enter first feet value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second feet value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        // Create Feet objects from user input
        Feet feet1 = new Feet(value1);
        Feet feet2 = new Feet(value2);

        // Perform comparison through service layer
        bool result = _service.DemonstrateFeetEquality(feet1, feet2);

        Console.WriteLine(result ? "Both values are equal" : "Both values are not equal");
      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input
        Console.WriteLine($"Invalid input: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles unexpected runtime errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Takes user input for inches values,
    /// compares them, and displays the result.
    /// </summary>
    private void InchesComparisonInput()
    {
      try
      {
        Console.WriteLine("Enter first inch value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second inch value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        // Create Inches objects from user input
        Inches inches1 = new Inches(value1);
        Inches inches2 = new Inches(value2);

        // Compare inch values
        bool result = _service.DemonstrateInchesEquality(inches1, inches2);

        Console.WriteLine(result
          ? "Both inch values are equal"
          : "Both inch values are not equal");
      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input
        Console.WriteLine($"Invalid input: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles unexpected runtime errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}