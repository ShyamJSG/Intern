using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class ProjectListDir : PageModel
    {
        private readonly ILogger<ProjectListDir> _logger;

        public ProjectListDir(ILogger<ProjectListDir> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

    }
}
