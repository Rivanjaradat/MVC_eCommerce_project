using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Models;

namespace MVC_eCommerce_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
            public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<FeaturedSection> FeaturedSections { get; set; }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Smell> Smells { get; set; }

    }
}
