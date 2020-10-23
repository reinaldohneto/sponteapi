namespace SponteAPI.Models
{
  public class ProductCategory
  {
    public int ProductID { get; set; }
    public Product Product { get; set; }
    public int CategoryID { get; set; }
    public Category Category { get; set; }

  }
}