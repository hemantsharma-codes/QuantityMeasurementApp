using ModelLayer.Entity;

namespace RepoLayer.Interfaces
{
  public interface IQuantityMeasurementRepository
  {
    Task AddAsync(QuantityMeasurement entity);

    Task<IEnumerable<QuantityMeasurement>> GetAllAsync();

    Task<QuantityMeasurement?> GetByIdAsync(int id);

    Task<IEnumerable<QuantityMeasurement>> GetByOperationAsync(string operation);

    Task<IEnumerable<QuantityMeasurement>> GetByCategoryAsync(string category);

    Task<IEnumerable<QuantityMeasurement>> GetFilteredAsync(string? operation, string? category);

    Task DeleteAsync(int id);
    Task DeleteAllAsync();
  }
}