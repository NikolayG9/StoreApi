using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class CollectionDtoValidator : AbstractValidator<CollectionDto>
    {
        public CollectionDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(CollectionDto.Name)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(CollectionDto.Name), 30));
        }
    }
}
