using ModelLayer.DTOs;
using ModelLayer.Entity;

namespace BusinessLayer.Mappers
{
  public static class QuantityEntityMapper
  {

    // For binary operations (Add, Subtract, Divide)
    public static QuantityMeasurementEntity ToEntity<U>(
        QuantityDto<U> dto1,
        QuantityDto<U> dto2,
        double result,
        string operation,
        U resultUnit
    )
    where U : Enum
    {
      return new QuantityMeasurementEntity
      {
        Id = Guid.NewGuid(),
        Operation = operation,
        Value1 = dto1.Value,
        Unit1 = dto1.Unit.ToString(),
        Value2 = dto2.Value,
        Unit2 = dto2.Unit.ToString(),
        ResultValue = result,
        ResultUnit = resultUnit.ToString(),
        CreatedAt = DateTime.Now
      };
    }

    // For Conversion

    public static QuantityMeasurementEntity ToEntity<U>(
      double value,
      U sourceUnit,
      double result,
      U targetUnit
      ) where U : Enum
    {
      return new QuantityMeasurementEntity
      {
        Id = Guid.NewGuid(),
        Operation = "Convert",
        Value1 = value,
        Unit1 = sourceUnit.ToString(),
        Value2 = 0,
        Unit2 = null,
        ResultValue = result,
        ResultUnit = targetUnit.ToString(),
        CreatedAt = DateTime.Now
      };
    }
  }
}