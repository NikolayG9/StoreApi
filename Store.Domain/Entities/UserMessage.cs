using System;
using System.Collections.Generic;
using System.Linq;
namespace Store.Domain.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
