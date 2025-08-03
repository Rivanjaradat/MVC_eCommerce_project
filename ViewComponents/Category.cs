using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;

namespace MVC_eCommerce_project.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class Category: ViewComponent
    {
        private readonly ApplicationDbContext context;

        public Category(ApplicationDbContext context)
        {
            this.context = context;
           
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await context.Categories.ToListAsync();
            return View("Index", categories);
        }



    }
}
