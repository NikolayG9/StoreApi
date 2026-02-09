namespace Store.Application.DataTransferObjects
{
    public class UserMessageDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
