using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Handles user interaction for the Quantity Measurement application.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {
    private QuantityComparisonService _service;

    /// <summary>
    /// Initializes the menu with required comparison service.
    /// </summary>
    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      _service = service;
    }

    /// <summary>
    /// Starts the application and displays menu options.
    /// </summary>
    public void StartApplication()
    {
      Console.WriteLine("========= Quantity Measurement App ==========");

      while (true)
      {
        // Display available comparison options
        Console.WriteLine("1. Compare Feet with Feet Equality");
        Console.WriteLine("2. Compare Inches with Inches Equality");
        Console.WriteLine("3. Compare Yards with Yards Equality");
        Console.WriteLine("4. Compare Centimeters with Centimeters Equality");
        Console.WriteLine("5. Compare Feet with Inches Equality");
        Console.WriteLine("6. Compare Yards with Inches Equality");
        Console.WriteLine("7. Compare Centimeters with Inches Equality");
        Console.WriteLine("0. Exit");

        Console.WriteLine("Enter your choice : ");
        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            PerformComparisonInput(QuantityType.Feet, QuantityType.Feet);
            break;

          case "2":
            PerformComparisonInput(QuantityType.Inches, QuantityType.Inches);
            break;

          case "3":
            PerformComparisonInput(QuantityType.Yards, QuantityType.Yards);
            break;
          case "4":
            PerformComparisonInput(QuantityType.Centimeters, QuantityType.Centimeters);
            break;
          case "5":
            PerformComparisonInput(QuantityType.Feet, QuantityType.Inches);
            break;
          case "6":
            PerformComparisonInput(QuantityType.Yards, QuantityType.Inches);
            break;
          case "7":
            PerformComparisonInput(QuantityType.Centimeters, QuantityType.Inches);
            break;
          case "0":
            Console.WriteLine("Exiting...");
            return;

          default:
            Console.WriteLine("Invalid choice");
            break;
        }
      }
    }

    /// <summary>
    /// Reads user input, performs quantity comparison, and displays result.
    /// </summary>
    private void PerformComparisonInput(QuantityType type1, QuantityType type2)
    {
      try
      {
        // Read first quantity value
        Console.WriteLine($"Enter first value in: {type1}");
        double value1 = Convert.ToDouble(Console.ReadLine());

        // Read second quantity value
        Console.WriteLine($"Enter second value in: {type2}");
        double value2 = Convert.ToDouble(Console.ReadLine());

        // Create quantity objects
        Quantity q1 = new Quantity(value1, type1);
        Quantity q2 = new Quantity(value2, type2);

        // Compare quantities using service layer
        bool result = _service.AreEqual(q1, q2);

        Console.WriteLine(result ? "Quantities are Equal" : "Quantities are not Equal");
      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input
        Console.WriteLine($"Invalid input : {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles unexpected runtime errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}