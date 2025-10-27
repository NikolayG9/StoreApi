namespace Store.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Product> Products { get; set; }
    }
}
