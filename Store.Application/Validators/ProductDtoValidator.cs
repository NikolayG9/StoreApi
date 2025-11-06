using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductDto.Name)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductDto.Name), 30));

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductDto.Description)))
                .MaximumLength(500)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductDto.Description), 500));

            RuleFor(x => x.ProductType)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductDto.ProductType)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductDto.ProductType), 30));

            // TODO: Add validators for Images and Colors
        }
    }
}
