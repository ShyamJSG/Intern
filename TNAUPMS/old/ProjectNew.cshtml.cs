using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectNew : PageModel
    {
        private readonly ILogger<ProjectNew> _logger;

        public ProjectNew(ILogger<ProjectNew> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
