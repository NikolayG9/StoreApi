namespace Store.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsSoftDeleted { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }

        public int OrderInformationId { get; set; }
        public OrderInformation OrderInformation { get; set; }
        
        public List<ProductOrder> OrderedProducts { get; set; }
    }
}
