using BusinessLayer.Helpers;
using BusinessLayer.Interfaces;
using BusinessLayer.Mappers;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using ModelLayer.Models;
using RepoLayer.Interfaces;

namespace BusinessLayer.Services
{
  public class QuantityMeasurementService : IQuantityMeasurement
  {
    private readonly IQuantityMeasurementRepository _repository;

    public QuantityMeasurementService(IQuantityMeasurementRepository repository)
    {
      _repository = repository;
    }
    public async Task<ComparisonResultDto> CompareAsync(ComparisonRequestDto request)
    {
      var (u1, m1, cat) = UnitParser.Parse(request.QuantityType, request.Unit1);
      var (u2, m2, _) = UnitParser.Parse(request.QuantityType, request.Unit2);

      dynamic q1 = new Quantity<Enum>(request.Value1, u1, m1);
      dynamic q2 = new Quantity<Enum>(request.Value2, u2, m2);

      bool result = q1.Equals(q2);

      await _repository.AddAsync(new QuantityMeasurement
      {
        Category = cat,
        Operation = "Compare",
        Value1 = request.Value1,
        Unit1 = request.Unit1,
        Value2 = request.Value2,
        Unit2 = request.Unit2,
        ResultValue = result ? 1 : 0,
        ResultUnit = "",
        CreatedAt = DateTime.UtcNow
      });

      return new ComparisonResultDto { AreEqual = result };
    }

    public async Task<QuantityResultDto> ConvertAsync(ConversionRequestDto request)
    {
      var (srcEnum, srcMeas, cat) = UnitParser.Parse(request.QuantityType, request.SourceUnit);
      var (tgtEnum, tgtMeas, _) = UnitParser.Parse(request.QuantityType, request.TargetUnit);

      dynamic quantity = new Quantity<Enum>(request.Value, srcEnum, srcMeas);
      var result = quantity.ConvertTo(tgtEnum, tgtMeas);

      await _repository.AddAsync(new QuantityMeasurement
      {
        Category = cat,
        Operation = "Convert",
        Value1 = request.Value,
        Unit1 = request.SourceUnit,
        ResultValue = result.Value,
        ResultUnit = request.TargetUnit,
        CreatedAt = DateTime.UtcNow
      });

      return new QuantityResultDto
      {
        Value = result.Value,
        UnitSymbol = request.TargetUnit
      };
    }
    public async Task<QuantityResultDto> AddAsync(AddRequestDto request)
    {
      var (u1, m1, cat) = UnitParser.Parse(request.QuantityType, request.Unit1);
      var (u2, m2, _) = UnitParser.Parse(request.QuantityType, request.Unit2);

      dynamic q1 = new Quantity<Enum>(request.Value1, u1, m1);
      dynamic q2 = new Quantity<Enum>(request.Value2, u2, m2);

      var result = q1.Add(q2);

      await _repository.AddAsync(new QuantityMeasurement
      {
        Category = cat,
        Operation = "Add",
        Value1 = request.Value1,
        Unit1 = request.Unit1,
        Value2 = request.Value2,
        Unit2 = request.Unit2,
        ResultValue = result.Value,
        ResultUnit = request.Unit1,
        CreatedAt = DateTime.UtcNow
      });

      return new QuantityResultDto
      {
        Value = result.Value,
        UnitSymbol = request.Unit1
      };
    }

    public async Task<QuantityResultDto> SubtractAsync(SubtractRequestDto request)
    {
      var (u1, m1, cat) = UnitParser.Parse(request.QuantityType, request.Unit1);
      var (u2, m2, _) = UnitParser.Parse(request.QuantityType, request.Unit2);

      dynamic q1 = new Quantity<Enum>(request.Value1, u1, m1);
      dynamic q2 = new Quantity<Enum>(request.Value2, u2, m2);

      var result = q1.Subtract(q2);

      await _repository.AddAsync(new QuantityMeasurement
      {
        Category = cat,
        Operation = "Subtract",
        Value1 = request.Value1,
        Unit1 = request.Unit1,
        Value2 = request.Value2,
        Unit2 = request.Unit2,
        ResultValue = result.Value,
        ResultUnit = request.ResultUnit,
        CreatedAt = DateTime.UtcNow
      });

      return new QuantityResultDto
      {
        Value = result.Value,
        UnitSymbol = request.ResultUnit
      };
    }

    public async Task<DivisionResultDto> DivideAsync(DivideRequestDto request)
    {
      var (u1, m1, cat) = UnitParser.Parse(request.QuantityType, request.Unit1);
      var (u2, m2, _) = UnitParser.Parse(request.QuantityType, request.Unit2);

      dynamic q1 = new Quantity<Enum>(request.Value1, u1, m1);
      dynamic q2 = new Quantity<Enum>(request.Value2, u2, m2);

      double result = q1.Divide(q2);

      await _repository.AddAsync(new QuantityMeasurement
      {
        Category = cat,
        Operation = "Divide",
        Value1 = request.Value1,
        Unit1 = request.Unit1,
        Value2 = request.Value2,
        Unit2 = request.Unit2,
        ResultValue = result,
        ResultUnit = "",
        CreatedAt = DateTime.UtcNow
      });

      return new DivisionResultDto { Ratio = result };
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetHistoryAsync()
    {
      return await _repository.GetAllAsync();
    }
    public async Task<IEnumerable<QuantityMeasurement>> GetHistoryByOperationAsync(string operation)
    {
      return await _repository.GetByOperationAsync(operation);
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetHistoryByCategoryAsync(string category)
    {
      return await _repository.GetByCategoryAsync(category);
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetFilteredHistoryAsync(string? operation, string? category) 
    {
      return await _repository.GetFilteredAsync(operation, category);
    }
    public async Task DeleteHistoryAsync(int id)
    {
      await _repository.DeleteAsync(id);
    }
    public async Task ClearHistoryAsync()
    {
      await _repository.DeleteAllAsync();
    }
  }
}