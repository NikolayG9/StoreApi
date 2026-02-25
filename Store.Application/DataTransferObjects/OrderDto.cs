namespace Store.Application.DataTransferObjects
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public string? Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsSoftDeleted { get; set; }
        public string UserId { get; set; }
        public int OrderInformationId { get; set; }
        public OrderInformationDto OrderInformation { get; set; }
        public List<ProductOrderDto> OrderedProducts { get; set; }
    }
}
