using FluentValidation;
using SponteAPI.Models;

namespace SponteAPI.Validators
{
  public class CategoryValidator : AbstractValidator<Category>
  {
    public CategoryValidator()
    {
      RuleFor(c => c.Title)
          .NotNull().WithMessage("O título da categoria não pode ser nulo")
          .Length(1, 100).WithMessage("O título deve ter entre 1 e 100 caracteres");
    }
  }
}