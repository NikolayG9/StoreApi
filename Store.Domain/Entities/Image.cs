namespace Store.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText {  get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
    }
}
