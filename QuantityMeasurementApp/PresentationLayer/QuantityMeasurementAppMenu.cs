using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Provides the console-based user interface for the Quantity Measurement Application.
  /// This class handles menu navigation, user input, and interaction with the service layer.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {
    /// <summary>
    /// Service responsible for performing quantity comparison, conversion logic and Addition of Quantity.
    /// </summary>
    private QuantityComparisonService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuantityMeasurementAppMenu"/> class.
    /// Injects the required <see cref="QuantityComparisonService"/> dependency.
    /// </summary>
    /// <param name="service">Service used for quantity operations.</param>
    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      _service = service;
    }

    /// <summary>
    /// Starts the main application loop and displays the primary menu options.
    /// Allows the user to perform quantity comparison, quantity conversion, or exit the application.
    /// </summary>
    public void StartApplication()
    {
      Console.WriteLine("========= Quantity Measurement App ==========");

      while (true)
      {
        // Display main menu options
        Console.WriteLine("1. Quantity Comparison");
        Console.WriteLine("2. Quantity Conversion");
        Console.WriteLine("3. Quantity Addition");
        Console.WriteLine("0. Exit");

        Console.WriteLine("Enter your choice : ");
        string choice = Console.ReadLine() ?? "";

        // Handle user selection
        switch (choice)
        {
          case "1":
            HandleQuanityComparison();
            break;

          case "2":
            HandleQuantityConversion();
            break;
          case "3":
            HandleQuantityAddition();
            break;
          case "0":
            Console.WriteLine("Exiting....");
            return;

          default:
            Console.WriteLine("Please enter valid choice (1/2/0)");
            break;
        }
      }
    }

    /// <summary>
    /// Displays the comparison menu and allows the user to compare
    /// quantities between different measurement units.
    /// </summary>
    private void HandleQuanityComparison()
    {
      while (true)
      {
        // Display comparison options
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

        // Execute selected comparison scenario
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
    /// Reads two numeric values from the user, creates corresponding quantity objects,
    /// and performs equality comparison using the service layer.
    /// </summary>
    /// <param name="type1">Measurement unit of the first quantity.</param>
    /// <param name="type2">Measurement unit of the second quantity.</param>
    private void PerformComparisonInput(QuantityType type1, QuantityType type2)
    {
      try
      {
        // Read first quantity value from user
        Console.WriteLine($"Enter first value in: {type1}");
        double value1 = Convert.ToDouble(Console.ReadLine());

        // Read second quantity value from user
        Console.WriteLine($"Enter second value in: {type2}");
        double value2 = Convert.ToDouble(Console.ReadLine());

        // Create quantity objects
        Quantity q1 = new Quantity(value1, type1);
        Quantity q2 = new Quantity(value2, type2);

        // Perform comparison using the business layer
        bool result = _service.AreEqual(q1, q2);

        // Display comparison result
        Console.WriteLine(result ? "Quantities are Equal" : "Quantities are not Equal");
      }
      catch (FormatException ex)
      {
        // Handle invalid numeric input from the user
        Console.WriteLine($"Invalid input : {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handle any unexpected runtime errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Handles the quantity conversion operation.
    /// Reads value, source unit, and target unit from the user
    /// and displays the converted result.
    /// </summary>
    private void HandleQuantityConversion()
    {
      try
      {
        // Read quantity value
        Console.WriteLine("Enter value: ");
        double value = Convert.ToDouble(Console.ReadLine());

        // Read source unit from user
        Console.WriteLine("Enter Source Type (Feet/Inches/Yards/Centimeters)");
        string sourceInput = Console.ReadLine() ?? "";

        // Read target unit from user
        Console.WriteLine("Enter Target Type (Feet/Inches/Yards/Centimeters)");
        string targetInput = Console.ReadLine() ?? "";

        // Convert source unit string to enum
        if (!Enum.TryParse(sourceInput, true, out QuantityType sourceUnit) ||
            !Enum.IsDefined(typeof(QuantityType), sourceInput))
        {
          Console.WriteLine("Invalid Source Unit");
        }

        // Convert target unit string to enum
        if (!Enum.TryParse(targetInput, true, out QuantityType targetUnit) ||
            !Enum.IsDefined(typeof(QuantityType), targetInput))
        {
          Console.WriteLine("Invalid Target Unit");
        }

        // Perform conversion using the service layer
        double result = _service.DemonstrateQuantityConversion(value, sourceUnit, targetUnit);

        // Display conversion result
        Console.WriteLine($"{sourceInput} -> {targetInput} after conversion : {result}");
      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input
        Console.WriteLine($"Invalid Input: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles unexpected errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
    private void HandleQuantityAddition()
    {
      try
      {
        Console.WriteLine("Quantity Type: Inches, Feet, Yards, Centimeters");

        // Get first Quantity
        Console.WriteLine("Enter first value");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Quanity Type for first value");
        string input1 = Console.ReadLine() ?? "";

        // Convert input1 string to enum
        if (!Enum.TryParse(input1, true, out QuantityType type1) ||
            !Enum.IsDefined(typeof(QuantityType), input1))
        {
          Console.WriteLine("Invalid first value Unit");
        }

        Quantity q1 = new Quantity(value1, type1);

        // Get Second Quantity
        Console.WriteLine("Enter second value");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Quantity type for second value");
        string input2 = Console.ReadLine() ?? "";

        // Convert input2 string to enum
        if (!Enum.TryParse(input2, true, out QuantityType type2) ||
            !Enum.IsDefined(typeof(QuantityType), input2))
        {
          Console.WriteLine("Invalid second value Unit");
        }

        Quantity q2 = new Quantity(value2, type2);

        Quantity resultQuantity = null;

        Console.WriteLine("Explicit taregt Selection (Y/N)");
        string choice = Console.ReadLine() ?? "";

        if (choice == "Y")
        {
          Console.WriteLine("Enter the target unit");
          string input = Console.ReadLine() ?? "";

          // Convert input string to enum
          if (!Enum.TryParse(input, true, out QuantityType targetunit) ||
              !Enum.IsDefined(typeof(QuantityType), input))
          {
            Console.WriteLine("Invalid second value Unit");
          }

          resultQuantity = _service.DemonstrateQuantityAddition(q1, q2, targetunit);
        }
        else
        {
          resultQuantity = _service.DemonstrateQuantityAddition(q1, q2);
        }

        Console.WriteLine($"First Quantity: {q1}\n Second Quantity: {q2}\n After addition: {resultQuantity}");

      }
      catch (FormatException ex)
      {
        // Handles invalid numeric input
        Console.WriteLine($"Invalid Input: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Handles unexpected errors
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}