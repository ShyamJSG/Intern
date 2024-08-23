using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectTask : PageModel
    {
        private readonly ILogger<ProjectTask> _logger;

        public ProjectTask(ILogger<ProjectTask> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
