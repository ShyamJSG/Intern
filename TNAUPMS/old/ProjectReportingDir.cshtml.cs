using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectReportingDir : PageModel
    {
        private readonly ILogger<ProjectReportingDir> _logger;

        public ProjectReportingDir(ILogger<ProjectReportingDir> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
