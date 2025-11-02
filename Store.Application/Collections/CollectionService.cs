using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Application.Collections.Dtos;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Collections
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly ILogger<CollectionService> _logger;
        private readonly IValidator<CollectionDto> _validator;
        private readonly IMapper _mapper;

        public CollectionService(
            ICollectionRepository collectionRepository,
            ILogger<CollectionService> logger,
            IValidator<CollectionDto> validator,
            IMapper mapper)
        {
            _collectionRepository = collectionRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting All Collections");
            var collections = await _collectionRepository.GetAllAsync(cancellationToken);
            
            return _mapper.Map<IEnumerable<CollectionDto>>(collections); 
        }

        public async Task<CollectionDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Collection By Id = {id}");
            var collection = await _collectionRepository.GetByIdAsync(id, cancellationToken);
            if (collection is null)
            {
                throw new NotFoundException(nameof(Collection), id.ToString());
            }

            return _mapper.Map<CollectionDto>(collection);
        }

        public async Task<CollectionDto> CreateAsync(CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating Collection");

            await CheckCollectionDtoValidation(collectionDto, cancellationToken);

            var collection = _mapper.Map<Collection>(collectionDto);
            collection.CreatedAt = DateTime.Now;

            var createdCollection = await _collectionRepository.CreateAsync(collection, cancellationToken);
            return _mapper.Map<CollectionDto>(createdCollection);
        }

        public async Task<CollectionDto> UpdateAsync(CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating Collection With Id = {collectionDto.Id}");
            var isCollectionExists = await _collectionRepository.GetByIdAsync(collectionDto.Id, cancellationToken);
            if (isCollectionExists == null)
            {
                throw new NotFoundException(nameof(Collection), collectionDto.Id.ToString());
            }

            await CheckCollectionDtoValidation(collectionDto, cancellationToken);

            var collection = _mapper.Map<Collection>(collectionDto);

            var updatedCollection = await _collectionRepository.UpdateAsync(collection, cancellationToken);
            return _mapper.Map<CollectionDto>(updatedCollection);
        }

        public async Task<bool> DeleteAsync(int collectionId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting Collection With Id = {collectionId}");
            var isCollectionExists = await _collectionRepository.GetByIdAsync(collectionId, cancellationToken);
            if (isCollectionExists == null)
            {
                throw new NotFoundException(nameof(Collection), collectionId.ToString());
            }

            await _collectionRepository.DeleteAsync(new Collection { Id = collectionId }, cancellationToken);

            return true;
        }

        private async Task CheckCollectionDtoValidation(CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(collectionDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new NotValidDtoException(nameof(Collection), validationResult?.Errors?.ToString() ?? "Unknown Errors");
            }
        }
    }
}
