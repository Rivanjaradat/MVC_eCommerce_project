using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce_project.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public int? Discount { get; set; }
        public string? Size { get; set; }
       
        public  DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; } = true;
        public int? SmellId { get; set; }
        public Smell Smells { get; set; }


    }
}
