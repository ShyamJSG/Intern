using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace LibraryManagementWebApp.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string Name,int ID=1)
        {
            ViewData["Message"] = "Hello " + Name;
            ViewData["NumTimes"] = ID;
            return View();
        }
    }
}
