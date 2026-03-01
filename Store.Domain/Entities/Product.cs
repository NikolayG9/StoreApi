namespace Store.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CollectionId { get; set; }

        public List<Image> Images { get; set; }
        public List<ProductColor> ProductColors { get; set; }
    }
}
