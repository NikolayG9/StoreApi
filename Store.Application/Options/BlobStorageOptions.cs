namespace Store.Application.Options
{
    public class BlobStorageOptions
    {
        public const string BlobStorageOptionsKey = "BlobStorage";

        public string ConnectionString { get; set; } = default!;
        public string CollectionImagesContainerName { get; set; } = default!;
        public string ProductImagesContainerName { get; set; } = default!;
    }
}
