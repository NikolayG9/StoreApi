using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Application.Common.Models;
using Store.Application.DataTransferObjects;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Repositories;

namespace Store.Application.Services
{
    public class ProductService : IProductService
    {
        private int[] allowPageSizes = [5, 10, 15, 30];

        private readonly IProductRepository _repository;
        private readonly IBlobStorageService _blobStorageService;
        private readonly ILogger<ProductService> _logger;
        private readonly IValidator<ProductDto> _validator;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            IBlobStorageService blobStorageService,
            ILogger<ProductService> logger,
            IValidator<ProductDto> validator,
            IMapper mapper)
        {
            _repository = repository;
            _blobStorageService = blobStorageService;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetByCollectionIdAsync(int collectionId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Products By Collection Id = {collectionId}");
            var products = await _repository.GetByCollectionIdAsync(collectionId, cancellationToken);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<SearchResponse<ProductDto>> GetByCollectionIdWithParamsAsync(int collectionId, SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Products By Collection Id And Params");
            if (searchRequest.PageNumber < 1 || !allowPageSizes.Contains(searchRequest.PageSize))
            {
                throw new NotValidDtoException(nameof(SearchRequest), $"PageNumber must be bigger than 1 or PageSize must be in [{string.Join(",", allowPageSizes)}]");
            }
            
            var (products, totalCount) = await _repository.GetByCollectionIdWithParamsAsync(collectionId, searchRequest.SearchPhrace, searchRequest.PageSize, searchRequest.PageNumber, cancellationToken);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new SearchResponse<ProductDto>(productDtos, totalCount, searchRequest.PageSize, searchRequest.PageNumber);
        }

        public async Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Product By Id = {id}");
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), id.ToString());
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating Product");
            
            await CheckProductDtoValidation(productDto, cancellationToken);

            var product = _mapper.Map<Product>(productDto);

            if (product.Images != null && product.Images.Any())
            {
                product.Images.Clear();
            }

            var createdProduct = await _repository.AddProductAsync(product, cancellationToken);

            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating Product With Id = {productDto.Id}");
            var isProductExist = await _repository.GetByIdAsync(productDto.Id, cancellationToken);
            if (await _repository.IsAnyProductByIdAsync(productDto.Id, cancellationToken) == false)
            {
                throw new NotFoundException(nameof(Product),  productDto.Id.ToString());
            }

            await CheckProductDtoValidation(productDto, cancellationToken);

            var product = _mapper.Map<Product>(productDto);

            var updatedProduct = await _repository.UpdateProductAsync(product, cancellationToken);
            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (await _repository.IsAnyProductByIdAsync(id, cancellationToken) == false)
            {
                throw new NotFoundException(nameof(Product), id.ToString());
            }

            await _repository.DeleteProductAsync(new Product { Id = id }, cancellationToken);
        }

        public async Task HandleImagesAsync(int productId, List<ImageFileDto> images, CancellationToken cancellationToken)
        {
            if (images == null || !images.Any())
            {
                return;
            }

            if (await _repository.IsAnyProductByIdAsync(productId, cancellationToken) == false)
            {
                throw new NotFoundException(nameof(Product), productId.ToString());
            }

            foreach (var imageDto in images)
            {
                if (imageDto.IsNew)
                {
                    var imageUrl = string.Empty;
                    if (imageDto.File != null)
                    {
                        using var stream = imageDto.File.OpenReadStream();
                        imageUrl = await _blobStorageService.UploadProductImageToBlobStorageAsync(imageDto.File.FileName, stream, cancellationToken);
                    }

                    var image = _mapper.Map<Image>(imageDto);
                    image.ProductId = productId;
                    image.ImageUrl = imageUrl;

                    await _repository.AddProductImageAsync(image, cancellationToken);
                }
                if (imageDto.IsDeleted)
                {
                    if (!string.IsNullOrEmpty(imageDto.ImageUrl))
                    {
                        await _blobStorageService.DeleteProductImageFromBlobStorageAsync(imageDto.ImageUrl, cancellationToken);
                    }

                    var image = _mapper.Map<Image>(imageDto);
                    image.ProductId = productId;

                    await _repository.DeleteProductImageAsync(image, cancellationToken);
                }
            }
        }

        private async Task CheckProductDtoValidation(ProductDto productDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(productDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var allErrors = string.Join("; ", validationResult.Errors
                                      .Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));

                throw new NotValidDtoException(nameof(Product), allErrors);
            }
        }
    }
}
