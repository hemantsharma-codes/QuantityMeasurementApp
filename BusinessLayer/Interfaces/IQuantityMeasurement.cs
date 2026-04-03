using ModelLayer.DTOs;
using ModelLayer.Entity;

namespace BusinessLayer.Interfaces
{
  public interface IQuantityMeasurement
  {
    Task<ComparisonResultDto> CompareAsync(ComparisonRequestDto request);
    Task<QuantityResultDto> ConvertAsync(ConversionRequestDto request);
    Task<QuantityResultDto> AddAsync(AddRequestDto request);
    Task<QuantityResultDto> SubtractAsync(SubtractRequestDto request);
    Task<DivisionResultDto> DivideAsync(DivideRequestDto request);

    Task<IEnumerable<QuantityMeasurement>> GetHistoryAsync();
    Task<IEnumerable<QuantityMeasurement>> GetHistoryByOperationAsync(string operation);
    Task<IEnumerable<QuantityMeasurement>> GetHistoryByCategoryAsync(string category);
    Task<IEnumerable<QuantityMeasurement>> GetFilteredHistoryAsync(string? operation, string? category);

    Task DeleteHistoryAsync(int id);
    Task ClearHistoryAsync();
  }
}