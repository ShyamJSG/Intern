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
    public class Institution : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Institute Name is required.")]
        public string InstituteName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "City Name is required.")]
        public string City { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "District Name is required.")]
        public string District { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Pin No is required.")]
        public string Pincode { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        public string PrincipalName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Email id is required.")]
        public string PrincipalEmail { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Mobile No is required.")]
        public string PrincipalMobileNo { get; set; }
        public List<masinstitute> insModel { get; set; }
        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }
        public Institution(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }


        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/institution/get-institution-byid/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masinstitute msm = new masinstitute();
            msm = System.Text.Json.JsonSerializer.Deserialize<masinstitute>(result.ToString());

            return Partial("_instituteEdit", msm);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            masinstitute dpt = new masinstitute();
            dpt.id = Id;
            dpt.code = Code;
            dpt.instituteName = InstituteName;
            dpt.address = Address;
            dpt.city = City;
            dpt.district = District;
            dpt.pincode = Pincode;
            dpt.principalName = PrincipalName;
            dpt.principalEmail = PrincipalEmail;
            dpt.principalMobileNo = PrincipalMobileNo;
            dpt.Password = "Admin@1";
            string token = HttpContext.Session.GetString("JwToken"); 
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync(baseurl + "/account/register-principal", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            return Page();

        }

        public async Task<IActionResult> OnPostEdit(masinstitute msm)
        {
            masinstitute dpt = new masinstitute();
            dpt.id = msm.id;
            dpt.code = msm.code;
            dpt.instituteName = msm.instituteName;
            dpt.address = msm.address;
            dpt.city = msm.city;
            dpt.district = msm.district;
            dpt.pincode = msm.pincode;
            dpt.principalName = msm.principalName;
            dpt.principalMobileNo = msm.principalMobileNo;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/institution/update-institution", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/institution/get-institution-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            insModel = System.Text.Json.JsonSerializer.Deserialize<List<masinstitute>>(result1.ToString());
            return Page();
        }

        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    string token = HttpContext.Session.GetString("JwToken");
        //    string json = JsonConvert.SerializeObject("");
        //    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    HttpResponseMessage response = await _client.PostAsync(baseurl + "/fundagency/remove/" + id.ToString(), httpContent);

        //    var response1 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
        //    var result1 = await response1.Content.ReadAsStringAsync();
        //    fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result1.ToString());
        //    return Page();
        //}
    }
}