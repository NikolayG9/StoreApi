using Microsoft.AspNetCore.Http;

namespace Store.Application.DataTransferObjects
{
    public class ImageFileDto
    {
        public string? AltText { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFile? File { get; set; }
    }
}
