using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class OrderInformationDtoValidator : AbstractValidator<OrderInformationDto>
    {
        public OrderInformationDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.FirstName)))
                .MaximumLength(80)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.FirstName), 80));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.LastName)))
                .MaximumLength(80)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.LastName), 80));

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.Country)))
                .MaximumLength(40)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.Country), 40));

            RuleFor(x => x.City)
                 .NotEmpty()
                 .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.City)))
                 .MaximumLength(80)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.City), 80));

            RuleFor(x => x.Address)
                 .NotEmpty()
                 .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.Address)))
                 .MaximumLength(200)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.Address), 200));

            RuleFor(x => x.PostalCode)
                 .NotEmpty()
                 .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.PostalCode)))
                 .MaximumLength(20)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.PostalCode), 20));

            RuleFor(x => x.PhoneNumber)
                 .NotEmpty()
                 .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.PhoneNumber)))
                 .MaximumLength(30)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.PhoneNumber), 30));

            RuleFor(x => x.Email)
                 .NotEmpty()
                 .WithMessage(ValidationHelper.GetRequiredMessage(nameof(OrderInformationDto.Email)))
                 .MaximumLength(100)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.Email), 100));

            RuleFor(x => x.OrderDetails)
                 .MaximumLength(500)
                 .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(OrderInformationDto.OrderDetails), 500));
        }
    }
}
