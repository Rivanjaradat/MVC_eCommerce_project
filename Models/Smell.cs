using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce_project.Models
{
    public class Smell
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
