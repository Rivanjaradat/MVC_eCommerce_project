using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;
using MVC_eCommerce_project.ViewModel;

namespace MVC_eCommerce_project.Controllers
{
    public class HomeController : Controller

    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
          
            // get 6 products from the database
            var products = await _context.Products
                .Include(p => p.Category)
               
                .Take(8)
                .ToListAsync();

            var categories = await _context.Categories.ToListAsync();
            var sliderImages = await _context.SliderImages.ToListAsync();
            var fearuredSection = await _context.FeaturedSections.FirstOrDefaultAsync(a => a.IsActive == true);
           var ads=await _context.Ads.FirstOrDefaultAsync(a => a.IsActive == true);
            var model = new HomeViewModel
            {
                Products = products,
                SliderImages = sliderImages,
                FeaturedSections =fearuredSection,
                


                Categories = categories

            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
