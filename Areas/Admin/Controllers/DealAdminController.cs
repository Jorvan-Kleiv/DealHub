using Microsoft.AspNetCore.Mvc;

namespace DealHub.Areas.Admin.Controllers
{
    public class DealsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
