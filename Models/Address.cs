using Microsoft.AspNetCore.Identity;

namespace MVC_eCommerce_project.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string AddresssLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

    }
}
