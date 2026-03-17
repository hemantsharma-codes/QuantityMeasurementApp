using System;
using ModelLayer.Interfaces;

namespace ModelLayer.Models
{
  /// <summary>
  /// Represents a measurable quantity with a numeric value and a unit.
  /// Can perform arithmetic operations, unit conversion, and comparison.
  /// </summary>
  public sealed class Quantity<U> where U : Enum
  {
    private const double Tolerance = 1e-6; //0.000001

    /// <summary>
    /// Numeric value of the quantity.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Unit associated with the value.
    /// </summary>
    public U Unit { get; }

    private IMeasurable _measurable;

    /// <summary>
    /// Creates a new Quantity instance with value and unit.
    /// </summary>
    public Quantity(double value, U unit, IMeasurable measurable)
    {
      if (!double.IsFinite(value))
        throw new ArgumentException("Invalid value");

      Value = value;
      Unit = unit;
      _measurable = measurable ?? throw new ArgumentNullException(nameof(measurable));
    }

    private enum ArithmeticOperation { Add, Subtract, Divide }

    /// <summary>
    /// Utility method to perform arithmetic operations and return the result in base value.
    /// </summary>
    private double PerformArithmeticOperation(Quantity<U> other, ArithmeticOperation operation)
    {
      if (other == null) throw new ArgumentNullException(nameof(other));
      if (!this.Unit.GetType().Equals(other.Unit.GetType()))
        throw new ArgumentException("Incompatible measurement quantity");

      double baseValue1 = this.ToBase();
      double baseValue2 = other.ToBase();

      return operation switch
      {
        ArithmeticOperation.Add => baseValue1 + baseValue2,
        ArithmeticOperation.Subtract => baseValue1 - baseValue2,
        ArithmeticOperation.Divide => Math.Abs(baseValue2) < Tolerance
            ? throw new InvalidOperationException("Cannot divide by zero")
            : baseValue1 / baseValue2,
        _ => throw new InvalidOperationException(operation.ToString())
      };
    }

    /// <summary>
    /// Converts the current quantity value to the base unit.
    /// </summary>
    private double ToBase()
    {
      return _measurable.ConvertToBase(Value);
    }

    /// <summary>
    /// Converts the current quantity to the specified target unit.
    /// </summary>
    public Quantity<U> ConvertTo(U targetUnit, IMeasurable targetMeasurable)
    {
      double baseValue = ToBase();
      double converted = targetMeasurable.ConvertFromBase(baseValue);
      return new Quantity<U>(converted, targetUnit, targetMeasurable);
    }

    /// <summary>
    /// Adds another quantity and returns the result in the same unit as this quantity.
    /// </summary>
    public Quantity<U> Add(Quantity<U> other)
    {
      return Add(other, this.Unit, _measurable);
    }

    /// <summary>
    /// Adds another quantity and returns the result in the specified target unit.
    /// </summary>
    public Quantity<U> Add(Quantity<U> other, U targetUnit, IMeasurable targetMeasurable)
    {
      double sum = PerformArithmeticOperation(other, ArithmeticOperation.Add);
      double result = targetMeasurable.ConvertFromBase(sum);
      return new Quantity<U>(result, targetUnit, targetMeasurable);
    }

    /// <summary>
    /// Subtracts another quantity and returns the result in the same unit as this quantity.
    /// </summary>
    public Quantity<U> Subtract(Quantity<U> other)
    {
      return Subtract(other, this.Unit, _measurable);
    }

    /// <summary>
    /// Subtracts another quantity and returns the result in the specified target unit.
    /// </summary>
    public Quantity<U> Subtract(Quantity<U> other, U targetUnit, IMeasurable targetMeasurable)
    {
      double diffInBase = PerformArithmeticOperation(other, ArithmeticOperation.Subtract);
      double result = targetMeasurable.ConvertFromBase(diffInBase);
      return new Quantity<U>(result, targetUnit, targetMeasurable);
    }

    /// <summary>
    /// Divides this quantity by another quantity and returns the double result.
    /// </summary>
    public double Divide(Quantity<U> other)
    {
      return PerformArithmeticOperation(other, ArithmeticOperation.Divide);
    }

    /// <summary>
    /// Checks if two quantities are equal by comparing their base unit values.
    /// </summary>
    public override bool Equals(object? obj)
    {
      if (obj is not Quantity<U> other) return false;
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
      string symbol = _measurable.GetSymbol();
      return $"{Value} {symbol}";
    }
  }
}