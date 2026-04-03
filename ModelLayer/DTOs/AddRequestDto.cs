namespace ModelLayer.DTOs
{
  public class AddRequestDto
  {
    public required string QuantityType { get; set; }   // Length, Weight
    public double Value1 { get; set; }
    public double Value2 { get; set; }
    public required string Unit1 { get; set; }          // cm, meter
    public required string Unit2 { get; set; }
  }
}