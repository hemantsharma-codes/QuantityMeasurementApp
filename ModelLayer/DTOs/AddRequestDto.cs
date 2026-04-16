using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs
{
  public class AddRequestDto
  {
    [Required]
    public string QuantityType { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Value1 { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Value2 { get; set; }

    [Required]
    public string Unit1 { get; set; }

    [Required]
    public string Unit2 { get; set; }
  }
}