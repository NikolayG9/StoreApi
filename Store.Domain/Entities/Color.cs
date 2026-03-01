namespace Store.Domain.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? HexColorCode { get; set; }
    
        public List<ProductColor> ProductColors { get; set; }
    }
}
