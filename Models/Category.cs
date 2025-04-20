using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce_project.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image{ get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
