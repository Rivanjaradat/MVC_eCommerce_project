using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;
using MVC_eCommerce_project.ViewModel;

namespace MVC_eCommerce_project.ViewComponents
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation:ViewComponent
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public Navigation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
      
          public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                
                var emptyModel = new NavigationViewModel
                {
                    Cart = new List<Cart>()
                };
                return View("Index", emptyModel);
            }

            var cart = await context.Carts
                .Where(c => c.UserId == currentUser.Id)
                .ToListAsync();

            var model = new NavigationViewModel
            {
                Cart = cart
            };

            return View("Index", model);
        }

    }

}
