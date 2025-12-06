using Microsoft.AspNetCore.Http;

namespace Store.Application.DataTransferObjects
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile? File { get; set; }
    }
}
