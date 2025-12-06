namespace Store.Application.DataTransferObjects
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
