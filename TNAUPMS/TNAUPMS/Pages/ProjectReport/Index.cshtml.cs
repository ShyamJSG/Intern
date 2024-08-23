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

namespace TNAUPMS.Pages.ProjectReport
{
    public class IndexModel : PageModel
    {
        public string userRole { get; set; }
        string baseurl;
        HttpClient _client;
        private IConfiguration configuration;
        public List<trnprojecttask> taskModel { get; set; }
        public List<trnprojecttaskextensioninfo> extModel { get; set; }
        public List<trnprojecttaskreport> rptModel { get; set; }
        public trnproject projectModel { get; set; }
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
            userRole = HttpContext.Session.GetString("USRrole");
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

            var response2 = await _client.GetAsync(baseurl + "/projecttaskreport/get-task-report-by-projectId/" + _ProjectId);
            var result2 = await response2.Content.ReadAsStringAsync();
            rptModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskreport>>(result2.ToString());

            var response3 = await _client.GetAsync(baseurl + "/projecttaskextention/get-task-extention-by-projectId/" + _ProjectId);
            var result3 = await response3.Content.ReadAsStringAsync();
            extModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskextensioninfo>>(result3.ToString());

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
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            userRole = HttpContext.Session.GetString("USRrole");
            if (_ProjectId == null)
            {
                return Redirect("../projects");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttask/get-protask-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            trnprojecttask tsk = System.Text.Json.JsonSerializer.Deserialize<trnprojecttask>(result.ToString());
            string taskName = "";
            if (tsk != null) { taskName = tsk.taskName; }

            reportingModel rpt = new reportingModel();
            rpt.id = 0;
            rpt.projectId = Convert.ToInt32(_ProjectId);
            rpt.taskId = id;
            rpt.taskName = taskName;
            return Partial("_reportingModal", rpt);
        }
        public async Task<IActionResult> OnPostExtentionInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            if (_ProjectId == null)
            {
                return Redirect("../projects");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttask/get-protask-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            trnprojecttask tsk = System.Text.Json.JsonSerializer.Deserialize<trnprojecttask>(result.ToString());
            string taskName = "";
            if (tsk != null) { taskName = tsk.taskName; }
          
            extentionModel ext = new extentionModel();
            ext.id = 0;
            ext.projectId = Convert.ToInt32(_ProjectId);
            ext.taskId = id;
            ext.taskName = taskName;
            return Partial("_extendedModal", ext);

        }

        public async Task<IActionResult> OnPostFinalReport(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            if (_ProjectId == null)
            {
                return Redirect("../projects");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/projecttask/get-protask-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            trnprojecttask tsk = System.Text.Json.JsonSerializer.Deserialize<trnprojecttask>(result.ToString());
            string taskName = "";
            if (tsk != null) { taskName = tsk.taskName; }

            reportingModel rpt = new reportingModel();
            rpt.id = 0;
            rpt.projectId = Convert.ToInt32(_ProjectId);
            rpt.taskId = id;
            rpt.taskName = taskName;
            return Partial("_finalreportModal", rpt);
            
        }

        public async Task<IActionResult> OnPostReportSave(reportingModel rpt)
        {
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            trnprojecttaskreport dpt = new trnprojecttaskreport();
            dpt.id = 0;
            dpt.projectId= Convert.ToInt32(_ProjectId);
            dpt.taskId= rpt.id;
            dpt.reportedDate = rpt.reportedDate;
            dpt.reportDetails = rpt.reportDetails;
            dpt.reportFiles = "";
            dpt.reportedBy = 1;
            dpt.isactive = 1;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttaskreport/save-task-report", httpContent);
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

            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response2 = await _client.GetAsync(baseurl + "/projecttaskreport/get-task-report-by-projectId/" + _ProjectId);
            var result2 = await response2.Content.ReadAsStringAsync();
            rptModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskreport>>(result2.ToString());

            var response3 = await _client.GetAsync(baseurl + "/projecttaskextention/get-task-extention-by-projectId/" + _ProjectId);
            var result3 = await response3.Content.ReadAsStringAsync();
            extModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskextensioninfo>>(result3.ToString());


            return Page();
        }
        public async Task<IActionResult> OnPostExtentionSave(extentionModel rpt)
        {
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");
            trnprojecttaskextensioninfo dpt = new trnprojecttaskextensioninfo();
            dpt.id = 0;
            dpt.projectId = Convert.ToInt32(_ProjectId);
            dpt.taskId = rpt.id;
            dpt.extensionDate= rpt.extendedDate;
            dpt.reason= rpt.reason;
            dpt.approved = "Yes";
            dpt.isactive = 1;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttaskextention/save-extention-request", httpContent);
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

            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response2 = await _client.GetAsync(baseurl + "/projecttaskreport/get-task-report-by-projectId/" + _ProjectId);
            var result2 = await response2.Content.ReadAsStringAsync();
            rptModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskreport>>(result2.ToString());

            var response3 = await _client.GetAsync(baseurl + "/projecttaskextention/get-task-extention-by-projectId/" + _ProjectId);
            var result3 = await response3.Content.ReadAsStringAsync();
            extModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskextensioninfo>>(result3.ToString());


            return Page();
        }
        public async Task<IActionResult> OnPostFinalReportSave(reportingModel rpt)
        {
            string _ProjectId = HttpContext.Session.GetString("DTSProjId");

        trnprojecttaskreport dpt = new trnprojecttaskreport();
            dpt.id = 0;
            dpt.projectId = Convert.ToInt32(_ProjectId);
            dpt.taskId = rpt.id;
            dpt.completedOn = rpt.reportedDate;
            dpt.reportDetails = rpt.reportDetails;
            dpt.reportFiles = "";
            dpt.reportedBy = 1;
            dpt.isactive = 1;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/projecttask/project-task-completion", httpContent);
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

            var responsepro = await _client.GetAsync(baseurl + "/project/get-project-byid/" + _ProjectId);
            var resultpro = await responsepro.Content.ReadAsStringAsync();
            projectModel = System.Text.Json.JsonSerializer.Deserialize<trnproject>(resultpro.ToString());

            var response1 = await _client.GetAsync(baseurl + "/projecttask/get-protask-byProId/" + _ProjectId);
            var result1 = await response1.Content.ReadAsStringAsync();
            taskModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttask>>(result1.ToString());

            var response2 = await _client.GetAsync(baseurl + "/projecttaskreport/get-task-report-by-projectId/" + _ProjectId);
            var result2 = await response2.Content.ReadAsStringAsync();
            rptModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskreport>>(result2.ToString());

            var response3 = await _client.GetAsync(baseurl + "/projecttaskextention/get-task-extention-by-projectId/" + _ProjectId);
            var result3 = await response3.Content.ReadAsStringAsync();
            extModel = System.Text.Json.JsonSerializer.Deserialize<List<trnprojecttaskextensioninfo>>(result3.ToString());


            return Page();
        }

    }
}
