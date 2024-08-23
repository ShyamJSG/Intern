using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TNAUPMS.Pages.ProjectList
{
    public class IndexModel : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;
        string baseurl;
        public List<trnproject> pjModel { get; set; }
        public IndexModel(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/project/get-project-byDept/10");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            pjModel = System.Text.Json.JsonSerializer.Deserialize<List<trnproject>>(co);
            return Page();
        }
        public IActionResult OnGetEditInfo(int ProjectId)
        {
            HttpContext.Session.SetString("DTSProjectId", ProjectId.ToString());

            return RedirectToPage("./ProjectEdit");
        }
    }
}
