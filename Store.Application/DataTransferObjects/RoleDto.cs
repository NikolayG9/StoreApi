namespace Store.Application.DataTransferObjects
{
    public class RoleDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? NormalizedName 
        {
            get { return Name.ToUpper(); } 
        }
    }
}
