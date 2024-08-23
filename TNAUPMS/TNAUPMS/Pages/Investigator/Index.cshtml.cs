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
    public class InvestigatorMaster : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Investigator Name is required.")]
        public string InvestigatorName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Educational Qualification is required.")]
        public string Qualification { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "DepartmentId is required.")]
        public int DepartmentId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "InstituteId is required.")]
        public int InstituteId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "EmailId is required.")]
        public string EmailId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "MobileNo is required.")]
        public string MobileNo { get; set; }

        public List<masinvestigator> invModel { get; set; }
        public List<masinstitute> instModel { get; set; }
        public List<SelectListItem> InstList { get; set; }
        public List<masunits> UnitModel { get; set; }
        public List<SelectListItem> UnitList { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }
        public InvestigatorMaster(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }


        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result = await response.Content.ReadAsStringAsync();
            invModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response2 = await _client.GetAsync(baseurl + "/units/get-units-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            UnitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result2.ToString());
            UnitList = UnitModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.unitName}).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/investigator/get-investigator-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masinvestigator ms = System.Text.Json.JsonSerializer.Deserialize<masinvestigator>(result.ToString());

            var response1 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            var response2 = await _client.GetAsync(baseurl + "/units/get-units-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            UnitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result2.ToString());
            UnitList = UnitModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.unitName }).ToList();

            masinvestigatorEdit mue = new masinvestigatorEdit();
            mue.id = ms.id;
            mue.investigatorName = ms.investigatorName;
            mue.designation = ms.designation;
            mue.qualification = ms.qualification;
            mue.departmentId = ms.departmentId;
            mue.instituteId = ms.instituteId;
            mue.mobileNo = ms.mobileNo;
            mue.InstList = InstList;
            mue.UnitList = UnitList;
            return Partial("_investigatorEdit", mue);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            masinvestigator dpt = new masinvestigator();
            dpt.id = Id;
            dpt.investigatorName = InvestigatorName;
            dpt.qualification = Qualification;
            dpt.designation = Designation;
            dpt.departmentId = DepartmentId;
            dpt.instituteId = InstituteId;
            dpt.emailId = EmailId;
            dpt.mobileNo = MobileNo;
            dpt.Password = "Admin@1";
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(baseurl + "/account/register-investigator", httpContent);
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
            var response3 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result3 = await response3.Content.ReadAsStringAsync();
            invModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result3.ToString());

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response1 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response2 = await _client.GetAsync(baseurl + "/units/get-units-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            UnitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result2.ToString());
            UnitList = UnitModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.unitName }).ToList();


            return Page();

        }
        public async Task<IActionResult> OnPostEdit(masinvestigator msm)
        {
            masinvestigator dpt = new masinvestigator();
            dpt.id = msm.id;
            dpt.investigatorName= msm.investigatorName;
            dpt.qualification= msm.qualification;
            dpt.designation= msm.designation;
            dpt.instituteId = msm.instituteId;
            dpt.departmentId = msm.departmentId;
            dpt.mobileNo = msm.mobileNo;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/investigator/update-investigator", httpContent);
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

            var response3 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result3 = await response3.Content.ReadAsStringAsync();
            invModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result3.ToString());

            var response1 = await _client.GetAsync(baseurl + "/units/get-units-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            UnitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result1.ToString());
            UnitList = UnitModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.unitName }).ToList();

            var response2 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result2.ToString());
            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/investigator/remove/" + id.ToString(), httpContent);

            var response3 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result3 = await response3.Content.ReadAsStringAsync();
            invModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result3.ToString());
            
            var response1 = await _client.GetAsync(baseurl + "/units/get-units-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            UnitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result1.ToString());

            var response2 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result2.ToString());
            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();
            return Page();
        }
    }
}