using System.ComponentModel;

namespace MVC_eCommerce_project.Models
{
    public class SliderImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [DisplayName(" Sort order")]
        public int SortOrder { get; set; }
    }
}
