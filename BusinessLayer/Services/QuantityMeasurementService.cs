using BusinessLayer.Factories;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using ModelLayer.Models;
using RepoLayer.Interfaces;

namespace BusinessLayer.Services
{
  /// <summary>
  /// Service class for performing operations on measurable quantities.
  /// Accepts DTOs from the presentation layer, performs operations using Quantity models,
  /// and returns results as DTOs.
  /// </summary>
  public class QuantityMeasurementService : IQuantityMeasurement
  {

    // Repository (Cache + Disk)
    private readonly IQuantityMeasurementRepository _repository;

    public QuantityMeasurementService(IQuantityMeasurementRepository repository)
    {
      _repository = repository;
    }

    // In-Memory cache for repeated conversions
    private Dictionary<string, double> _ConversionCache = new Dictionary<string, double>();

    /// <summary>
    /// Compares two quantities and returns true if they are equal.
    /// </summary>
    public bool Compare<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum
    {
      var measurable1 = MeasurableFactory.GetMeasurable(dto1.Unit);
      var measurable2 = MeasurableFactory.GetMeasurable(dto2.Unit);

      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, measurable1);
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, measurable2);

      return q1.Equals(q2);
    }

    /// <summary>
    /// Converts a numeric value from a source unit to a target unit.
    /// </summary>
    public QuantityDto<U> Convert<U>(QuantityDto<U> dto, U targetUnit) where U : Enum
    {
      var sourceMeasurable = MeasurableFactory.GetMeasurable(dto.Unit);
      var targetMeasurable = MeasurableFactory.GetMeasurable(targetUnit);

      var quantity = new Quantity<U>(dto.Value, dto.Unit, sourceMeasurable);
      var converted = quantity.ConvertTo(targetUnit, targetMeasurable);

      return new QuantityDto<U>
      {
        Value = converted.Value,
        Unit = converted.Unit
      };
    }

    /// <summary>
    /// Converts a numeric value from a source unit to a target unit directly.
    /// </summary>
    public double ConvertValue<U>(double value, U sourceUnit, U targetUnit) where U : Enum
    {
      string cacheKey = $"{value}-{sourceUnit}-{targetUnit}";

      // Check cache
      if (_ConversionCache.ContainsKey(cacheKey))
      {
        return _ConversionCache[cacheKey];
      }

      // Perform conversion
      var sourceMeasurable = MeasurableFactory.GetMeasurable(sourceUnit);
      var targetMeasurable = MeasurableFactory.GetMeasurable(targetUnit);

      var quantity = new Quantity<U>(value, sourceUnit, sourceMeasurable);
      var converted = quantity.ConvertTo(targetUnit, targetMeasurable);

      double result = converted.Value;

      // save to repository
      var entity = new QuantityMeasurementEntity
      {
        Id = Guid.NewGuid(),
        Value = value,
        SourceUnit = sourceUnit.ToString(),
        TargetUnit = targetUnit.ToString(),
        Result = result,
        CreatedAt = DateTime.Now
      };

      _repository.Save(entity);

      _ConversionCache[cacheKey] = result;

      return result;
    }

    /// <summary>
    /// Adds two quantities and returns the result as a DTO in the unit of the first quantity.
    /// </summary>
    public QuantityDto<U> Add<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum
    {
      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, MeasurableFactory.GetMeasurable(dto1.Unit));
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, MeasurableFactory.GetMeasurable(dto2.Unit));

      var result = q1.Add(q2);

      return new QuantityDto<U>
      {
        Value = result.Value,
        Unit = result.Unit
      };
    }

    /// <summary>
    /// Adds two quantities and returns the result as a DTO in a specified target unit.
    /// </summary>
    public QuantityDto<U> Add<U>(QuantityDto<U> dto1, QuantityDto<U> dto2, U targetUnit) where U : Enum
    {
      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, MeasurableFactory.GetMeasurable(dto1.Unit));
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, MeasurableFactory.GetMeasurable(dto2.Unit));
      var targetMeasurable = MeasurableFactory.GetMeasurable(targetUnit);

      var result = q1.Add(q2, targetUnit, targetMeasurable);

      return new QuantityDto<U>
      {
        Value = result.Value,
        Unit = result.Unit
      };
    }

    /// <summary>
    /// Subtracts the second quantity from the first and returns the result as a DTO.
    /// </summary>
    public QuantityDto<U> Subtract<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum
    {
      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, MeasurableFactory.GetMeasurable(dto1.Unit));
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, MeasurableFactory.GetMeasurable(dto2.Unit));

      var result = q1.Subtract(q2);

      return new QuantityDto<U>
      {
        Value = result.Value,
        Unit = result.Unit
      };
    }

    /// <summary>
    /// Subtracts the second quantity from the first and returns the result as a DTO in a specified target unit.
    /// </summary>
    public QuantityDto<U> Subtract<U>(QuantityDto<U> dto1, QuantityDto<U> dto2, U targetUnit) where U : Enum
    {
      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, MeasurableFactory.GetMeasurable(dto1.Unit));
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, MeasurableFactory.GetMeasurable(dto2.Unit));
      var targetMeasurable = MeasurableFactory.GetMeasurable(targetUnit);

      var result = q1.Subtract(q2, targetUnit, targetMeasurable);

      return new QuantityDto<U>
      {
        Value = result.Value,
        Unit = result.Unit
      };
    }

    /// <summary>
    /// Divides one quantity by another and returns the ratio as a double.
    /// </summary>
    public double Divide<U>(QuantityDto<U> dto1, QuantityDto<U> dto2) where U : Enum
    {
      var q1 = new Quantity<U>(dto1.Value, dto1.Unit, MeasurableFactory.GetMeasurable(dto1.Unit));
      var q2 = new Quantity<U>(dto2.Value, dto2.Unit, MeasurableFactory.GetMeasurable(dto2.Unit));

      return q1.Divide(q2);
    }

    // this method returns Conversion history
    public List<QuantityMeasurementEntity> GetConversionHistory()
    {
      return _repository.GetAll();
    }
  }
}