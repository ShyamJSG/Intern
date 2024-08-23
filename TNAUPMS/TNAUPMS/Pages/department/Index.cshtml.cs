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
    public class Directorate : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int DeptId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Directorate Name is required.")]
        public string DepartmentName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Director Name is required.")]
        public string DirectorName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Email Id is required.")]
        public string DirectorEmail { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Mobile No is required.")]
        public string DirectorMobile { get; set; }

        public List<masdepartment> deptModel { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }
        public Directorate(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }


        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/department/get-department-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostCreate()
        {
            string token = HttpContext.Session.GetString("JwToken");
            masdepartment dpt = new masdepartment();
            dpt.id = DeptId;
            dpt.code = Code;
            dpt.departmentName = DepartmentName;
            dpt.directorName = DirectorName;
            dpt.directorEmail = DirectorEmail;
            dpt.directorMobileNo = DirectorMobile;
            dpt.Password = "Admin@123";
            dpt.CreatedBy = "Admin";
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(baseurl + "/account/register-director", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            dynamic dynamicObject = JsonConvert.DeserializeObject(result);
            if (dynamicObject.errorMsgs.Count > 0)
            {
                displaymsg = dynamicObject.errorMsgs[0];
            }
            else
            {
                displaymsg = dynamicObject.result;
            }

            var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
            return Page();



        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/department/get-department/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masdepartment msm = new masdepartment();
            msm = System.Text.Json.JsonSerializer.Deserialize<masdepartment>(result.ToString());

            return Partial("_deptEdit", msm);
        }
        public async Task<IActionResult> OnPostEdit(masdepartment msm)
        {
            masdepartment dpt = new masdepartment();
            dpt.id = msm.id;
            dpt.code = msm.code;
            dpt.departmentName = msm.departmentName;
            dpt.directorName = msm.directorName;
            dpt.directorMobileNo = msm.directorMobileNo;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/department/update-department", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
            return Page();
        }
        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    string token = HttpContext.Session.GetString("JwToken");
        //    string json = JsonConvert.SerializeObject("");
        //    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    HttpResponseMessage response = await _client.PostAsync(baseurl + "/department/remove/" + id.ToString(), httpContent);

        //    var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
        //    var result1 = await response1.Content.ReadAsStringAsync();
        //    deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
        //    return Page();
        //}
    }
}
