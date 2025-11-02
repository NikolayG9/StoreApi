namespace Store.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName, string identifier) : base($"{resourceName} Not Exists With Id = {identifier}")
        {
        }
    }
}
