using System.Text.Json;
using ModelLayer.Entity;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
  public class QuantityMeasurementRepository : IQuantityMeasurementRepository
  {
    private static QuantityMeasurementRepository _quantityMeasurementCacheInstance;
    private List<QuantityMeasurementEntity> _storage;
    private static readonly object lockObject = new object();

    private QuantityMeasurementRepository()
    {
      _storage = new List<QuantityMeasurementEntity>();
      LoadFromDisk();
    }

    public static QuantityMeasurementRepository GetInstance()
    {
      lock (lockObject)
      {
        if (_quantityMeasurementCacheInstance == null)
        {
          _quantityMeasurementCacheInstance = new QuantityMeasurementRepository();
        }
      }
      return _quantityMeasurementCacheInstance;
    }

    public void Save(QuantityMeasurementEntity entity)
    {
      _storage.Add(entity);
      SaveToDisk(entity);
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
      return _storage;
    }

    private void SaveToDisk(QuantityMeasurementEntity entity)
    {
      string json = JsonSerializer.Serialize(entity);
      File.AppendAllText("measurement.txt", json + Environment.NewLine);
    }

    private void LoadFromDisk()
    {
      if (!File.Exists("measurement.txt")) return;

      var lines = File.ReadAllLines("measurement.txt");

      foreach (var line in lines)
      {
        var entity = JsonSerializer.Deserialize<QuantityMeasurementEntity>(line);
        _storage.Add(entity);
      }
    }

  }
}