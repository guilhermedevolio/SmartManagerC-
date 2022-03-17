using FluentValidation;
using SmartManager.Domain.Entities;
using SmartManager.Entities;

namespace SmartManager.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade n�o pode ser vazia");

            RuleFor(x => x.Name)
               .NotNull()
               .WithMessage("O nome não pode ser nulo")
               .NotEmpty()
               .WithMessage("O nome não pode ser vazio");
        }
    }
}
