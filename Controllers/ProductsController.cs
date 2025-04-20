using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using System.Security.Policy;

namespace MVC_eCommerce_project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context; // Change to ApplicationDbContext

        public ProductsController(ApplicationDbContext context) // Change to ApplicationDbContext
        {
          
            this.context = context;
        }
        public async Task<IActionResult> Index(int ? categoryId)
        {
            var title = "All product";


            var products =  context.Products.AsQueryable();
              
            if (categoryId != null)
            {
                products = context.Products
                       .Where(p => p.CategoryId == categoryId)
                       ;
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category != null)
                {
                    title = $"{category.Name}s";
                }
            }
            var categories = await context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList( categories, "Id", "Name",categoryId);
            ViewBag.Title = title;

            var data = await products.ToListAsync();

            return View(data);
        }
        public async Task< IActionResult> Details(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> ProductsByCategory(int categoryId)
        {
            var products = await context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return View();
        }
    }
}
