using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SponteAPI.Data;
using SponteAPI.Models;

namespace SponteAPI.Controllers
{
  [Route("api/categories")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryRepo _repository;
    private readonly IValidator<Category> _categoryValidateValidator;
    public CategoryController(ICategoryRepo repository, IValidator<Category> categoryValidateValidator)
    {
      _repository = repository;
      _categoryValidateValidator = categoryValidateValidator;
    }

    [HttpPost]
    [Route("insert")]
    public ActionResult<string> InsertCategory([FromBody] Category category)
    {
      List<string> response = new List<string>();

      var validationResult = _categoryValidateValidator.Validate(category);


      if (!validationResult.IsValid)
      {
        foreach (var error in validationResult.Errors)
        {
          response.Add(error.ToString());
        }
        return BadRequest(JsonConvert.SerializeObject(response, Formatting.Indented));
      }
      else
      {
        var categoryResponse = _repository.InsertCategory(category.Title);
        return Ok(JsonConvert.SerializeObject(categoryResponse, Formatting.Indented));
      }
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> GetByID(int id)
    {
      if (id != 0)
      {
        var category = _repository.GetByID(id);
        if (category != null)
        {
          return Ok(JsonConvert.SerializeObject(category, Formatting.Indented));
        }
        return NotFound();
      }
      else
      {
        return NotFound();
      }
    }
    [HttpGet]
    [Route("getall")]
    public ActionResult<string> GetAll()
    {
      var category = _repository.GetAll();
      if (category != null)
      {
        return Ok(JsonConvert.SerializeObject(category, Formatting.Indented));
      }
      return NotFound();
    }
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> Delete(int id)
    {
      if (id != 0)
      {
        if (_repository.DeleteCategory(id))
        {
          return Ok("Deleted");
        }
      }
      return BadRequest();
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> Update(int id, [FromBody] Category category)
    {
      List<string> response = new List<string>();

      var validationResult = _categoryValidateValidator.Validate(category);


      if (!validationResult.IsValid)
      {
        foreach (var error in validationResult.Errors)
        {
          response.Add(error.ToString());
        }
        return BadRequest(JsonConvert.SerializeObject(response, Formatting.Indented));
      }
      else
      {
        var categoryResponse = _repository.UpdateCategory(id, category.Title);
        if (categoryResponse.CategoryID == 0)
          return NotFound(JsonConvert.SerializeObject(categoryResponse, Formatting.Indented));
        else
          return Ok(JsonConvert.SerializeObject(categoryResponse, Formatting.Indented));
      }
    }
  }
}