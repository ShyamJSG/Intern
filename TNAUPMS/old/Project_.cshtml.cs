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
    public class Project_ : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int ProjectId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Code is required.")]
        public string ProjectCode { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Name is required.")]
        public string ProjectName { get; set; }
        public int DepartmentId { get; set; }
        public int InstituteId { get; set; }
        public int ScienceId { get; set; }
        public int ScienceMeetId { get; set; }
        public int ProjectTypeId { get; set; }
        public int ProjectSubTypeId { get; set; }
        public int FundingAgencyId { get; set; }

        public string PrincipalInvestigator { get; set; }
        public decimal Budget { get; set; }

        public string Objective { get; set; }

        public string Methodology { get; set; }

        public string Output { get; set; }


        public List<trnproject> pjModel { get; set; }

        //public List<masdepartment> deptModel { get; set; }
        //public List<masinstitute> insModel { get; set; }
        //public List<masscience> sceModel { get; set; }
        //public List<massciencemeet> sciModel { get; set; }
        //public List<masprojecttype> proModel { get; set; }
        //public List<masprojecttype> prosModel { get; set; }
        //public List<masfundingagency> fundModel { get; set; }

        public List<SelectListItem> DeptList { get; set; }
        public List<SelectListItem> InsList { get; set; }
        public List<SelectListItem> SceList { get; set; }
        public List<SelectListItem> SciList { get; set; }
        public List<SelectListItem> ProList { get; set; }
        public List<SelectListItem> ProsList { get; set; }
        public List<SelectListItem> FundList { get; set; }


        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }

        public Project_(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/project/get-project-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            pjModel = System.Text.Json.JsonSerializer.Deserialize<List<trnproject>>(co);

            var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
            var result1 = await response.Content.ReadAsStringAsync();
            List<masdepartment>  deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
            DeptList = deptModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.departmentName }).ToList();

            var response2 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result2 = await response.Content.ReadAsStringAsync();
            List<masinstitute>  insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            InsList = insModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            var response3 = await _client.GetAsync(baseurl + "/science/get-science-bydeptid/{p_deptId}");
            var result3 = await response.Content.ReadAsStringAsync();
            List<masscience> sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result1.ToString());
            SceList = sceModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceName }).ToList();


            var response4 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result4 = await response.Content.ReadAsStringAsync();
            List<massciencemeet> sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result1.ToString());
            SciList = sciModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceMeetName }).ToList();

            var response5 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result5 = await response.Content.ReadAsStringAsync();
            List<masprojecttype>  proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result1.ToString());
            ProList = proModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();

            var response6 = await _client.GetAsync(baseurl + "/projectsubtype/get-projectsubtype-all");
            var result6 = await response.Content.ReadAsStringAsync();
            List<masprojectsubtype> prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result1.ToString());
            ProsList = prosModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.subTypeName}).ToList();


            var response7 = await _client.GetAsync(baseurl + "/fundingagency/get-fundingagency-all");
            var result7 = await response.Content.ReadAsStringAsync();
            List<masfundingagency> fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result1.ToString());
            FundList = fundModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.fundingAgencyName }).ToList();


            return Page();
        }
     
        public async Task<IActionResult> OnPostCreate()
        {
            string token = HttpContext.Session.GetString("JwToken");

            trnproject dpt = new trnproject();
            dpt.id =ProjectId;
            dpt.projectCode= ProjectCode;
            dpt.projectName = ProjectName;
            dpt.departmentId = DepartmentId;
            dpt.instituteId = InstituteId;
            dpt.scienceId = ScienceId;
            dpt.scienceMeetId = ScienceMeetId;
            dpt.projectTypeId = ProjectTypeId;
            dpt.projectSubTypeId = ProjectSubTypeId;
            dpt.fundingAgencyId = FundingAgencyId;
            dpt.budget = Budget;
            dpt.objective = Objective;
            dpt.methodology = Methodology;
            dpt.output = Output;
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/project/save-project", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/project/get-project-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            pjModel = System.Text.Json.JsonSerializer.Deserialize<List<trnproject>>(result1.ToString());
            return Page();

        }
    }
}

