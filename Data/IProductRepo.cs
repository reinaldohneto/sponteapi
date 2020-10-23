using System;
using System.Collections.Generic;
using SponteAPI.Models;

namespace SponteAPI.Data
{
  public interface IProductRepo
  {
    ProductResponse InsertProduct(Product product);
    IEnumerable<Product> GetAll();
    Product GetByID(int ProductID);
    bool DeleteProduct(int ProductID);
    Product UpdateProduct(int ProductID, Product product);
  }
}