namespace RepoLayer.Config
{
  public static class DatabaseConfig
  {
    private static readonly string _connectionString =
            "Server=localhost\\SQLEXPRESS;" +
            "Database=QuantityMeasurementDB;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";
    public static string GetConnection()
    {
      return _connectionString;
    }
  }
}