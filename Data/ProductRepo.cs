using System;
using System.Collections.Generic;
using System.Linq;
using SponteAPI.Models;

namespace SponteAPI.Data
{
  public class ProductRepo : IProductRepo
  {
    private readonly ProductContext _context;

    public ProductRepo(ProductContext context)
    {
      _context = context;
    }

    public bool DeleteProduct(int ProductID)
    {
      try
      {
        var product = _context.Products.Single(p => p.ProductID == ProductID);

        _context.Products.Remove(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        _context.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public IEnumerable<Product> GetAll()
    {
      var categories = _context.Products.ToList();
      if (categories.Count > 0)
      {
        return categories;
      }
      return null;
    }

    public Product GetByID(int ProductID)
    {
      var product = new Product();
      try
      {
        product = _context.Products.Single(p => p.ProductID == ProductID);
      }
      catch
      {
        product.ProductID = 0;
        product.Title = "";
      }

      return product;
    }

    public ProductResponse InsertProduct(Product product)
    {
      var productresponse = new ProductResponse();
      var categoryrepo = new CategoryRepo(_context);
      try
      {
        foreach (int category in product.CategoryID)
        {
          var categoryDB = categoryrepo.GetByID(category);
        }

        _context.Products.Add(product).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        _context.SaveChanges();
        foreach (int category in product.CategoryID)
        {
          var productcategory = new ProductCategory();
          productcategory.CategoryID = category;
          productcategory.ProductID = product.ProductID;
          _context.ProductCategories.Add(productcategory).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
        _context.SaveChanges();

        if (product.ProductID != 0)
        {
          productresponse.ProductID = product.ProductID;
          productresponse.Title = product.Title;
          productresponse.CompleteDescription = product.CompleteDescription;
          productresponse.Height = product.Height;
          productresponse.Width = product.Width;
          productresponse.Length = product.Length;
          productresponse.Weight = product.Weight;
          productresponse.BarCode = product.BarCode;
          productresponse.Value = product.Value;
          productresponse.AcquisitionDate = product.AcquisitionDate;
          productresponse.ProductImageLink = product.ProductImageLink;
        }
        return productresponse;
      }
      catch
      {
        productresponse.ProductID = 0;
        return productresponse;
      }
    }

    public Product UpdateProduct(int ProductID, Product product)
    {
      var productInternal = new Product();
      var categoryrepo = new CategoryRepo(_context);
      try
      {
        foreach (int category in product.CategoryID)
        {
          var categoryDB = categoryrepo.GetByID(category);
        }

        productInternal = _context.Products.Single(p => p.ProductID == ProductID);

        productInternal.Title = product.Title;
        productInternal.CompleteDescription = product.CompleteDescription;
        productInternal.Height = product.Height;
        productInternal.Length = product.Length;
        productInternal.Weight = product.Weight;
        productInternal.BarCode = product.BarCode;
        productInternal.Value = product.Value;
        productInternal.AcquisitionDate = product.AcquisitionDate;
        productInternal.ProductImageLink = product.ProductImageLink;
        productInternal.CategoryID = product.CategoryID;

        _context.Products.Add(productInternal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        foreach (var productCategories in _context.ProductCategories.ToList())
        {
          var pc = _context.ProductCategories.FirstOrDefault(pcQ => pcQ.CategoryID == productCategories.CategoryID && pcQ.ProductID == productInternal.ProductID);
          _context.ProductCategories.Attach(pc);
          _context.ProductCategories.Remove(pc);
        }

        foreach (int category in product.CategoryID)
        {
          var productcategory = new ProductCategory();
          productcategory.CategoryID = category;
          productcategory.ProductID = productInternal.ProductID;
          _context.ProductCategories.Add(productcategory).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        }

        _context.SaveChanges();

        return productInternal;
      }
      catch
      {
        productInternal.ProductID = 0;
        return productInternal;
      }
    }
  }
}