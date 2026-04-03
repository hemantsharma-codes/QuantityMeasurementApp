using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using RepoLayer.Data;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
  public class QuantityMeasurementRepository : IQuantityMeasurementRepository
  {
    private readonly AppDbContext _context;

    public QuantityMeasurementRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(QuantityMeasurement quantity)
    {
      await _context.AddAsync(quantity);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetAllAsync()
    {
      return await _context.QuantityMeasurement
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<QuantityMeasurement?> GetByIdAsync(int id)
    {
      return await _context.QuantityMeasurement
                           .AsNoTracking()
                           .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByOperationAsync(string operation)
    {
      return await _context.QuantityMeasurement
                           .Where(q => q.Operation == operation)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByCategoryAsync(string category)
    {
      return await _context.QuantityMeasurement
                           .Where(q => q.Category == category)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetFilteredAsync(string? operation, string? category)
    {
      var result = _context.QuantityMeasurement.AsNoTracking();

      if (!string.IsNullOrEmpty(operation))
        result = result.Where(q => q.Operation == operation);

      if (!string.IsNullOrEmpty(category))
        result = result.Where(q => q.Category == category);

      return await result.ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var entity = await _context.QuantityMeasurement.FindAsync(id);
      if (entity != null)
      {
        _context.QuantityMeasurement.Remove(entity);
        await _context.SaveChangesAsync();
      }
    }

    public async Task DeleteAllAsync()
    {
      await _context.QuantityMeasurement.ExecuteDeleteAsync();
      await _context.SaveChangesAsync();
    }
  }
}