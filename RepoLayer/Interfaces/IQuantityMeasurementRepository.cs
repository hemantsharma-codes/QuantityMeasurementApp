using ModelLayer.Entity;

namespace RepoLayer.Interfaces
{
  public interface IQuantityMeasurementRepository
  {
    void Save(QuantityMeasurementEntity entity);

    List<QuantityMeasurementEntity> GetAll();
  }
}