using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Store.Application.Options;
using Store.Application.Services.Interfaces;

namespace Store.Application.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageOptions _blobStorageOptions;

        public BlobStorageService(IOptions<BlobStorageOptions> blobStorageOptions)
        {
            _blobStorageOptions = blobStorageOptions.Value;
        }

        public async Task<string> UploadCollectionImageToBlobStorageAsync(string fileName, Stream data, CancellationToken cancellationToken)
        {
            return await UploadImageToBlobStorageAsync(fileName, _blobStorageOptions.CollectionImagesContainerName, data, cancellationToken);
        }

        public async Task<string> UploadProductImageToBlobStorageAsync(string fileName, Stream data, CancellationToken cancellationToken)
        {
            return await UploadImageToBlobStorageAsync(fileName, _blobStorageOptions.ProductImagesContainerName, data, cancellationToken);
        }

        public async Task DeleteCollectionImageFromBlobStorageAsync(string filePath, CancellationToken cancellationToken)
        {
            var uri = new Uri(filePath);
            var fileName = uri.Segments.Last();

            await DeleteImageFromBlobStorageAsync(fileName, _blobStorageOptions.CollectionImagesContainerName, cancellationToken);
        }

        public async Task DeleteProductImageFromBlobStorageAsync(string filePath, CancellationToken cancellationToken)
        {
            var uri = new Uri(filePath);
            var fileName = uri.Segments.Last();

            await DeleteImageFromBlobStorageAsync(fileName, _blobStorageOptions.ProductImagesContainerName, cancellationToken);
        }

        private async Task<string> UploadImageToBlobStorageAsync(string fileName, string containerName, Stream data, CancellationToken cancellationToken)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageOptions.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(data, cancellationToken);

            var blobUrl = blobClient.Uri.ToString();
            return blobUrl;
        }

        private async Task DeleteImageFromBlobStorageAsync(string fileName, string containerName, CancellationToken cancellationToken)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageOptions.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, null, cancellationToken);
        }
    }
}
