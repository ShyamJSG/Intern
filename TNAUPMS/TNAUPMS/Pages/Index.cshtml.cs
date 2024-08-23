using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;

namespace TNAUPMS.Pages
{
    public class IndexModel : PageModel
    {
        HttpClient _client;
        private IConfiguration configuration;
        string baseurl;

        public IndexModel(HttpClient client, IConfiguration iConfig)
        {
            _client = client;
            configuration = iConfig;
            baseurl = iConfig.GetValue<string>("AppSettings:baseurl");
        }

        [BindProperty] 
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        public void OnGet()
        {
            //HttpContext.Session.Remove("employeename");
            //HttpContext.Session.Remove("token");
        }
        public async Task<IActionResult> OnPost()
        {
            string responseContent = "[]";

            var data = new SignInDataModel
            {
                username = Username,
                password = Password
            };

            var userdata = System.Text.Json.JsonSerializer.Serialize(data);
            var requestContent = new StringContent(userdata, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(baseurl + "/account/connect", requestContent);

            if (response.IsSuccessStatusCode)
            {
                responseContent = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseContent);
                string token = result.token;
                string usrRole = result.userType;
                if (!string.IsNullOrEmpty(token))
                {
                    // string empName = result.staff.empName;
                    // HttpContext.Session.SetString("employeename", empName);
                    HttpContext.Session.SetString("JwToken", token);
                    HttpContext.Session.SetString("USRrole", usrRole);

                    if (result.userType == "Admin")
                        return RedirectToPage("homeadmin");
                    else if (result.userType == "Director")
                        return RedirectToPage("homedirector");
                    else if (result.userType == "Investigator")
                        return RedirectToPage("homeInvestigator");
                    else
                    {
                        Msg = "Invalid Authentication";
                        return Page();
                    }
                }
                else
                {
                    Msg = "Invalid UserName/Password";
                    return Page();
                }
            }
            else
            {
                Msg = "Invalid UserName/Password";
                return Page();
            }
        }
    }
}
