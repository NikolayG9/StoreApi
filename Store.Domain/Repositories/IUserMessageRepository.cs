using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IUserMessageRepository
    {
        Task<List<UserMessage>> GetUserMessagesAsync(CancellationToken cancellationToken);
        Task<UserMessage> AddUserMessageAsync(UserMessage message, CancellationToken cancellationToken);
    }
}
