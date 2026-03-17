namespace ModelLayer.DTOs
{
  public class QuantityDto<U> where U : Enum
  {
    public double Value { get; set; }
    public U Unit { get; set; }
  }
}