namespace Store.Application.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadCollectionImageToBlobStorageAsync(string fileName, Stream data, CancellationToken cancellationToken);
        Task<string> UploadProductImageToBlobStorageAsync(string fileName, Stream data, CancellationToken cancellationToken);
        Task DeleteCollectionImageFromBlobStorageAsync(string filePath, CancellationToken cancellationToken);
        Task DeleteProductImageFromBlobStorageAsync(string filePath, CancellationToken cancellationToken);
    }
}
