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

namespace TNAUPMS.Pages.ProjectTask
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int TaskId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Code is required.")]
        public string TaskCode { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Project Name is required.")]
        public string TaskName { get; set; }
        [BindProperty] public string TaskInformation { get; set; }
        [BindProperty] public DateTime? StartDate { get; set; }
        [BindProperty] public DateTime? EndDate { get; set; }
        [BindProperty] public int TaskOwner { get; set; }

        public List<trnprojecttask> taskModel { get; set; }
        public trnproject projectModel { get; set; }

        public List<SelectListItem> OfficerList { get; set; }

        string baseurl;
        HttpClient _client;
        private IConfiguration configuration;

        [BindProperty] public string displaymsg { get; set; }

        public IndexModel(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            if (_ProjectId == null)
            {
                return Redirect("../projects");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostBack()
        {
            HttpContext.Session.Remove("DTSProjId");
            return Redirect("../projects");
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync(baseurl + "/projecttask/get-protask-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            trnprojecttask tt = System.Text.Json.JsonSerializer.Deserialize<trnprojecttask>(result.ToString());

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();

            trnprojecttaskEdit tte = new trnprojecttaskEdit();
            tte.id = tt.id;
            tte.taskCode = tt.taskCode;
            tte.taskName = tt.taskName;
            tte.taskInformation = tt.taskInformation;
            tte.startDate = tt.startDate;
            tte.endDate = tt.endDate;
            tte.assignedTo = tt.assignedTo;
            tte.OfficerList = OfficerList;
            return Partial("_projecttaskEdit", tte);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            trnprojecttask dpt = new trnprojecttask();
            dpt.id = 0;
            dpt.projectId = Convert.ToInt32(_ProjectId);
            dpt.taskCode = TaskCode;
            dpt.taskName = TaskName;
            dpt.taskInformation = TaskInformation;
            dpt.startDate = StartDate;
            dpt.endDate = EndDate;
            dpt.assignedTo = TaskOwner;
            dpt.activeStatus = "New";
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttask/save-project-task", httpContent);
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
            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostEdit(trnprojecttaskEdit tte)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            trnprojecttask dpt = new trnprojecttask();
            dpt.id = tte.id;
            dpt.taskCode = tte.taskCode;
            dpt.taskName = tte.taskName;
            dpt.taskInformation = tte.taskInformation;
            dpt.startDate = tte.startDate;
            dpt.endDate = tte.endDate;
            dpt.assignedTo = tte.assignedTo;
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttask/save-project-task", httpContent);
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
            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttask/remove/" + id.ToString(), httpContent);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response8 = await _client.GetAsync(baseurl + "/investigator/get-investigator-all");
            var result8 = await response8.Content.ReadAsStringAsync();
            List<masinvestigator> investModel = System.Text.Json.JsonSerializer.Deserialize<List<masinvestigator>>(result8.ToString());
            OfficerList = investModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.investigatorName }).ToList();
            return Page();
        }
    }
}