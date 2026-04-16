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

    // User-specific queries 

    public async Task<IEnumerable<QuantityMeasurement>> GetByUserIdAsync(int userId)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.UserId == userId)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByUserIdAndOperationAsync(int userId, string operation)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.UserId == userId && q.Operation == operation)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByUserIdAndCategoryAsync(int userId, string category)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.UserId == userId && q.Category == category)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetFilteredByUserIdAsync(int userId, string? operation, string? category)
    {
      var query = _context.QuantityMeasurement
          .Where(q => q.UserId == userId)
          .AsNoTracking();

      if (!string.IsNullOrEmpty(operation))
        query = query.Where(q => q.Operation == operation);

      if (!string.IsNullOrEmpty(category))
        query = query.Where(q => q.Category == category);

      return await query.OrderByDescending(q => q.CreatedAt).ToListAsync();
    }

    // Admin queries (all users)

    public async Task<IEnumerable<QuantityMeasurement>> GetAllAsync()
    {
      return await _context.QuantityMeasurement
          .Include(q => q.User)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetAllByUserIdAsync(int userId)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.UserId == userId)
          .Include(q => q.User)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByOperationAsync(string operation)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.Operation == operation)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetByCategoryAsync(string category)
    {
      return await _context.QuantityMeasurement
          .Where(q => q.Category == category)
          .AsNoTracking()
          .OrderByDescending(q => q.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<QuantityMeasurement>> GetFilteredAsync(string? operation, string? category)
    {
      var query = _context.QuantityMeasurement.AsNoTracking();

      if (!string.IsNullOrEmpty(operation))
        query = query.Where(q => q.Operation == operation);

      if (!string.IsNullOrEmpty(category))
        query = query.Where(q => q.Category == category);

      return await query.OrderByDescending(q => q.CreatedAt).ToListAsync();
    }

    public async Task<QuantityMeasurement?> GetByIdAsync(int id)
    {
      return await _context.QuantityMeasurement
          .AsNoTracking()
          .FirstOrDefaultAsync(q => q.Id == id);
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
    }

    public async Task DeleteByUserIdAsync(int userId)
    {
      await _context.QuantityMeasurement
          .Where(q => q.UserId == userId)
          .ExecuteDeleteAsync();
    }
  }
}