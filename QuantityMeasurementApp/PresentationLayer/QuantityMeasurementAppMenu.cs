using BusinessLayer.Interfaces;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using BusinessLayer.Factories;
using QuantityMeasurementApp.Interfaces;
using ModelLayer.DTOs;

namespace QuantityMeasurementApp.PresentationLayer
{
  /// <summary>
  /// Console menu for the Quantity Measurement application.
  /// It accepts user input and calls service methods
  /// for comparison, conversion and addition of quantities.
  /// </summary>
  public sealed class QuantityMeasurementAppMenu : IQuantityMeasurementAppMenu
  {
    // Generic measurement service used for all unit types
    private IQuantityMeasurement _service;

    public QuantityMeasurementAppMenu(IQuantityMeasurement service)
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
        Console.WriteLine("\n========= Quantity Measurement App ==========\n");
        Console.WriteLine("1. Length Measurement");
        Console.WriteLine("2. Weight Measurement");
        Console.WriteLine("3. Volume Measurement");
        Console.WriteLine("4. Temperature Measurement");
        Console.WriteLine("5. View Conversion History");
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

          case "5":
            ShowConversionHistory();
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

          case "4":
            HandleSubtraction<LengthUnit>();
            break;

          case "5":
            HandleDivision<LengthUnit>();
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

          default:
            Console.WriteLine("Invalid choice");
            break;
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

          default:
            Console.WriteLine("Invalid choice");
            break;
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

    private void ShowConversionHistory()
    {
      var history = _service.GetConversionHistory();

      if (history.Count == 0)
      {
        Console.WriteLine("No Conversion history found");
        return;
      }

      Console.WriteLine("\n==== Conversion History ====\n");
      foreach (var entity in history)
      {
        Console.WriteLine($"{entity.Value} {entity.SourceUnit} -> {entity.Result} {entity.TargetUnit} at {entity.CreatedAt}");
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

        IMeasurable measurable1 = MeasurableFactory.GetMeasurable(unit1);
        IMeasurable measurable2 = MeasurableFactory.GetMeasurable(unit2);

        var dto1 = new QuantityDto<U> { Value = value1, Unit = unit1 };
        var dto2 = new QuantityDto<U> { Value = value2, Unit = unit2 };

        bool result = _service.Compare(dto1, dto2);

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

        double result = _service.ConvertValue(value, sourceUnit, targetUnit);

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

        var dto1 = new QuantityDto<U> { Value = value1, Unit = unit1 };
        var dto2 = new QuantityDto<U> { Value = value2, Unit = unit2 };

        Console.WriteLine("Do you want to Convert the result into targetUnit (Y/N)");
        string choice = Console.ReadLine() ?? "";

        QuantityDto<U> resultDto;

        if (choice.Equals("Y"))
        {
          Console.WriteLine("Enter target unit");
          string input3 = Console.ReadLine() ?? "";

          Enum.TryParse(input3, true, out U targetUnit);

          resultDto = _service.Add(dto1, dto2, targetUnit);
        }
        else if (choice.Equals("N"))
        {
          resultDto = _service.Add(dto1, dto2);
        }
        else
        {
          Console.WriteLine("Try again! Please enter valid choice");
          return;
        }

        Console.WriteLine($"After addition: {resultDto.Value}\t{resultDto.Unit}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Generic method to subtract two quantities
    /// </summary>
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

        var dto1 = new QuantityDto<U> { Value = value1, Unit = unit1 };
        var dto2 = new QuantityDto<U> { Value = value2, Unit = unit2 };

        QuantityDto<U> result = _service.Subtract(dto1, dto2);

        Console.WriteLine($"After Subtraction: {result.Value}\t{result.Unit}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// generic method to handle division of two quantities
    /// </summary>
    private void HandleDivision<U>() where U : struct, Enum
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

        var dto1 = new QuantityDto<U> { Value = value1, Unit = unit1 };
        var dto2 = new QuantityDto<U> { Value = value2, Unit = unit2 };

        double result = _service.Divide(dto1, dto2);

        Console.WriteLine($"After division: {result}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}