using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


    }
}
