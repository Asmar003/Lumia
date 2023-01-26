using Bilet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bilet.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}