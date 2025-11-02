namespace Store.Domain.Exceptions
{
    public class NotValidDtoException : Exception
    {
        public NotValidDtoException(string resourceName, string errorMessage) : base($"{resourceName} is invalid due to {errorMessage}")
        {
        }
    }
}
