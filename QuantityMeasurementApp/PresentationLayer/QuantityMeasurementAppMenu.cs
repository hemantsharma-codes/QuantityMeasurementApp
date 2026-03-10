using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Console menu for the Quantity Measurement application.
  /// It reads user input and calls the appropriate service methods.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {
    // Generic measurement service
    private QuantityComparisonService _service;

    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      _service = service;
    }

    /// <summary>
    /// Entry point for the menu system.
    /// Shows the main options and redirects to the selected module.
    /// </summary>
    public void StartApplication()
    {
      while (true)
      {
        Console.WriteLine("========= Quantity Measurement App ==========\n");
        Console.WriteLine("1. Length Measurement");
        Console.WriteLine("2. Weight Measurement");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            StartLength();
            break;

          case "2":
            StartWeight();
            break;

          case "0":
            return;

          default:
            Console.WriteLine("Invalid choice");
            break;
        }
      }
    }

    /// <summary>
    /// Menu for length related operations.
    /// </summary>
    private void StartLength()
    {
      while (true)
      {
        Console.WriteLine("1. Quantity Comparison");
        Console.WriteLine("2. Quantity Conversion");
        Console.WriteLine("3. Quantity Addition");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            HandleQuantityComparison();
            break;

          case "2":
            HandleQuantityConversion();
            break;

          case "3":
            HandleQuantityAddition();
            break;

          case "0":
            return;

          default:
            Console.WriteLine("Invalid choice");
            break;
        }
      }
    }

    /// <summary>
    /// Menu for weight related operations.
    /// </summary>
    private void StartWeight()
    {
      while (true)
      {
        Console.WriteLine("1. Weight Comparison");
        Console.WriteLine("2. Weight Conversion");
        Console.WriteLine("3. Weight Addition");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            HandleWeightComparison();
            break;

          case "2":
            HandleWeightConversion();
            break;

          case "3":
            HandleWeightAddition();
            break;

          case "0":
            return;

          default:
            Console.WriteLine("Invalid choice");
            break;
        }
      }
    }

    /// <summary>
    /// Reads two values and checks if they represent the same length.
    /// </summary>
    private void PerformComparisonInput(LengthUnit unit1, LengthUnit unit2)
    {
      try
      {
        Console.WriteLine($"Enter first value in {unit1}");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine($"Enter second value in {unit2}");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(value1, unit1);
        Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(value2, unit2);

        bool result = _service.Compare(q1, q2);

        Console.WriteLine(result ? "Quantities are Equal" : "Quantities are not Equal");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Shows predefined comparison cases for length units.
    /// </summary>
    private void HandleQuantityComparison()
    {
      while (true)
      {
        Console.WriteLine("1. Compare Feet with Feet");
        Console.WriteLine("2. Compare Inches with Inches");
        Console.WriteLine("3. Compare Yards with Yards");
        Console.WriteLine("4. Compare Centimeters with Centimeters");
        Console.WriteLine("5. Compare Feet with Inches");
        Console.WriteLine("6. Compare Yards with Inches");
        Console.WriteLine("7. Compare Centimeters with Inches");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            PerformComparisonInput(LengthUnit.Feet, LengthUnit.Feet);
            break;
          case "2":
            PerformComparisonInput(LengthUnit.Inches, LengthUnit.Inches);
            break;
          case "3":
            PerformComparisonInput(LengthUnit.Yards, LengthUnit.Yards);
            break;
          case "4":
            PerformComparisonInput(LengthUnit.Centimeters, LengthUnit.Centimeters);
            break;
          case "5":
            PerformComparisonInput(LengthUnit.Feet, LengthUnit.Inches);
            break;
          case "6":
            PerformComparisonInput(LengthUnit.Yards, LengthUnit.Inches);
            break;
          case "7":
            PerformComparisonInput(LengthUnit.Centimeters, LengthUnit.Inches);
            break;
          case "0":
            return;
        }
      }
    }

    private void HandleQuantityConversion()
    {
      try
      {
        Console.WriteLine("Enter value:");
        double value = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Source Unit (Feet/Inches/Yards/Centimeters)");
        string sourceInput = Console.ReadLine() ?? "";

        Console.WriteLine("Enter Target Unit (Feet/Inches/Yards/Centimeters)");
        string targetInput = Console.ReadLine() ?? "";

        Enum.TryParse(sourceInput, true, out LengthUnit sourceUnit);
        Enum.TryParse(targetInput, true, out LengthUnit targetUnit);

        double result = _service.DemonstrateConversion(value, sourceUnit, targetUnit);

        Console.WriteLine($"Converted value: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    private void HandleQuantityAddition()
    {
      try
      {
        Console.WriteLine("Enter first value");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter first unit");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out LengthUnit unit1);

        Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(value1, unit1);

        Console.WriteLine("Enter second value");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second unit");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out LengthUnit unit2);

        Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(value2, unit2);

        Quantity<LengthUnit> result = _service.DemonstrateAddition(q1, q2);

        Console.WriteLine($"After addition: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    private void HandleWeightComparison()
    {
      try
      {
        Console.WriteLine("Enter first value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter first unit:");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out WeightUnit unit1);

        Console.WriteLine("Enter second value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second unit:");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out WeightUnit unit2);

        Quantity<WeightUnit> w1 = new Quantity<WeightUnit>(value1, unit1);
        Quantity<WeightUnit> w2 = new Quantity<WeightUnit>(value2, unit2);

        bool result = _service.Compare(w1, w2);

        Console.WriteLine(result ? "Weights are Equal" : "Weights are not Equal");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    private void HandleWeightConversion()
    {
      try
      {
        Console.WriteLine("Enter value:");
        double value = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter source unit:");
        string sourceInput = Console.ReadLine() ?? "";

        Console.WriteLine("Enter target unit:");
        string targetInput = Console.ReadLine() ?? "";

        Enum.TryParse(sourceInput, true, out WeightUnit sourceUnit);
        Enum.TryParse(targetInput, true, out WeightUnit targetUnit);

        double result = _service.DemonstrateConversion(value, sourceUnit, targetUnit);

        Console.WriteLine($"{value} {sourceUnit} = {result} {targetUnit}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    private void HandleWeightAddition()
    {
      try
      {
        Console.WriteLine("Enter first value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter first unit:");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out WeightUnit unit1);

        Console.WriteLine("Enter second value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second unit:");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out WeightUnit unit2);

        Quantity<WeightUnit> w1 = new Quantity<WeightUnit>(value1, unit1);
        Quantity<WeightUnit> w2 = new Quantity<WeightUnit>(value2, unit2);

        Quantity<WeightUnit> result = _service.DemonstrateAddition(w1, w2);

        Console.WriteLine($"After addition: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}