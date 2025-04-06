using Microsoft.AspNetCore.Identity;

namespace MVC_eCommerce_project.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
      
      


    }
}
