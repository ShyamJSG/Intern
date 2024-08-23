using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectReporting : PageModel
    {
        private readonly ILogger<ProjectReporting> _logger;

        public ProjectReporting(ILogger<ProjectReporting> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
