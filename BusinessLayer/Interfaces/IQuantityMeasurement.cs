using ModelLayer.DTOs;
using ModelLayer.Entity;
using RepoLayer.Repositories;

namespace BusinessLayer.Interfaces
{
  /// <summary>
  /// Defines operations for comparing, converting, and performing arithmetic
  /// on measurable quantities using DTOs.
  /// </summary>
  public interface IQuantityMeasurement
  {
    /// <summary>
    /// Compares two quantities and returns true if they are equal.
    /// </summary>
    bool Compare<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum;

    /// <summary>
    /// Converts a numeric value from a source unit to a target unit.
    /// </summary>
    double ConvertValue<U>(double value, U sourceUnit, U targetUnit) where U : Enum;

    /// <summary>
    /// Converts a quantity DTO from its current unit to a target unit and returns a new DTO.
    /// </summary>
    QuantityDto<U> Convert<U>(QuantityDto<U> dto, U targetUnit) where U : Enum;

    /// <summary>
    /// Adds two quantities and returns the result as a DTO in the first quantity's unit.
    /// </summary>
    QuantityDto<U> Add<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum;

    /// <summary>
    /// Adds two quantities and returns the result as a DTO in the specified target unit.
    /// </summary>
    QuantityDto<U> Add<U>(QuantityDto<U> dto1, QuantityDto<U> dto2, U targetUnit) where U : Enum;

    /// <summary>
    /// Subtracts the second quantity from the first and returns the result as a DTO.
    /// </summary>
    QuantityDto<U> Subtract<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum;

    /// <summary>
    /// Subtracts two quantities and returns the result as a DTO in the specified unit.
    /// </summary>
    QuantityDto<U> Subtract<U>(QuantityDto<U> dto1, QuantityDto<U> dto2, U targetUnit) where U : Enum;

    /// <summary>
    /// Divides one quantity by another and returns the ratio as a double.
    /// </summary>
    double Divide<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum;

    List<QuantityMeasurementEntity> GetConversionHistory();
  }
}