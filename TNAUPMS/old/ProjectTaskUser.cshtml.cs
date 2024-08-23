using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectTaskUser : PageModel
    {
        private readonly ILogger<ProjectTaskUser> _logger;

        public ProjectTaskUser(ILogger<ProjectTaskUser> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
