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
      // Validate the numeric value
      if (!double.IsFinite(value))
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

      if (Unit is VolumeUnit v)
      {
        return VolumeUnitConverter.ConvertToBase(v, Value);
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
      else if (targetUnit is VolumeUnit v)
      {
        converted = VolumeUnitConverter.ConvertFromBase(v, baseValue);
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
      return Add(other, this.Unit);
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
      else if (targetUnit is VolumeUnit v)
      {
        result = VolumeUnitConverter.ConvertFromBase(v, sum);
      }
      else
      {
        throw new InvalidOperationException("Unsupported unit type");
      }

      return new Quantity<U>(result, targetUnit);
    }

    /// <summary>
    /// Subtract another quantity and returns the result
    /// in the same unit as the current object.
    /// </summary>
    public Quantity<U> Subtract(Quantity<U> other)
    {
      return Subtract(other, this.Unit);
    }

    /// <summary>
    /// Subtract two quantities and converts the result
    /// into the specified target unit.
    /// </summary>
    public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
    {
      Validate(other);
      double diffInBase = this.ToBase() - other.ToBase();
      double result;

      if (targetUnit is LengthUnit l)
      {
        result = QuantityConverter.ConvertFromBase(l, diffInBase);
      }
      else if (targetUnit is WeightUnit w)
      {
        result = WeightUnitConverter.ConvertFromBase(w, diffInBase);
      }
      else if (targetUnit is VolumeUnit v)
      {
        result = VolumeUnitConverter.ConvertFromBase(v, diffInBase);
      }
      else
      {
        throw new InvalidOperationException("Unsupported unit type");
      }
      return new Quantity<U>(result, targetUnit);
    }

    /// <summary>
    /// Perform division Operation and return the result in double value
    /// </summary>
    public double Divide(Quantity<U> other)
    {
      Validate(other);
      double divisorInBase = other.ToBase();

      if (Math.Abs(divisorInBase) < Tolerance)
      {
        throw new ArithmeticException("Cannot divide by zero value quanity");
      }

      return this.ToBase() / divisorInBase;
    }

    // This method performs the validation for quantity
    private void Validate(Quantity<U> other)
    {
      if (other == null)
      {
        throw new ArgumentNullException(nameof(other));
      }
      if (!this.Unit.GetType().Equals(other.Unit.GetType()))
      {
        throw new ArgumentException("Incompatible measurement categories");
      }
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
      return this.ToBase().GetHashCode();
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
      else if (Unit is VolumeUnit v)
      {
        symbol = VolumeUnitConverter.GetSymbol(v);
      }

      return $"{Value} {symbol}";
    }
  }
}