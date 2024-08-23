using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            var connectionString = _configuration.GetConnectionString("DefaultString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Employees", con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                List<Employee> EmployeeList = new List<Employee>();
                Response response = new Response();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Employee employee = new Employee();
                        employee.EmpId = Convert.ToInt32(dt.Rows[i]["EmpId"]);
                        employee.EmpName = Convert.ToString(dt.Rows[i]["EmpName"]);
                        employee.Password = Convert.ToString(dt.Rows[i]["Password"]);
                        EmployeeList.Add(employee);
                    }
                }
                if (EmployeeList.Count > 0)
                {
                    return JsonConvert.SerializeObject(EmployeeList);
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = ("No Data Found");
                    return JsonConvert.SerializeObject(response);
                }
            }
        }
    }
}
