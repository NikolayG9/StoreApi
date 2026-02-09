using Store.Application.DataTransferObjects;

namespace Store.Application.Services.Interfaces
{
    public interface IUserMessageService
    {
        Task<List<UserMessageDto>> GetAllUserMessagesAsync(CancellationToken cancellationToken);
        Task<UserMessageDto> AddUserMessageAsync(UserMessageDto userMessageDto,  CancellationToken cancellationToken);
    }
}
