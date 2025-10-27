namespace Store.Domain.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CollectionName { get; set; }
        public string SelectedColor { get; set; }
        public string SelectedSize { get; set; }
        public int ProductQuantity { get; set; }
        public decimal? Discount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
