using FluentValidation;
using Store.Application.Collections.Dtos;

namespace Store.Application.Validators
{
    public class CollectionDtoValidator : AbstractValidator<CollectionDto>
    {
        public CollectionDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(30)
                .WithMessage("Length of the name cannot be more than 30 symbols");
        }
    }
}
