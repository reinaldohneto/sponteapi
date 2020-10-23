using System;
using System.Collections.Generic;
using System.Linq;
using SponteAPI.Models;

namespace SponteAPI.Data
{
  public class CategoryRepo : ICategoryRepo
  {
    private readonly ProductContext _context;

    public CategoryRepo(ProductContext context)
    {
      _context = context;
    }

    public bool DeleteCategory(int CategoryID)
    {
      try
      {
        var category = _context.Categories.Single(c => c.CategoryID == CategoryID);

        var pc = new ProductCategory();

        pc = _context.ProductCategories.Single(pc => pc.CategoryID == CategoryID);
        if (pc.CategoryID != 0)
        {
          return false;
        }
        _context.Categories.Remove(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        _context.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public IEnumerable<Category> GetAll()
    {
      var categories = _context.Categories.ToList();
      if (categories.Count > 0)
      {
        return categories;
      }
      return null;
    }

    public CategoryResponse GetByID(int CategoryID)
    {
      var category = new Category();
      try
      {
        category = _context.Categories.Single(c => c.CategoryID == CategoryID);
      }
      catch
      {
        category.CategoryID = 0;
        category.Title = null;
      }

      var categoryResponse = new CategoryResponse();
      categoryResponse.CategoryID = category.CategoryID;
      categoryResponse.Title = category.Title;

      return categoryResponse;
    }

    public CategoryResponse InsertCategory(string Title)
    {
      try
      {
        var category = new Category();
        category.Title = Title;
        _context.Add(category).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        _context.SaveChanges();

        var categoryResponse = new CategoryResponse();

        categoryResponse.CategoryID = category.CategoryID;
        categoryResponse.Title = category.Title;
        return categoryResponse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public Category UpdateCategory(int CategoryID, string Title)
    {
      var category = new Category();
      try
      {
        category = _context.Categories.Single(c => c.CategoryID == CategoryID);
        category.Title = Title;
        _context.Categories.Update(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
        return category;
      }
      catch
      {
        category.CategoryID = 0;
        category.Title = null;
        return category;
      }
    }
  }
}