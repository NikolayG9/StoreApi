using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken);
        Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken);
        Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken);
    }
}
