using MVC_eCommerce_project.Models;

namespace MVC_eCommerce_project.ViewModel
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List <SliderImage> SliderImages { get; set; }
        public List<Category> Categories { get; set; }
        public FeaturedSection FeaturedSections { get; set; }
        
    }
}
