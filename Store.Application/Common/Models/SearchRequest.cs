namespace Store.Application.Common.Models
{
    public class SearchRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrace { get; set; }
    }
}
