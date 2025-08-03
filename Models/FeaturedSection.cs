namespace MVC_eCommerce_project.Models
{
    public class FeaturedSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
