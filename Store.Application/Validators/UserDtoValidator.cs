using FluentValidation;
using Store.Application.DataTransferObjects;
using Entity = Store.Domain.Entities;

namespace Store.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
        }
    }
}
