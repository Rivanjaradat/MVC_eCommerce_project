using Microsoft.AspNetCore.Identity;

namespace MVC_eCommerce_project.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
