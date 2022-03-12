using FluentValidation;
using SmartManager.Domain.Entities;

namespace SmartManager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade n�o pode ser vazia");

            RuleFor(x => x.Name)
               .NotNull()
               .WithMessage("O nome n�o pode ser nulo")
               .NotEmpty()
               .WithMessage("O nome n�o pode ser vazio");

            RuleFor(x => x.Email)
               .NotNull()
               .WithMessage("O email n�o pode ser nulo")
               .NotEmpty()
               .WithMessage("O email n�o pode ser vazio");

            RuleFor(x => x.Password)
               .NotNull()
               .WithMessage("A senha n�o pode ser nulo")
               .NotEmpty()
               .WithMessage("A senha n�o pode ser vazio");
        }
    }
}
