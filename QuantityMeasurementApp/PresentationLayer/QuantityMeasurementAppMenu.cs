using QuantityMeasurementApp.BusinessLayer;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Console menu for the Quantity Measurement application.
  /// It accepts user input and calls service methods
  /// for comparison, conversion and addition of quantities.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu
  {
    // Generic measurement service used for all unit types
    private QuantityComparisonService _service;

    public QuantityMeasurementAppMenu(QuantityComparisonService service)
    {
      _service = service;
    }

    /// <summary>
    /// Starts the application and shows the main menu.
    /// </summary>
    public void StartApplication()
    {
      while (true)
      {
        Console.WriteLine("========= Quantity Measurement App ==========\n");
        Console.WriteLine("1. Length Measurement");
        Console.WriteLine("2. Weight Measurement");
        Console.WriteLine("3. Volume Measurement");
        Console.WriteLine("4. Temperature Measurement");
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

          case "3":
            StartVolume();
            break;

          case "4":
            StartTemperature();
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
    /// Menu for length operations.
    /// </summary>
    private void StartLength()
    {
      while (true)
      {
        Console.WriteLine("1. Length Comparison");
        Console.WriteLine("2. Length Conversion");
        Console.WriteLine("3. Length Addition");
        Console.WriteLine("4. Length Subtraction");
        Console.WriteLine("5. Length Division");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            HandleComparison<LengthUnit>();
            break;

          case "2":
            HandleConversion<LengthUnit>();
            break;

          case "3":
            HandleAddition<LengthUnit>();
            break;

          case "0":
            return;
        }
      }
    }

    /// <summary>
    /// Menu for weight operations.
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
            HandleComparison<WeightUnit>();
            break;

          case "2":
            HandleConversion<WeightUnit>();
            break;

          case "3":
            HandleAddition<WeightUnit>();
            break;

          case "0":
            return;
        }
      }
    }

    /// <summary>
    /// Menu for volume operations.
    /// </summary>
    private void StartVolume()
    {
      while (true)
      {
        Console.WriteLine("1. Volume Comparison");
        Console.WriteLine("2. Volume Conversion");
        Console.WriteLine("3. Volume Addition");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            HandleComparison<VolumeUnit>();
            break;

          case "2":
            HandleConversion<VolumeUnit>();
            break;

          case "3":
            HandleAddition<VolumeUnit>();
            break;

          case "0":
            return;
        }
      }
    }
    /// <summary>
    /// Menu for temperature operations.
    /// </summary>
    private void StartTemperature()
    {
      while (true)
      {
        Console.WriteLine("1. Temperature Comparison");
        Console.WriteLine("2. Temperature Conversion");
        Console.WriteLine("3. Temperature Addition");
        Console.WriteLine("4. Temperature Subtraction");
        Console.WriteLine("5. Temperature Division");
        Console.WriteLine("0. Exit");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
          case "1":
            HandleComparison<TemperatureUnit>();
            break;

          case "2":
            HandleConversion<TemperatureUnit>();
            break;

          case "3":
            HandleAddition<TemperatureUnit>();
            break;

          case "4":
            HandleSubtraction<TemperatureUnit>();
            break;

          case "5":
            HandleDivision<TemperatureUnit>();
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
    /// Generic method to compare two quantities.
    /// </summary>
    private void HandleComparison<U>() where U : struct, Enum
    {
      try
      {
        Console.WriteLine("Enter first value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter first unit:");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out U unit1);

        Console.WriteLine("Enter second value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second unit:");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out U unit2);

        Quantity<U> q1 = new Quantity<U>(value1, unit1);
        Quantity<U> q2 = new Quantity<U>(value2, unit2);

        bool result = _service.Compare(q1, q2);

        Console.WriteLine(result ? "Quantities are Equal" : "Quantities are not Equal");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Generic method for converting quantities.
    /// </summary>
    private void HandleConversion<U>() where U : struct, Enum
    {
      try
      {
        Console.WriteLine("Enter value:");
        double value = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter source unit:");
        string sourceInput = Console.ReadLine() ?? "";

        Console.WriteLine("Enter target unit:");
        string targetInput = Console.ReadLine() ?? "";

        Enum.TryParse(sourceInput, true, out U sourceUnit);
        Enum.TryParse(targetInput, true, out U targetUnit);

        double result = _service.DemonstrateConversion(value, sourceUnit, targetUnit);

        Console.WriteLine($"{value} {sourceUnit} = {result} {targetUnit}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Generic method to add two quantities.
    /// </summary>
    private void HandleAddition<U>() where U : struct, Enum
    {
      try
      {
        Console.WriteLine("Enter first value:");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter first unit:");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out U unit1);

        Console.WriteLine("Enter second value:");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second unit:");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out U unit2);

        Quantity<U> q1 = new Quantity<U>(value1, unit1);
        Quantity<U> q2 = new Quantity<U>(value2, unit2);

        Quantity<U> result = _service.DemonstrateAddition(q1, q2);

        Console.WriteLine($"After addition: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Generic method to subtract two quantities
    /// </summary>
    /// <typeparam name="U"></typeparam>
    private void HandleSubtraction<U>() where U : struct, Enum
    {
      try
      {
        Console.WriteLine("Enter first value : ");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter unit type of first value");
        string input1 = Console.ReadLine() ?? "";

        Enum.TryParse(input1, true, out U unit1);

        Console.WriteLine("Enter second value : ");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter unit type of second value");
        string input2 = Console.ReadLine() ?? "";

        Enum.TryParse(input2, true, out U unit2);

        Quantity<U> q1 = new Quantity<U>(value1, unit1);
        Quantity<U> q2 = new Quantity<U>(value2, unit2);

        Quantity<U> result = _service.DemonstrateSubtraction(q1, q2);

        Console.WriteLine($"After Subtraction: {result.Value}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// generic method to handle division of two quantities
    /// </summary>
    /// <typeparam name="U"></typeparam>
    private void HandleDivision<U>() where U : struct, Enum
    {
      Console.WriteLine("Enter first value : ");
      double value1 = Convert.ToDouble(Console.ReadLine());

      Console.WriteLine("Enter unit type of first value");
      string input1 = Console.ReadLine() ?? "";

      Enum.TryParse(input1, true, out U unit1);

      Console.WriteLine("Enter second value : ");
      double value2 = Convert.ToDouble(Console.ReadLine());

      Console.WriteLine("Enter unit type of second value");
      string input2 = Console.ReadLine() ?? "";

      Enum.TryParse(input2, true, out U unit2);

      Quantity<U> q1 = new Quantity<U>(value1, unit1);
      Quantity<U> q2 = new Quantity<U>(value2, unit2);

      double result = _service.DemonstarteDivision<U>(q1, q2);

      Console.WriteLine($"After division: {result}");
    }
  }
}