using ModelLayer.DTOs;
using ModelLayer.Entity;

namespace BusinessLayer.Mappers
{
  public static class QuantityMeasurementMapper
  {
    // For binary operations (Add, Subtract, Divide)
    public static QuantityMeasurement ToEntity<U>(
        QuantityDto<U> dto1,
        QuantityDto<U> dto2,
        double result,
        string operation,
        U resultUnit,
        string category
    )
    where U : Enum
    {
      return new QuantityMeasurement
      {
        Category = category,
        Operation = operation,
        Value1 = dto1.Value,
        Unit1 = dto1.Unit.ToString(),
        Value2 = dto2.Value,
        Unit2 = dto2.Unit.ToString(),
        ResultValue = result,
        ResultUnit = resultUnit.ToString(),
        CreatedAt = DateTime.UtcNow
      };
    }

    // For Conversion
    public static QuantityMeasurement ToEntity<U>(
      double value,
      U sourceUnit,
      double result,
      U targetUnit,
      string category
    ) where U : Enum
    {
      return new QuantityMeasurement
      {
        Category = category,
        Operation = "Convert",
        Value1 = value,
        Unit1 = sourceUnit.ToString(),
        Value2 = null,
        Unit2 = null,
        ResultValue = result,
        ResultUnit = targetUnit.ToString(),
        CreatedAt = DateTime.UtcNow
      };
    }
  }
}