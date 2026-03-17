namespace ModelLayer.Entity
{
  public class QuantityMeasurementEntity
  {
    public Guid Id { get; set; }
    public double Value { get; set; }
    public string? SourceUnit { get; set; }
    public string? TargetUnit { get; set; }
    public double Result { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}