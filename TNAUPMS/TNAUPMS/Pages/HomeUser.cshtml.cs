using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class HomeUser : PageModel
    {
        private readonly ILogger<HomeUser> _logger;

        public HomeUser(ILogger<HomeUser> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
