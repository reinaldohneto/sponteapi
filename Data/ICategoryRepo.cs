using System.Collections.Generic;
using SponteAPI.Models;

namespace SponteAPI.Data
{
  public interface ICategoryRepo
  {
    CategoryResponse InsertCategory(string Title);
    IEnumerable<Category> GetAll();
    CategoryResponse GetByID(int CategoryID);
    bool DeleteCategory(int CategoryID);
    Category UpdateCategory(int CategoryID, string Title);
  }
}