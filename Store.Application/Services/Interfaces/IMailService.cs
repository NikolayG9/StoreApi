namespace Store.Application.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string recipient, string subject, string body, byte[] pdfData, CancellationToken cancellationToken);
    }
}
