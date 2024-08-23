using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIApplication.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CrudController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public Response GetAllEmployees()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.GetAllEmployees(connection);
            return response;
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public Response GetEmployeeById(int id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.GetEmployeeByID(connection,id);
            return response;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public Response AddEmployee(Crud crud)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddEmployee(connection,crud);
            return response;
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public Response UpdateEmployee(Crud crud)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.UpdateEmployee(connection,crud);
            return response;
        }
        [HttpPost]
        [Route("DeleteEmployee/{id}")]
        public Response DeleteEmployee(int id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultString").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.DeleteEmployee(connection, id);
            return response;
        }
    }
}
