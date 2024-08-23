using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public readonly IConfiguration _Configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]
        public string Registration(Registration registration)
        {
            var connectionString = _Configuration.GetConnectionString("DefaultString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultString' not found.");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Register(UserName,Password,Email,IsActive) values ('"+registration.UserName+ "','"+registration.Password+ "','"+registration.Email+ "','"+registration.IsActive+"')",con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return "Data Inserted";
                }
                else
                {
                    return "Error";
                }

            }
        }

        [HttpPost]
        [Route("Login")]
        public string Login(Registration registration)
        {
            var connectionString = _Configuration.GetConnectionString("DefaultString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Register where UserName = '"+registration.UserName+ "' and Password = '"+registration.Password+ "'", con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    return "Valid Login";
                }
                else
                {
                    return "Invalid Login";
                }
            }
        }
    }
}
