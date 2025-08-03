using Microsoft.AspNetCore.Mvc;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Data.Migrations;
using MVC_eCommerce_project.ViewModel;

namespace MVC_eCommerce_project.ViewComponents
{
    [ViewComponent(Name = "Ads")]
    public class Ads:ViewComponent
    {
        private readonly ApplicationDbContext context;

        public Ads(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            var ads = context.Ads.FirstOrDefault(a => a.IsActive == true);
            
            var model = new AdsViewModel
            {
                Ads = ads
            };
            return View("Index", model);
        }
    }
}
