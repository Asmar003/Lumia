using Microsoft.AspNetCore.Mvc;

namespace Bilet.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
