using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Exceptions;
using Entity = Store.Domain.Entities;

namespace Store.Application.Services
{
    public class UserService(
        SignInManager<Entity.User> signInManager,
        IValidator<UserDto> validator,
        ILogger<UserService> logger) 
        : IUserService
    {
        public async Task<bool> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            logger.LogInformation("Register new user");

            var validationResult = await validator.ValidateAsync(userDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new NotValidDtoException(nameof(UserDto), validationResult.Errors.ToString());
            }

            var user = new Entity.User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email,
                Country = userDto.Country,
                City = userDto.City,
                PhoneNumber = userDto.PhoneNumber,
                RegistrationDate = DateTime.UtcNow
            };

            var userResult = await signInManager.UserManager.CreateAsync(user, userDto.Password);

            if (!userResult.Succeeded)
            {
                throw new InvalidOperationException(userResult.Errors.ToString());
            }

            return userResult.Succeeded;
        }
    }
}
