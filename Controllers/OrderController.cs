using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;

namespace MVC_eCommerce_project.Controllers
{
    public class OrderController : Controller
    {
        public readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .ToListAsync();
            return View(orders);
        }
        public async Task< IActionResult> Details(int id)
        {
            var order = await _context.Orders .FirstOrDefaultAsync(o => o.Id == id);
            return View(order);
        }

    }
}
