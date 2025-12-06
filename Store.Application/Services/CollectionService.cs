using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly ILogger<CollectionService> _logger;
        private readonly IValidator<CollectionDto> _validator;
        private readonly IMapper _mapper;

        public CollectionService(
            ICollectionRepository collectionRepository,
            IBlobStorageService blobStorageService,
            ILogger<CollectionService> logger,
            IValidator<CollectionDto> validator,
            IMapper mapper)
        {
            _collectionRepository = collectionRepository;
            _blobStorageService = blobStorageService;
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
            if (collection == null)
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

            if (collectionDto.File != null)
            {
                using var stream = collectionDto.File.OpenReadStream();
                var imageUrl = await _blobStorageService.UploadCollectionImageToBlobStorageAsync(collectionDto.File.FileName, stream, cancellationToken);

                collection.ImageUrl = imageUrl;
            }

            var createdCollection = await _collectionRepository.CreateAsync(collection, cancellationToken);
            return _mapper.Map<CollectionDto>(createdCollection);
        }

        public async Task<CollectionDto> UpdateAsync(CollectionDto collectionDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating Collection With Id = {collectionDto.Id}");

            await CheckCollectionDtoValidation(collectionDto, cancellationToken);

            var collection = _mapper.Map<Collection>(collectionDto);

            if (collectionDto.File != null) 
            {
                using var stream = collectionDto.File.OpenReadStream();

                if (!string.IsNullOrEmpty(collectionDto.ImageUrl))
                {
                    await _blobStorageService.DeleteCollectionImageFromBlobStorageAsync(collectionDto.ImageUrl, cancellationToken);
                }

                var imageUrl = await _blobStorageService.UploadCollectionImageToBlobStorageAsync(collectionDto.File.FileName, stream, cancellationToken);
                collection.ImageUrl = imageUrl;
            }

            var updatedCollection = await _collectionRepository.UpdateAsync(collection, cancellationToken);
            return _mapper.Map<CollectionDto>(updatedCollection);
        }

        public async Task DeleteAsync(int collectionId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting Collection With Id = {collectionId}");
            var collection = await _collectionRepository.GetByIdAsync(collectionId, cancellationToken);
            if (collection == null)
            {
                throw new NotFoundException(nameof(Collection), collectionId.ToString());
            }

            if (!string.IsNullOrEmpty(collection.ImageUrl))
            {
                await _blobStorageService.DeleteCollectionImageFromBlobStorageAsync(collection.ImageUrl, cancellationToken);
            }

            await _collectionRepository.DeleteAsync(collection, cancellationToken);
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
