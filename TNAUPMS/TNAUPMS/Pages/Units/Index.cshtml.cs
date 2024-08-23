using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TNAUPMS.Pages
{
    public class UnitMaster : PageModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public int InstituteId { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Unit Name is required.")]
        public string UnitName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email Id is required.")]
        public string AdminEmail { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Institute Id is required.")]
        public List<masunits> unitModel { get; set; }

        public List<masinstitute> instModel { get; set; }

        public List<SelectListItem> InstList { get; set; }

        [BindProperty]
        public string displaymsg { get; set; }

        private readonly string _baseurl;

        public UnitMaster(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _baseurl = _configuration.GetValue<string>("AppSettings:baseurl");
        }

        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync(_baseurl + "/units/get-units-all");
            var result = await response.Content.ReadAsStringAsync();
            unitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result);

            response = await _client.GetAsync(_baseurl + "/institution/get-institution-all");
            result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result);

            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync(_baseurl + "/units/get-unitsbyid/" + id);
            var result = await response.Content.ReadAsStringAsync();
            var ms = System.Text.Json.JsonSerializer.Deserialize<masunits>(result);

            response = await _client.GetAsync(_baseurl + "/institution/get-institution-all");
            result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result.ToString());

            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            var mue = new masunitedit
            {
                id = ms.id,
                code = ms.code,
                unitName = ms.unitName,
                instituteId = ms.instituteId,
                InstList = InstList
            };

            return Partial("_unitsEdit", mue);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var dpt = new masunits
            {
                id = Id,
                code = Code,
                unitName = UnitName,
                adminEmail = AdminEmail,
                instituteId = InstituteId,
                Password = "Pass@123"
            };

            string json = JsonConvert.SerializeObject(dpt);
            string token = HttpContext.Session.GetString("JwToken");
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync(_baseurl + "/account/register-unit", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            dynamic dynamicObject = JsonConvert.DeserializeObject(result);

            displaymsg = dynamicObject.result != "Success" && dynamicObject.errorMsgs.Count > 0
                ? dynamicObject.errorMsgs[0]
                : dynamicObject.result;

            response = await _client.GetAsync(_baseurl + "/units/get-units-all");
            result = await response.Content.ReadAsStringAsync();
            unitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result);

            response = await _client.GetAsync(_baseurl + "/institution/get-institution-all");
            result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result.ToString());

            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostEdit(masunits msm)
        {
            var dpt = new masunits
            {
                id = msm.id,
                code = msm.code,
                unitName = msm.unitName,
                instituteId = msm.instituteId
            };

            string json = JsonConvert.SerializeObject(dpt);
            string token = HttpContext.Session.GetString("JwToken");
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync(_baseurl + "/units/update-units", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            dynamic dynamicObject = JsonConvert.DeserializeObject(result);

            displaymsg = dynamicObject.result != "Success" && dynamicObject.errorMsgs.Count > 0
                ? dynamicObject.errorMsgs[0]
                : dynamicObject.result;

            response = await _client.GetAsync(_baseurl + "/units/get-units-all");
            result = await response.Content.ReadAsStringAsync();
            unitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result);

            response = await _client.GetAsync(_baseurl + "/institution/get-institution-all");
            result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result.ToString());

            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            var httpContent = new StringContent(JsonConvert.SerializeObject(""), System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _client.PostAsync(_baseurl + "/units/remove/" + id, httpContent);

            var response = await _client.GetAsync(_baseurl + "/units/get-units-all");
            var result = await response.Content.ReadAsStringAsync();
            unitModel = System.Text.Json.JsonSerializer.Deserialize<List<masunits>>(result);

            response = await _client.GetAsync(_baseurl + "/institution/get-institution-all");
            result = await response.Content.ReadAsStringAsync();
            instModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result.ToString());

            InstList = instModel.Select(a => new SelectListItem { Value = a.id.ToString(), Text = a.instituteName }).ToList();

            return Page();
        }
    }
}
