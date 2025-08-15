using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;

namespace MVC_eCommerce_project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
                return RedirectToAction("Login", "Account");

            var cart = await context.Carts
                .Where(c => c.UserId == currentUser.Id)
                .Include(c => c.Product) 
                .ToListAsync();

            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId, int Qty = 1)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var product = await context.Products.FindAsync(productId);

            if (product == null)
                return BadRequest("Product not found");

            var cart = new Cart
            {
                ProductId = productId,
                Qty = Qty,
                UserId = currentUser.Id,

            };

            context.Add(cart);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var cart = await context.Carts.FindAsync(id);

            if (cart == null)
                return BadRequest("Cart item not found");

            context.Remove(cart);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQty(int id, int quantity)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            var cartItem = await context.Carts
                .Where(c => c.Id == id && c.UserId == currentUser.Id)
                .FirstOrDefaultAsync();

            if (cartItem == null)
                return BadRequest("Cart item not found");

            cartItem.Qty = quantity;
            await context.SaveChangesAsync();

            return Ok(); 
        }
    }
}
