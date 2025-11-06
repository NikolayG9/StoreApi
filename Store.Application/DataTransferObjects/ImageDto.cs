namespace Store.Application.DataTransferObjects
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }
        public bool IsMain { get; set; }
    }
}
