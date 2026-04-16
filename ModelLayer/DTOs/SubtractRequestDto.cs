using System.ComponentModel.DataAnnotations;

public class SubtractRequestDto
{
  [Required]
  public string QuantityType { get; set; }

  [Range(0, double.MaxValue)]
  public double Value1 { get; set; }

  [Range(0, double.MaxValue)]
  public double Value2 { get; set; }

  [Required]
  public string Unit1 { get; set; }

  [Required]
  public string Unit2 { get; set; }

  [Required]
  public string ResultUnit { get; set; }
}