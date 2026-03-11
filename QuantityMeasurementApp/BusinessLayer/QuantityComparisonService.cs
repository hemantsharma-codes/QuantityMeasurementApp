using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.BusinessLayer
{
  /// <summary>
  /// UC10: Service class for performing operations on Quantity objects.
  /// This class delegates the actual logic to the Quantity model.
  /// </summary>
  public class QuantityComparisonService
  { 
    /// <summary>
    /// Compares two quantities of the same unit type.
    /// </summary>
    public bool Compare<U>(Quantity<U> q1, Quantity<U> q2) where U : Enum
    {
      if (q1 == null || q2 == null)
      {
        return false;
      }

      return q1.Equals(q2);
    }

    /// <summary>
    /// Converts a numeric value from one unit to another.
    /// </summary>
    public double DemonstrateConversion<U>(double value, U sourceUnit, U targetUnit) where U : Enum
    {
      Quantity<U> quantity = new Quantity<U>(value, sourceUnit);

      Quantity<U> converted = quantity.ConvertTo(targetUnit);

      return converted.Value;
    }

    /// <summary>
    /// Converts an existing quantity to the specified unit.
    /// </summary>
    public Quantity<U> DemonstrateConversion<U>(Quantity<U> source, U targetUnit) where U : Enum
    {
      return source.ConvertTo(targetUnit);
    }

    /// <summary>
    /// Adds two quantities and returns the result in the unit of the first quantity.
    /// </summary>
    public Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2) where U : Enum
    {
      return q1.Add(q2);
    }

    /// <summary>
    /// Adds two quantities and returns the result in the specified target unit.
    /// </summary>
    public Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit) where U : Enum
    {
      return q1.Add(q2, targetUnit);
    }
  }
}