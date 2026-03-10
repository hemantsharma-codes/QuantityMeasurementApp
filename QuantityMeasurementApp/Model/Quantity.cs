using System;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp.Models
{
  /// <summary>
  /// Represents a measurable quantity with a numeric value and a unit.
  /// This generic class can work with different measurement categories
  /// such as LengthUnit or WeightUnit.
  /// </summary>
  public sealed class Quantity<U> where U : Enum
  {
    // Used while comparing two quantities to avoid floating-point precision issues
    private const double Tolerance = 1e-6;

    /// <summary>
    /// Numeric value of the quantity.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Unit associated with the value (Feet, Inches, Kg, Gram, etc.)
    /// </summary>
    public U Unit { get; }

    /// <summary>
    /// Creates a new Quantity instance with value and unit.
    /// </summary>
    public Quantity(double value, U unit)
    {
      if (unit == null)
      {
        throw new ArgumentNullException(nameof(unit));
      }

      // Validate the numeric value
      if (!double.IsFinite(value) || double.IsNaN(value))
      {
        throw new ArgumentException("Invalid value");
      }

      Value = value;
      Unit = unit;
    }

    /// <summary>
    /// Converts the current quantity into its base unit.
    /// For example: Feet/Inches/Yards → Inches, Kg/Gram → Gram.
    /// </summary>
    private double ToBase()
    {
      if (Unit is LengthUnit l)
      {
        return QuantityConverter.ConvertToBase(l, Value);
      }

      if (Unit is WeightUnit w)
      {
        return WeightUnitConverter.ConvertToBase(w, Value);
      }

      throw new InvalidOperationException("Unsupported unit type");
    }

    /// <summary>
    /// Converts the current quantity to the specified target unit.
    /// </summary>
    public Quantity<U> ConvertTo(U targetUnit)
    {
      double baseValue = this.ToBase();
      double converted;

      if (targetUnit is LengthUnit l)
      {
        converted = QuantityConverter.ConvertFromBase(l, baseValue);
      }
      else if (targetUnit is WeightUnit w)
      {
        converted = WeightUnitConverter.ConvertFromBase(w, baseValue);
      }
      else
      {
        throw new InvalidOperationException("Unsupported unit type");
      }

      return new Quantity<U>(converted, targetUnit);
    }

    /// <summary>
    /// Adds another quantity and returns the result
    /// in the same unit as the current object.
    /// </summary>
    public Quantity<U> Add(Quantity<U> other)
    {
      return Add(other, Unit);
    }

    /// <summary>
    /// Adds two quantities and converts the result
    /// into the specified target unit.
    /// </summary>
    public Quantity<U> Add(Quantity<U> other, U targetUnit)
    {
      if (other == null)
      {
        throw new ArgumentNullException(nameof(other));
      }

      // Convert both quantities to base unit before addition
      double sum = this.ToBase() + other.ToBase();
      double result;

      if (targetUnit is LengthUnit l)
      {
        result = QuantityConverter.ConvertFromBase(l, sum);
      }
      else if (targetUnit is WeightUnit w)
      {
        result = WeightUnitConverter.ConvertFromBase(w, sum);
      }
      else
      {
        throw new InvalidOperationException("Unsupported unit type");
      }

      return new Quantity<U>(result, targetUnit);
    }

    /// <summary>
    /// Checks if two quantities are equal by comparing
    /// their base unit values.
    /// </summary>
    public override bool Equals(object? obj)
    {
      if (obj is not Quantity<U> other)
      {
        return false;
      }

      return Math.Abs(this.ToBase() - other.ToBase()) < Tolerance;
    }

    /// <summary>
    /// Returns hash code based on the base unit value.
    /// </summary>
    public override int GetHashCode()
    {
      return ToBase().GetHashCode();
    }

    /// <summary>
    /// Returns a readable string representation of the quantity.
    /// Example: "5 ft" or "200 g".
    /// </summary>
    public override string ToString()
    {
      string symbol = "";

      if (Unit is LengthUnit l)
      {
        symbol = QuantityConverter.GetSymbol(l);
      }
      else if (Unit is WeightUnit w)
      {
        symbol = WeightUnitConverter.GetSymbol(w);
      }

      return $"{Value} {symbol}";
    }
  }
}