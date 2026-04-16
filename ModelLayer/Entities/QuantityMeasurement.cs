using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entity
{
  [Table("QuantityMeasurement")]
  public class QuantityMeasurement
  {
    [Key]
    public int Id { get; set; }

    // Foreign key — which user performed this operation
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public string Category { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public double Value1 { get; set; }
    public string Unit1 { get; set; } = string.Empty;
    public double? Value2 { get; set; }
    public string? Unit2 { get; set; }
    public double ResultValue { get; set; }
    public string ResultUnit { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
  }
}