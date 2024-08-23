using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectView : PageModel
    {
        private readonly ILogger<ProjectView> _logger;

        public ProjectView(ILogger<ProjectView> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
