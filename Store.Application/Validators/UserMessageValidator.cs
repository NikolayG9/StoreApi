using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Helpers;

namespace Store.Application.Validators
{
    public class UserMessageValidator : AbstractValidator<UserMessageDto>
    {
        public UserMessageValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(UserMessageDto.UserName)))
                .MaximumLength(30)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(UserMessageDto.UserName), 30));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(UserMessageDto.UserEmail)))
                .MaximumLength(100)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(UserMessageDto.UserEmail), 100));


            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(UserMessageDto.Subject)))
                .MaximumLength(100)
                .WithMessage(ValidationHelper.GetMaxLengthMessage(nameof(UserMessageDto.Subject), 100));


            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(ValidationHelper.GetRequiredMessage(nameof(UserMessageDto.Message)));
        }
    }
}
