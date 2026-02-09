using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Constants;
using Store.Domain.Exceptions;
using System.Text;
using Entity = Store.Domain.Entities;

namespace Store.Application.Services
{
    public class UserService(
        UserManager<Entity.User> userManager,
        SignInManager<Entity.User> signInManager,
        IValidator<UserDto> validator,
        ILogger<UserService> logger,
        IMailService mailService)
        : IUserService
    {
        public async Task<bool> IsAnyUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var existedUser = await userManager.FindByEmailAsync(email);
            return existedUser != null ? true : false;
        }

        public async Task<bool> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Register new user - {userDto.Email}");

            var validationResult = await validator.ValidateAsync(userDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new NotValidDtoException(nameof(UserDto), string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
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

            var userResult = await userManager.CreateAsync(user, userDto.Password);

            if (!userResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", userResult.Errors.Select(e => e.Description)));
            }

            var createdUser = await userManager.FindByEmailAsync(userDto.Email)
                ?? throw new NotFoundException(nameof(UserDto), userDto.Email);

            await userManager.AddToRoleAsync(createdUser, UserRole.Client);

            return userResult.Succeeded;
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(forgotPasswordDto.Email) || string.IsNullOrEmpty(forgotPasswordDto.ClientUrl))
            { 
                throw new ArgumentNullException(nameof(forgotPasswordDto)); 
            }

            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null) 
            {
                return;
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            await mailService.SendResetPasswordEmailAsync(user.Email, token, forgotPasswordDto.ClientUrl, cancellationToken);

            logger.LogInformation($"Reset password email to {user.Email}");
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(resetPasswordDto.Email) || 
                string.IsNullOrEmpty(resetPasswordDto.Token) || 
                string.IsNullOrEmpty(resetPasswordDto.NewPassword))
            {
                throw new ArgumentNullException(nameof(resetPasswordDto));
            }

            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return;
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));
            
            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            logger.LogInformation($"Password successfully reset for {user.Email}");
        }

        public async Task LogOutAsync(CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
        }
    }
}
