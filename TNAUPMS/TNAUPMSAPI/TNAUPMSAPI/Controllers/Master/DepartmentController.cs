using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNAUPMS.Domains;
using TNAUPMS.Domains.Common;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;
using TNAUPMS.Domains.Repositories.Master;
using TNAUPMS.Domains.Repositories.Transaction;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using IO = System.IO;
using Microsoft.Extensions.Options;

namespace TNAUPMS.WebApi.Controllers.Master
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
    [Route("api/department")]
    public class DepartmentController : TNAUPMSApiController<masdepartment, ImasdepartmentRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public DepartmentController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }

        [HttpPost("update-department")]
        public async Task<TNAUPMSActionResult> SaveDepartment([FromBody] masdepartment p_dept)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();
            try
            {
                List<string> validationErrors = await IsValid(p_dept);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_dept.id > 0)
                        {
                            masdepartment mDept = new masdepartment();
                            mDept =await this.Repository.GetByIdAsync(p_dept.id);
                            if (mDept != null)
                            {
                                mDept.Code = p_dept.Code;
                                mDept.DepartmentName = p_dept.DepartmentName;
                                mDept.DirectorName = p_dept.DirectorName;
                                mDept.DirectorMobileNo = p_dept.DirectorMobileNo;
                                mDept.UpdatedOn = System.DateTime.Now;
                                mDept.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(mDept);
                                authResult.result = "Success";
                            }
                        }
                    }
                    else
                    {
                        authResult.result = "Fail";
                        authResult.ErrorMsgs.Add("Invalid Authorization");
                    }
                }
                else
                {
                    authResult.result = "Fail";
                    authResult.ErrorMsgs = validationErrors;
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);

            }
            return authResult;
        }

        [HttpGet("get-department-all")]
        public async Task<List<masdepartment>> GetDepartmentAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masdepartment> inst = await this.Repository.GetAllAsync();
                    return inst;
            }
            return null;
        }
        [HttpGet("get-department/{p_DeptId}")]
        public async Task<masdepartment> GetDepartmentByIdAsync(string p_DeptId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int deptId = int.Parse(p_DeptId);
                masdepartment inst = await this.Repository.GetByIdAsync(deptId);
                return inst;
            }
            return null;
        }

        private async Task<List<string>> IsValid(masdepartment p_dept, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_dept == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_dept.Code))
                    ErrorMsgs.Add("Code is required");


                if (string.IsNullOrEmpty(p_dept.DepartmentName))
                    ErrorMsgs.Add("Department Name is required");
            }

            return ErrorMsgs;
        }


    }
}
