using AutoMapper;
using FluentValidation;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IUserMessageRepository _userMessageRepository;
        private readonly IValidator<UserMessageDto> _validator;
        private readonly IMapper _mapper;

        public UserMessageService(
            IUserMessageRepository userMessageRepository, 
            IValidator<UserMessageDto> validator,
            IMapper mapper)
        {
            _userMessageRepository = userMessageRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<List<UserMessageDto>> GetAllUserMessagesAsync(CancellationToken cancellationToken)
        {
            var userMessages = await _userMessageRepository.GetUserMessagesAsync(cancellationToken);
            return _mapper.Map<List<UserMessageDto>>(userMessages);
        }

        public async Task<UserMessageDto> AddUserMessageAsync(UserMessageDto userMessageDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(userMessageDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var allErrors = string.Join("; ", validationResult.Errors
                      .Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));

                throw new NotValidDtoException(nameof(UserMessage), allErrors);
            }

            var mappedUserMessage = _mapper.Map<UserMessage>(userMessageDto);
            var newUserMessage = await _userMessageRepository.AddUserMessageAsync(mappedUserMessage, cancellationToken);

            return _mapper.Map<UserMessageDto>(newUserMessage);
        }
    }
}
