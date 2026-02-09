using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    internal class UserMessageRepository(StoreDbContext dbContext) : IUserMessageRepository
    {
        public async Task<List<UserMessage>> GetUserMessagesAsync(CancellationToken cancellationToken)
        {
            var userMessages = await dbContext.UserMessages.ToListAsync(cancellationToken);
            return userMessages;
        }

        public async Task<UserMessage> AddUserMessageAsync(UserMessage message, CancellationToken cancellationToken)
        {
            await dbContext.UserMessages.AddAsync(message, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return message;
        }
    }
}
