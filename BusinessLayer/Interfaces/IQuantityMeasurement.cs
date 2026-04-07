using ModelLayer.DTOs;
using ModelLayer.Entity;

namespace BusinessLayer.Interfaces
{
  public interface IQuantityMeasurement
  {
    // Operations — now accept userId so history is linked
    Task<ComparisonResultDto> CompareAsync(ComparisonRequestDto request, int? userId);
    Task<QuantityResultDto> ConvertAsync(ConversionRequestDto request, int? userId);
    Task<QuantityResultDto> AddAsync(AddRequestDto request, int? userId);
    Task<QuantityResultDto> SubtractAsync(SubtractRequestDto request, int? userId);
    Task<DivisionResultDto> DivideAsync(DivideRequestDto request, int? userId);

    // User history
    Task<IEnumerable<QuantityMeasurement>> GetMyHistoryAsync(int userId);
    Task<IEnumerable<QuantityMeasurement>> GetMyHistoryByOperationAsync(int userId, string operation);
    Task<IEnumerable<QuantityMeasurement>> GetMyHistoryByCategoryAsync(int userId, string category);
    Task<IEnumerable<QuantityMeasurement>> GetMyFilteredHistoryAsync(int userId, string? operation, string? category);

    // Admin history
    Task<IEnumerable<QuantityMeasurement>> GetAllHistoryAsync();
    Task<IEnumerable<QuantityMeasurement>> GetHistoryByUserIdAsync(int userId);
    Task<IEnumerable<QuantityMeasurement>> GetHistoryByOperationAsync(string operation);
    Task<IEnumerable<QuantityMeasurement>> GetHistoryByCategoryAsync(string category);
    Task<IEnumerable<QuantityMeasurement>> GetFilteredHistoryAsync(string? operation, string? category);

    // Delete
    Task DeleteHistoryAsync(int id);
    Task ClearHistoryAsync();
    Task ClearMyHistoryAsync(int userId);
  }
}