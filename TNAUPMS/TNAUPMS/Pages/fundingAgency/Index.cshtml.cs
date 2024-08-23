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
    public class FundingagencyMaster : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;


        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Code { get; set; }
        [BindProperty]
        public string FundingAgencyName { get; set; }

        [BindProperty]
        public List<masfundingagency>fundModel { get; set; }

        string baseurl;

        [BindProperty]
        public string displaymsg { get; set; }


        public FundingagencyMaster(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(co);
            return Page();
        }
        public async Task<IActionResult> OnPostEditInfo(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/fundagency/get-fundagency/" + id.ToString());
            var result = await response.Content.ReadAsStringAsync();
            masfundingagency msm = new masfundingagency();
            msm = System.Text.Json.JsonSerializer.Deserialize<masfundingagency>(result.ToString());
           
            return Partial("_fundingagencyEdit", msm);
        }
        public async Task<IActionResult> OnPostCreate()
        {
            masfundingagency dpt = new masfundingagency();
            dpt.id = Id;
            dpt.code = Code;
            dpt.fundingAgencyName = FundingAgencyName;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/fundagency/save-fundingagency", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            dynamic dynamicObject = JsonConvert.DeserializeObject(result);
            if (dynamicObject.result != "Success")
            { 
                if (dynamicObject.errorMsgs.Count > 0)
                {
                    displaymsg = dynamicObject.errorMsgs[0];
                } 
            }else {displaymsg = dynamicObject.result;}

            var response1 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result1.ToString());
            return Page();
        }
        public async Task<IActionResult> OnPostEdit(masfundingagency msm)
        {
            masfundingagency dpt = new masfundingagency();
            dpt.id = msm.id;
            dpt.code = msm.code;
            dpt.fundingAgencyName = msm.fundingAgencyName;
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject(dpt);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/fundagency/save-fundingagency", httpContent);
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

            var response1 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result1.ToString());
            return Page();
        }

        public async Task<IActionResult>  OnPostDeleteAsync(int id)
        {
            string token = HttpContext.Session.GetString("JwToken");
            string json = JsonConvert.SerializeObject("");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/fundagency/remove/" + id.ToString(), httpContent);

            var response1 = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result1 = await response1.Content.ReadAsStringAsync();
            fundModel = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(result1.ToString());
            return Page();
        }
    }
}

 