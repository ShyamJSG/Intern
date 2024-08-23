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
    public class ProjectTypeMaster : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Project Type Name is required.")]
        public string ProjectTypeName{ get; set; }
        public List<masprojecttype> proModel { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }


        public ProjectTypeMaster(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttype/get-projecttypebyid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masprojecttype msm = new masprojecttype();
            msm = System.Text.Json.JsonSerializer.Deserialize<masprojecttype>(result.ToString());

            return Partial("_projecttypeEdit", msm);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            masprojecttype dpt = new masprojecttype();
            dpt.id = Id;
            dpt.code = Code;
            dpt.projectType = ProjectTypeName;
            dpt.Isactive =1;
            string json = JsonConvert.SerializeObject(dpt);
            string token = HttpContext.Session.GetString("JwToken");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(baseurl + "/projecttype/save-projecttype", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            return Page();
        }

        public async Task<IActionResult> OnPostEdit(masprojecttype msm)
        {
            masprojecttype dpt = new masprojecttype();
            dpt.id = msm.id;
            dpt.code= msm.code;
            dpt.projectType = msm.projectType;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttype/save-projecttype", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttype/remove/" + id.ToString(), httpContent);

            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            return Page();
        }
    }
}

