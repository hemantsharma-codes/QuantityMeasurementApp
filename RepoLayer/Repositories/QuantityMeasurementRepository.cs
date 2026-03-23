using Microsoft.Data.SqlClient;
using ModelLayer.Entity;
using RepoLayer.Config;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
  public class QuantityMeasurementRepository : IQuantityMeasurementRepository
  {
    public void Save(QuantityMeasurementEntity entity)
    {
      using SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnection());
      connection.Open();

      string query = @"INSERT INTO QuantityMeasurements 
      (Id, Operation, Value1, Unit1, Value2, Unit2, ResultValue, ResultUnit, CreatedAt) 
      VALUES 
      (@Id, @Operation, @Value1, @Unit1, @Value2, @Unit2, @ResultValue, @ResultUnit, @CreatedAt)";

      SqlCommand command = new SqlCommand(query, connection);

      command.Parameters.AddWithValue("@Id", entity.Id);
      command.Parameters.AddWithValue("@Operation", entity.Operation);
      command.Parameters.AddWithValue("@Value1", entity.Value1);
      command.Parameters.AddWithValue("@Unit1", (object?)entity.Unit1 ?? DBNull.Value);
      command.Parameters.AddWithValue("@Value2", entity.Value2);
      command.Parameters.AddWithValue("@Unit2", (object?)entity.Unit2 ?? DBNull.Value);
      command.Parameters.AddWithValue("@ResultValue", entity.ResultValue);
      command.Parameters.AddWithValue("@ResultUnit", entity.ResultUnit);
      command.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);

      command.ExecuteNonQuery();
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
      List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();

      using SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnection());
      connection.Open();

      string query = "SELECT * FROM QuantityMeasurements";

      SqlCommand command = new SqlCommand(query, connection);

      using SqlDataReader reader = command.ExecuteReader();

      while (reader.Read())
      {
        QuantityMeasurementEntity entity = new QuantityMeasurementEntity
        {
          Id = reader.GetGuid(0),
          Operation = reader.GetString(1),
          Value1 = reader.GetDouble(2),
          Unit1 = reader.IsDBNull(3) ? null : reader.GetString(3),
          Value2 = reader.GetDouble(4),
          Unit2 = reader.IsDBNull(5) ? null : reader.GetString(5),
          ResultValue = reader.GetDouble(6),
          ResultUnit = reader.GetString(7),
          CreatedAt = reader.GetDateTime(8)
        };

        entities.Add(entity);
      }

      return entities;
    }
  }
}