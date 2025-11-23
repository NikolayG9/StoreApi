namespace Store.Application.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string recipient, string subject, string body, byte[] pdfData, CancellationToken cancellationToken);
        Task SendResetPasswordEmailAsync(string email, string token, string clientUrl, CancellationToken cancellationToken);
    }
}
