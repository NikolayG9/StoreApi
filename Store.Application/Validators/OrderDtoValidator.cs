using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator(
            IValidator<OrderInformationDto> orderInfoValidator, 
            IValidator<ProductOrderDto> productOrderValidator)
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderDto.Status)));

            RuleFor(x => x.OrderInformation).SetValidator(orderInfoValidator);

            RuleForEach(x => x.OrderedProducts).SetValidator(productOrderValidator);
        }
    }
}
