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
    public class ProjectUpdate : PageModel
    {
        [BindProperty]
        public int ProjectId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Code is required.")]
        public string ProjectCode { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Name is required.")]
        public string ProjectName { get; set; }
        [BindProperty] public int DepartmentId { get; set; }
        [BindProperty] public int InstituteId { get; set; }
        [BindProperty] public int ScienceId { get; set; }
        [BindProperty] public int ScienceMeetId { get; set; }
        [BindProperty] public int ProjectTypeId { get; set; }
        [BindProperty] public int ProjectSubTypeId { get; set; }
        [BindProperty] public int FundingAgencyId { get; set; }

        [BindProperty] public int InvestigatorId { get; set; }
        [BindProperty] public decimal Budget { get; set; }

        [BindProperty] public string Objective { get; set; }

        [BindProperty] public string Methodology { get; set; }

        [BindProperty] public string Output { get; set; }
        public List<SelectListItem> DeptList { get; set; }
        public List<SelectListItem> InsList { get; set; }
        public List<SelectListItem> SceList { get; set; }
        public List<SelectListItem> SciList { get; set; }
        public List<SelectListItem> ProList { get; set; }
        public List<SelectListItem> ProsList { get; set; }
        public List<SelectListItem> FundList { get; set; }
        public List<SelectListItem> OfficerList { get; set; }

        string baseurl;
        HttpClient _client;
        private IConfiguration configuration;

        [BindProperty] public string displaymsg { get; set; }

        public ProjectUpdate(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responsepro = await _client.GetAsync(baseurl + "/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            trnproject projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());
          


            var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            List<masdepartment> deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
            DeptList = deptModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.departmentName }).ToList();

            var response2 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result2 = await response2.Content.ReadAsStringAsync();
            List<masinstitute> insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result2.ToString());
            InsList = insModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            var response3 = await _client.GetAsync(baseurl + "/science/get-science-all");
            var result3 = await response3.Content.ReadAsStringAsync();
            List<masscience> sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result3.ToString());
            SceList = sceModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceName }).ToList();

            var response4 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
            var result4 = await response4.Content.ReadAsStringAsync();
            List<massciencemeet> sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result4.ToString());
            SciList = sciModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceMeetName }).ToList();

            var response5 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
            var result5 = await response5.Content.ReadAsStringAsync();
            List<masprojecttype> proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result5.ToString());
            ProList = proModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();

            var response6 = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
            var result6 = await response6.Content.ReadAsStringAsync();
            List<masprojectsubtype> prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result6.ToString());
            ProsList = prosModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.subTypeName }).ToList();

            var response7 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result7 = await response7.Content.ReadAsStringAsync();
            List<masfundingagency> fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result7.ToString());
            FundList = fundModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.fundingAgencyName }).ToList();

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();
            
            if (projectModel != null)
            {
                ProjectId = projectModel.id;
                ProjectCode = projectModel.projectCode;
                ProjectName = projectModel.projectName;
                DepartmentId = projectModel.departmentId;
                InstituteId = projectModel.instituteId;
                ScienceId = projectModel.scienceId;
                ScienceMeetId = projectModel.scienceMeetId;
                ProjectTypeId = projectModel.projectTypeId;
                ProjectSubTypeId = projectModel.projectSubTypeId;
                FundingAgencyId = projectModel.fundingAgencyId;
                InvestigatorId = projectModel.principalInvestigator;
                Budget = projectModel.budget;
                Objective = projectModel.objective;
                Methodology = projectModel.methodology;
                Output = projectModel.output;
                return Page();
            }
            else
            {
                return Redirect("../projectlist");
            }
        }
       

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JwToken");

            trnproject dpt = new trnproject();
            dpt.id = ProjectId;
            dpt.projectCode = ProjectCode;
            dpt.projectName = ProjectName;
            dpt.departmentId = DepartmentId;
            dpt.instituteId = InstituteId;
            dpt.principalInvestigator = InvestigatorId;
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
            if (dynamicObject.result != "Success")
            {
                if (dynamicObject.errorMsgs.Count > 0)
                {
                    displaymsg = dynamicObject.errorMsgs[0];
                }
                var response1 = await _client.GetAsync(baseurl + "/department/get-department-all");
                var result1 = await response1.Content.ReadAsStringAsync();
                List<masdepartment> deptModel = System.Text.Json.JsonSerializer.Deserialize<List<masdepartment>>(result1.ToString());
                DeptList = deptModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.departmentName }).ToList();

                var response2 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
                var result2 = await response2.Content.ReadAsStringAsync();
                List<masinstitute> insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result2.ToString());
                InsList = insModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

                var response3 = await _client.GetAsync(baseurl + "/science/get-science-all");
                var result3 = await response3.Content.ReadAsStringAsync();
                List<masscience> sceModel = System.Text.Json.JsonSerializer.Deserialize<List<masscience>>(result3.ToString());
                SceList = sceModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceName }).ToList();

                var response4 = await _client.GetAsync(baseurl + "/sciencemeet/get-sciencemeet-all");
                var result4 = await response4.Content.ReadAsStringAsync();
                List<massciencemeet> sciModel = System.Text.Json.JsonSerializer.Deserialize<List<massciencemeet>>(result4.ToString());
                SciList = sciModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.scienceMeetName }).ToList();

                var response5 = await _client.GetAsync(baseurl + "/projecttype/get-projecttype-all");
                var result5 = await response5.Content.ReadAsStringAsync();
                List<masprojecttype> proModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojecttype>>(result5.ToString());
                ProList = proModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.projectType }).ToList();

                var response6 = await _client.GetAsync(baseurl + "/projecttype/get-subtypebyproject-all");
                var result6 = await response6.Content.ReadAsStringAsync();
                List<masprojectsubtype> prosModel = System.Text.Json.JsonSerializer.Deserialize<List<masprojectsubtype>>(result6.ToString());
                ProsList = prosModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.subTypeName }).ToList();

                var response7 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
                var result7 = await response7.Content.ReadAsStringAsync();
                List<masfundingagency> fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result7.ToString());
                FundList = fundModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.fundingAgencyName }).ToList();

                var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
                var result8 = await response8.Content.ReadAsStringAsync();
                List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
                OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();
                return Page();
            }
            else
            {
                displaymsg = dynamicObject.result;
                
                return Redirect("../projectlist");
            }
        }

    }
}
