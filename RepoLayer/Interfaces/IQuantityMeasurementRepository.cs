using ModelLayer.Entity;

namespace RepoLayer.Interfaces
{
  public interface IQuantityMeasurementRepository
  {
    Task AddAsync(QuantityMeasurement entity);

    // User-specific
    Task<IEnumerable<QuantityMeasurement>> GetByUserIdAsync(int userId);
    Task<IEnumerable<QuantityMeasurement>> GetByUserIdAndOperationAsync(int userId, string operation);
    Task<IEnumerable<QuantityMeasurement>> GetByUserIdAndCategoryAsync(int userId, string category);
    Task<IEnumerable<QuantityMeasurement>> GetFilteredByUserIdAsync(int userId, string? operation, string? category);

    // Admin — all users
    Task<IEnumerable<QuantityMeasurement>> GetAllAsync();
    Task<IEnumerable<QuantityMeasurement>> GetAllByUserIdAsync(int userId);  // admin fetches specific user
    Task<IEnumerable<QuantityMeasurement>> GetByOperationAsync(string operation);
    Task<IEnumerable<QuantityMeasurement>> GetByCategoryAsync(string category);
    Task<IEnumerable<QuantityMeasurement>> GetFilteredAsync(string? operation, string? category);

    Task<QuantityMeasurement?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task DeleteAllAsync();
    Task DeleteByUserIdAsync(int userId);
  }
}