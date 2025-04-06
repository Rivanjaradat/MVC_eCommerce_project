using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;

namespace MVC_eCommerce_project.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public CheckOutController (ApplicationDbContext context,UserManager <IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var addresses= await context.Addresses.
                Include(a => a.User).
                Where(a => a.UserId == currentUser.Id).
                ToListAsync();
            ViewBag.Addresses = addresses;
            return View();
        }
        public async Task<IActionResult> Confirm(int addressId)
        {
            var address = await context.Addresses.Where(a => a.Id == addressId).FirstOrDefaultAsync();
            if (address == null)
            {
                return BadRequest("Address not found");
            }
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            double orderCost=0;
            var cart = await context.Carts
                .Where(c => c.UserId == currentUser.Id)
                .Include(c => c.Product)

                .ToListAsync();
            foreach (var item in cart)
            {

                orderCost += item.Product.Price * item.Qty;
            } 
                var order = new Order
            {
                UserId = currentUser.Id,
                
                AddressId = address.Id,
                CreatedAt = DateTime.Now,
                Status = "Order placed",
                Amount = orderCost,
            };
            context.Orders.Add(order);
            context.SaveChanges();
            foreach (var item in cart)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Price = item.Product.Price,
                    Qty = item.Qty,

                };
                context.Add(orderProduct);
              

            }
            await context.SaveChangesAsync();
            return RedirectToAction("ThankYou");

        }
        public IActionResult ThankYou()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult >Index(Address address)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                address.UserId = currentUser.Id;
                context.Addresses.Add(address);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }
    }
}
