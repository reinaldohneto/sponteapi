using System;
using FluentValidation;
using SponteAPI.Models;

namespace SponteAPI.Validators
{
  public class ProductValidator : AbstractValidator<Product>
  {
    public ProductValidator()
    {
      RuleFor(p => p.Title)
      .NotNull().WithMessage("O título do produto não pode ser nulo")
      .Length(1, 100).WithMessage("O título do produto deve ter entre 1 e 100 caracteres");
      RuleFor(p => p.AcquisitionDate)
      .LessThan(DateTime.Now).WithMessage("A data de compra deve ser inferior a data atual");
      RuleFor(p => p.CompleteDescription)
      .NotNull().WithMessage("A descrição completa não pode ser nula");
      RuleFor(p => p.Height)
      .NotNull().WithMessage("A alutra não pode ser nula");
      RuleFor(p => p.Width)
      .NotNull().WithMessage("A largura não pode ser nula");
      RuleFor(p => p.Length)
      .NotNull().WithMessage("O comprimento não pode ser nulo");
      RuleFor(p => p.Weight)
      .NotNull().WithMessage("O peso não pode ser nulo");
      RuleFor(p => p.BarCode)
      .NotNull().WithMessage("O código de barras não pode ser nulo");
      RuleFor(p => p.CategoryID)
      .NotNull().WithMessage("É necessário pelo menos uma categoria");
      RuleFor(p => p.Value)
      .NotNull().WithMessage("O valor não pode ser nulo");
      RuleFor(p => p.ProductImageLink)
      .NotNull().WithMessage("O link para a imagem não pode ser nulo");
    }
  }
}