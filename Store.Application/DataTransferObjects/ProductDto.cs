namespace Store.Application.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CollectionId { get; set; }

        public List<ImageDto> Images { get; set; }
        public List<ColorDto> Colors { get; set; }
    }
}
