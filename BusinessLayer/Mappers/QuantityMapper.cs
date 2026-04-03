using ModelLayer.DTOs;
using ModelLayer.Models;
using ModelLayer.Interfaces;

namespace BusinessLayer.Mappers
{
  /// <summary>
  /// Provides mapping methods between Quantity DTOs and domain models.
  /// </summary>
  public static class QuantityMapper
  {
    /// <summary>
    /// Converts a QuantityDto into a Quantity domain object.
    /// </summary>
    public static Quantity<U> ToModel<U>(QuantityDto<U> dto, IMeasurable measurable) where U : Enum
    {
      return new Quantity<U>(dto.Value, dto.Unit, measurable);
    }

    /// <summary>
    /// Converts a Quantity domain object into a QuantityDto.
    /// </summary>
    public static QuantityDto<U> ToDto<U>(Quantity<U> quantity) where U : Enum
    {
      return new QuantityDto<U>
      {
        Value = quantity.Value,
        Unit = quantity.Unit
      };
    }
  }
}