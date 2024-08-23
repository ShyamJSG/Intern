using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class HomeAdmin : PageModel
    {
        private readonly ILogger<HomeAdmin> _logger;

        public HomeAdmin(ILogger<HomeAdmin> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/index");
        }
    }
}
