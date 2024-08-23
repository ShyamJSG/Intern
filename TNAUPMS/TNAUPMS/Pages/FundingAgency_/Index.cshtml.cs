using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Antiforgery;
using Web.Services;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TNAUPMS.Pages
{
    public class FundingAgencyModel : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;
       // private readonly IRazorRenderService _renderService;
        string baseurl;

        public FundingAgencyModel(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
           // _renderService = renderService;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }
        public IEnumerable<masfundingagency> masfundingagencyList { get; set; }
        public masfundingagency masfundingagency { get; set; }
        public async void OnGet()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiZDk0YzA2ZDQtZTE3NS00NWE1LThiMTItZjFiZDg4MTFhNWYwIiwibmFtZSI6IkFkbWluIiwiY2xpZW50X2lkIjoiMSIsInVzZXJuYW1lIjoiYWRtaW5fdG5hdXBtc0BnbWFpbC5jb20iLCJwaG9uZV9udW1iZXIiOiI5ODc2NTQzMjEwIiwiUm9sZSI6ImFkbWluIiwiSXNBZG1pbiI6IlRydWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIl0sImV4cCI6MTY2Mzk0ODk1MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDgvIn0.Dsb-_fZJK3ZryHOI6sOFloWSIx9KOnEG9ntfFOQU45c";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
            var result = await response.Content.ReadAsStringAsync();
            string co = result.ToString();
            masfundingagencyList = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(co);
        }
        //public async Task<PartialViewResult> OnGetViewAllPartial()
        //{
        //    //string token = HttpContext.Session.GetString("JwToken");
        //    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiZDk0YzA2ZDQtZTE3NS00NWE1LThiMTItZjFiZDg4MTFhNWYwIiwibmFtZSI6IkFkbWluIiwiY2xpZW50X2lkIjoiMSIsInVzZXJuYW1lIjoiYWRtaW5fdG5hdXBtc0BnbWFpbC5jb20iLCJwaG9uZV9udW1iZXIiOiI5ODc2NTQzMjEwIiwiUm9sZSI6ImFkbWluIiwiSXNBZG1pbiI6IlRydWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIl0sImV4cCI6MTY2Mzk0ODk1MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDgvIn0.Dsb-_fZJK3ZryHOI6sOFloWSIx9KOnEG9ntfFOQU45c";
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var response = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
        //    var result = await response.Content.ReadAsStringAsync();
        //    string co = result.ToString();
        //    masfundingagencyList = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(co);
        //    return new PartialViewResult
        //    {
        //        ViewName = "_ViewAll",
        //        ViewData = new ViewDataDictionary<IEnumerable<masfundingagency>>(ViewData, masfundingagencyList)
        //    };
        //}
        public async Task<IActionResult> OnPOSTCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
            {
                masfundingagency mf = new masfundingagency();
                mf.id = 0;
                mf.fundingAgency = "";
                return Partial("_CreateOrEdit", mf);
            }
            else
            {
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiZDk0YzA2ZDQtZTE3NS00NWE1LThiMTItZjFiZDg4MTFhNWYwIiwibmFtZSI6IkFkbWluIiwiY2xpZW50X2lkIjoiMSIsInVzZXJuYW1lIjoiYWRtaW5fdG5hdXBtc0BnbWFpbC5jb20iLCJwaG9uZV9udW1iZXIiOiI5ODc2NTQzMjEwIiwiUm9sZSI6ImFkbWluIiwiSXNBZG1pbiI6IlRydWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIl0sImV4cCI6MTY2Mzk0ODk1MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDgvIn0.Dsb-_fZJK3ZryHOI6sOFloWSIx9KOnEG9ntfFOQU45c";
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetAsync(baseurl + "/fundagency/get-fundagency/" + id.ToString());
                var result = await response.Content.ReadAsStringAsync();
                string co = result.ToString();
                masfundingagency = System.Text.Json.JsonSerializer.Deserialize<masfundingagency>(co);

                return Partial("_CreateOrEdit", masfundingagency);

                //return new PartialViewResult
                //{
                //    ViewName = "_CreateOrEdit",
                //    ViewData = new ViewDataDictionary<masfundingagency>(ViewData, masfundingagency)
                //};
                //return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", masfundingagency) });
            }
        }
        public async Task<PartialViewResult> OnPostCreateOrEditAsync(int id, masfundingagency fundagency)
        {
            if (ModelState.IsValid)
            {
                //if (id == 0)
                //{
                //    await _customer.AddAsync(customer);
                //    await _unitOfWork.Commit();
                //}
                //else
                //{
                //    await _customer.UpdateAsync(customer);
                //    await _unitOfWork.Commit();
                //}
                string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiZDk0YzA2ZDQtZTE3NS00NWE1LThiMTItZjFiZDg4MTFhNWYwIiwibmFtZSI6IkFkbWluIiwiY2xpZW50X2lkIjoiMSIsInVzZXJuYW1lIjoiYWRtaW5fdG5hdXBtc0BnbWFpbC5jb20iLCJwaG9uZV9udW1iZXIiOiI5ODc2NTQzMjEwIiwiUm9sZSI6ImFkbWluIiwiSXNBZG1pbiI6IlRydWUiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzOTMvIl0sImV4cCI6MTY2Mzk0ODk1MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDgvIn0.Dsb-_fZJK3ZryHOI6sOFloWSIx9KOnEG9ntfFOQU45c";
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.GetAsync(baseurl + "/fundagency/get-fundingagency-all");
                var result = await response.Content.ReadAsStringAsync();
                string co = result.ToString();
                masfundingagencyList = System.Text.Json.JsonSerializer.Deserialize<List<masfundingagency>>(co);

                return new PartialViewResult
                {
                    ViewName = "_ViewAll",
                    ViewData = new ViewDataDictionary<IEnumerable<masfundingagency>>(ViewData, masfundingagencyList)
                };
            }
            else
            {
                return new PartialViewResult
                {
                    ViewName = "_CreateOrEdit",
                    ViewData = new ViewDataDictionary<masfundingagency>(ViewData, masfundingagency)
                };
                //var html = await _renderService.ToStringAsync("_CreateOrEdit", fundagency);
                //return new JsonResult(new { isValid = false, html = html });
            }
        }
        //public async Task<JsonResult> OnPostDeleteAsync(int id)
        //{
        //var customer = await _customer.GetByIdAsync(id);
        //await _customer.DeleteAsync(customer);
        //await _unitOfWork.Commit();
        //Customers = await _customer.GetAllAsync();
        //var html = await _renderService.ToStringAsync("_ViewAll", Customers);
        //return new JsonResult(new { isValid = true, html = html });
        //}

    }

}