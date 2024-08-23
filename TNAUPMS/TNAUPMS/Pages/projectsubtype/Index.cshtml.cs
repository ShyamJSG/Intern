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
    public class ProjectSubType : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int PTId{ get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Sub Type Name is required.")]
        public string ProjectSubTypeName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        public List<masprojectsubtype> prosModel { get; set; }
        public List<masprojecttype> projectModel { get; set; }
        public List<SelectListItem> ProjectList { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }


        public ProjectSubType(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(co);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            ProjectList = projectModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();


            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masprojectsubtype ms = System.Text.Json.JsonSerializer.Deserialize<masprojectsubtype>(result.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            ProjectList = projectModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();
            
            masprojectsubtypeedit mue = new masprojectsubtypeedit();
            mue.id = ms.id;
            mue.code = ms.code;
            mue.ptId = ms.ptId;
            mue.subTypeName = ms.subTypeName;
            mue.ProjectList = ProjectList;
            return Partial("_projectsubtype", mue);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            masprojectsubtype dpt = new masprojectsubtype();
            dpt.id = Id;
            dpt.ptId = PTId;
            dpt.code = Code;
            dpt.subTypeName = ProjectSubTypeName;

            string json = JsonConvert.SerializeObject(dpt);
            string token = HttpContext.Session.GetString("JwToken");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(baseurl + "/projecttype/save-subtype", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result1.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response2 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result2.ToString());
            ProjectList = projectModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();
            return Page();



        }
        public async Task<IActionResult> OnPostEdit(masprojectsubtype msm)
        {
            masprojectsubtype dpt = new masprojectsubtype();
            dpt.id = msm.id;
            dpt.code = msm.code;
            dpt.subTypeName = msm.subTypeName;
            dpt.ptId = msm.ptId;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttype/save-subtype", httpContent);
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
            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result1.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response2 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result2.ToString());
            ProjectList = projectModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttype/remove-subtype/" + id.ToString(), httpContent);

            var response1 = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result1.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response2 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result2.ToString());
            ProjectList = projectModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();

            return Page();
        }
    }
}

