using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class ProductOrderDtoValidator : AbstractValidator<ProductOrderDto>
    {
        public ProductOrderDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductOrderDto.Name)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductOrderDto.Name), 30));

            RuleFor(x => x.CollectionName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductOrderDto.CollectionName)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductOrderDto.CollectionName), 30));

            RuleFor(x => x.CollectionName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductOrderDto.SelectedColor)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductOrderDto.SelectedColor), 30));

            RuleFor(x => x.CollectionName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductOrderDto.SelectedSize)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(ProductOrderDto.SelectedSize), 30));

            RuleFor(x => x.ProductQuantity)
                .GreaterThan(0)
                .WithMessage(ValidationHelper.GetMustBeGreaterThanMessage(nameof(ProductOrderDto.ProductQuantity), 0));

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(ProductOrderDto.ImageUrl)));
        }
    }
}
