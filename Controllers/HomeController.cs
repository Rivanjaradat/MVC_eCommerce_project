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
            var products = await _context.Products.ToListAsync();
            var sliderImages = await _context.SliderImages
                .OrderBy(s => s.SortOrder).
                ToListAsync();
            var model = new HomeViewModel
            {
                Products = products,
                SliderImages = sliderImages 

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
