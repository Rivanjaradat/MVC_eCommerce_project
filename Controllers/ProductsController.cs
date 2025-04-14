using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            var product = await context.Products.ToListAsync();
            return View(product);
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
    }
}
