using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;
using System.Linq;

namespace MVC_eCommerce_project.ViewComponents
{
    [ViewComponent(Name = "SeasonalProducts")]
    public class SeasonalProducts : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public SeasonalProducts(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            var seasonalCategory = context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Name == "شموع موسمية");

            var products = seasonalCategory?.Products.ToList() ?? new List<Product>();

            return View("Index", products);
        }
    }
}
