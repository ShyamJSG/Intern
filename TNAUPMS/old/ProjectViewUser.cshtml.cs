using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectViewUser : PageModel
    {
        private readonly ILogger<ProjectViewUser> _logger;

        public ProjectViewUser(ILogger<ProjectViewUser> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
