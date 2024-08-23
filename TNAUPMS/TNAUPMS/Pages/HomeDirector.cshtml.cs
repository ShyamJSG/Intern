using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

namespace TNAUPMS.Pages
{
    public class HomeDirector : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;
        string baseurl;
        [BindProperty] public string displaymsg { get; set; }
        public List<AnalyticsModel> instModel { get; set; }
        public List<AnalyticsModel> fundModel { get; set; }
        public List<AnalyticsModel> scienceModel { get; set; }
        public HomeDirector(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/project/get-Inst-wise-Projectcount/10");
            var result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<AnalyticsModel>>(result.ToString());

            var response1 = await _client.GetAsync(baseurl + "/project/get-science-wise-Projectcount/10");
            var result1 = await response1.Content.ReadAsStringAsync();
            scienceModel = System.Text.Json.JsonSerializer.Deserialize<List<AnalyticsModel>>(result1.ToString());

            var response2 = await _client.GetAsync(baseurl + "/project/get-fund-wise-Projectcount/10");
            var result2 = await response2.Content.ReadAsStringAsync();
            fundModel = System.Text.Json.JsonSerializer.Deserialize<List<AnalyticsModel>>(result2.ToString());
            return Page();
        }
    }
}
