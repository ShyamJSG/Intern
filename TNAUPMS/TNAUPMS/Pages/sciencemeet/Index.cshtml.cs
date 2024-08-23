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
    public class ScienceMeet : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "ScienceMeet Name is required.")]
        public string ScienceMeetName { get; set; }
        [BindProperty]
        public string ShortName { get; set; }
        public List<massciencemeet>sciModel { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }


        public ScienceMeet(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeetbyid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            massciencemeet msm = new massciencemeet();
            msm = System.Text.Json.JsonSerializer.Deserialize<massciencemeet>(result.ToString());
           
            return Partial("_sciencemeetEdit", msm);
        }
        public async Task<IActionResult> OnPostCreate()
        {
            massciencemeet dpt = new massciencemeet();
            dpt.id = Id;
            dpt.code= Code;
            dpt.scienceMeetName = ScienceMeetName;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/sciencemeet/save-sciencemeet", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result1.ToString());
            Code = "";
            ScienceMeetName = "";
            return Page();
        }
        public async Task<IActionResult> OnPostEdit(massciencemeet msm)
        {
            massciencemeet dpt = new massciencemeet();
            dpt.id = msm.id;
            dpt.code= msm.code;
            dpt.scienceMeetName = msm.scienceMeetName;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/sciencemeet/save-sciencemeet", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result1.ToString());
            return Page();
        }

        public async Task<IActionResult>  OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/sciencemeet/remove/" + id.ToString(), httpContent);

            var response1 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result1.ToString());
            return Page();
        }
    }
}

 