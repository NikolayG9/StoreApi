using Microsoft.AspNetCore.Identity;

namespace Store.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime RegistrationDate { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
