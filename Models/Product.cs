using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SponteAPI.Models
{
  public class Product
  {
    [Key]
    [JsonIgnore]
    public int ProductID { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public string CompleteDescription { get; set; }
    [Required]
    public double Height { get; set; }
    [Required]
    public double Width { get; set; }
    [Required]
    public double Length { get; set; }
    [Required]
    public double Weight { get; set; }
    [Required]
    public int BarCode { get; set; }
    [Required]
    public double Value { get; set; }
    [Required]
    public DateTime AcquisitionDate { get; set; }
    [Required]
    public string ProductImageLink { get; set; }
    [NotMapped]
    public IList<int> CategoryID { get; set; }
    [JsonIgnore]
    public ICollection<ProductCategory> ProductCategories { get; set; }

  }
}