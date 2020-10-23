using Microsoft.EntityFrameworkCore;
using SponteAPI.Models;

namespace SponteAPI.Data
{
  public class ProductContext : DbContext
  {
    public ProductContext(DbContextOptions<ProductContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ProductCategory>()
        .HasKey(pc => new { pc.CategoryID, pc.ProductID });
      modelBuilder.Entity<ProductCategory>()
        .HasOne(pc => pc.Product)
        .WithMany(p => p.ProductCategories)
        .HasForeignKey(pc => pc.ProductID);
      modelBuilder.Entity<ProductCategory>()
        .HasOne(pc => pc.Category)
        .WithMany(c => c.ProductCategories)
        .HasForeignKey(pc => pc.CategoryID);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
  }
}