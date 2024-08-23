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

namespace TNAUPMS.Pages
{
    public class Science: PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int SciId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Science Name is required.")]
        public string ScienceName { get; set; }
        public List<masscience>sceModel { get; set; }
        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }

        public Science(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/science/get-science-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/science/get-byscienceid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masscience ms = System.Text.Json.JsonSerializer.Deserialize<masscience>(result.ToString());
            
            return Partial("_scienceEdit", ms);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            masscience dpt = new masscience();
            dpt.id = 0;
            dpt.code = Code;
            dpt.scienceName= ScienceName;

            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/science/save-science", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            dynamic dynamicObject = JsonConvert.DeserializeObject(result);
            if (dynamicObject.result != "Success")
            {
                if (dynamicObject.errorMsgs.Count > 0)
                {
                    displaymsg = dynamicObject.errorMsgs[0];
                }
            }
            else
            {
                displaymsg = dynamicObject.result;
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/science/get-science-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result1.ToString());
            return Page();
        }

        public async Task<IActionResult> OnPostEdit(masscience msm)
        {
            masscience dpt = new masscience();
            dpt.id = msm.id;
            dpt.code = msm.code;
            dpt.scienceName = msm.scienceName;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/science/save-science", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            dynamic dynamicObject = JsonConvert.DeserializeObject(result);
            if (dynamicObject.result != "Success")
            {
                if (dynamicObject.errorMsgs.Count > 0)
                {
                    displaymsg = dynamicObject.errorMsgs[0];
                }
            }
            else { displaymsg = dynamicObject.result; }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/science/get-science-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result1.ToString());

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/science/remove/" + id.ToString(), httpContent);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/science/get-science-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result1.ToString());

            return Page();
        }
    }
}

 