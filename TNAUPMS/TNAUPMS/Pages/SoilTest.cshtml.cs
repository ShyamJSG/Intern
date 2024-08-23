using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class SoilTest : PageModel
    {
        private readonly ILogger<SoilTest> _logger;

        public SoilTest(ILogger<SoilTest> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
