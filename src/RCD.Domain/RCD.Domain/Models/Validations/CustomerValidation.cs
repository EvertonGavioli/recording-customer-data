using FluentValidation;
using RCD.Core.DomainObjects;

namespace RCD.Domain.Models.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => Email.Validate(f.Email.EmailAddress))
                .Equal(true).WithMessage("O Email informado é inválido");

            When(f => f.Phones.Count > 0, () =>
            {
                RuleForEach(p => p.Phones)
                    .Must(p => Phone.Validate(p.PhoneNumber) == true)
                    .WithMessage("O Telefone informado é inválido");
            });
        }
    }
}