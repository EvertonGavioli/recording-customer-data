using FluentValidation;

namespace RCD.Domain.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(f => f.Street)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => f.Number)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => f.ZipCode)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => f.City)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => f.State)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");

            RuleFor(f => f.Country)
                .NotEmpty().WithMessage("O Campo {PropertyName} é obrigatório.");
        }
    }
}
