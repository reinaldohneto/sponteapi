using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SponteAPI.Models
{
  public class Category
  {
    [Key]
    [JsonIgnore]
    public int CategoryID { get; set; }
    [MaxLength(100)]
    public string Title { get; set; }
    [JsonIgnore]
    public ICollection<ProductCategory> ProductCategories { get; set; }
  }
}