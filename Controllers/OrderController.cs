using Microsoft.AspNetCore.Mvc;

namespace MVC_eCommerce_project.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
