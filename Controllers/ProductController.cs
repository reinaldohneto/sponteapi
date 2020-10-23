using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SponteAPI.Data;
using SponteAPI.Models;

namespace SponteAPI.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepo _repository;
    private readonly IValidator<Product> _productValidateValidator;
    public ProductController(IProductRepo repository, IValidator<Product> productValidateValidator)
    {
      _repository = repository;
      _productValidateValidator = productValidateValidator;
    }

    [HttpPost]
    [Route("insert")]
    public ActionResult<string> InsertProduct([FromBody] Product product)
    {
      List<string> response = new List<string>();

      var validationResult = _productValidateValidator.Validate(product);


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
        var productResponse = _repository.InsertProduct(product);
        if (productResponse.ProductID == 0)
        {
          return BadRequest("Category does not exist");
        }
        return Ok(JsonConvert.SerializeObject(productResponse, Formatting.Indented));
      }
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> GetByID(int id)
    {
      if (id != 0)
      {
        var product = _repository.GetByID(id);
        if (product != null)
        {
          return Ok(JsonConvert.SerializeObject(product, Formatting.Indented));
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
        if (_repository.DeleteProduct(id))
        {
          return Ok("Deleted");
        }
      }
      return NotFound("Not Found");
    }
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> Update(int id, [FromBody] Product product)
    {
      List<string> response = new List<string>();

      var validationResult = _productValidateValidator.Validate(product);


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
        var productResponse = _repository.UpdateProduct(id, product);
        if (productResponse.ProductID == 0)
          return NotFound(JsonConvert.SerializeObject(productResponse, Formatting.Indented));
        else
          return Ok(JsonConvert.SerializeObject(productResponse, Formatting.Indented, new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          }));
      }
    }
  }
}