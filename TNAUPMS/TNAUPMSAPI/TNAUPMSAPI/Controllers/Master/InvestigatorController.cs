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
    [Route("api/investigator")]
    public class InvestigatorController : TNAUPMSApiController<masinvestigator, ImasinvestigatorRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public InvestigatorController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-investigator-byid/{p_Id}")]
        public async Task<masinvestigator> GetInstitutionAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masinvestigator inst = await this.Repository.GetByIdAsync(fId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-investigator-all")]
        public async Task<List<masinvestigator>> GetInvestigatorAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masinvestigator> inst = await this.Repository.GetInvestigatorAll();
                return inst;
            }
            return null;
        }

        [HttpGet("get-investigator-by-dept/{p_DeptId}")]
        public async Task<List<masinvestigator>> GetInvestigatorByDeptAsync(string p_DeptId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int deptId = int.Parse(p_DeptId);
                List<masinvestigator> inst = await this.Repository.GetInvestigatorByDeptId(deptId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-investigator-by-inst/{p_InstId}")]
        public async Task<List<masinvestigator>> GetInvestigatorByInstAsync(string p_InstId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int instId = int.Parse(p_InstId);
                List<masinvestigator> inst = await this.Repository.GetInvestigatorByInstId(instId);
                return inst;
            }
            return null;
        }


        [HttpPost("update-investigator")]
        public async Task<AuthResult> UpdateInvestigator([FromBody] masinvestigator p_invest)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValid(p_invest);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_invest.id > 0)
                        {
                            masinvestigator mInst = new masinvestigator();
                            mInst = await this.Repository.GetByIdAsync(p_invest.id);
                            if (mInst != null)
                            {
                                mInst.InvestigatorName = p_invest.InvestigatorName;
                                mInst.Designation= p_invest.Designation;
                                mInst.Qualification= p_invest.Qualification;
                                mInst.MobileNo= p_invest.MobileNo;
                                mInst.DepartmentId= p_invest.DepartmentId;
                                mInst.InstituteId= p_invest.InstituteId;
                                mInst.UpdatedOn = System.DateTime.Now;
                                mInst.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(mInst);
                                authResult.result = "Success";
                            }
                        }
                        else
                        {
                            authResult.result = "Fail";
                            authResult.ErrorMsgs.Add("Invalid Authorization");
                        }
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

        [HttpPost("remove/{p_Id}")]
        public async Task<AuthResult> RemoveInvestigator(string p_Id)
        {
            int _id = int.Parse(p_Id);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masinvestigator invest= await this.Repository.GetByIdAsync(_id);
                    invest.Isactive = isactive;
                    invest.UpdatedOn = System.DateTime.Now;
                    invest.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(invest);
                    result.result = "Sucess";
                }
                else
                {
                    result.result = "Fail";
                    result.ErrorMsgs.Add("Invalid Authorization");
                }
            }
            catch (Exception ex)
            {
                result.result = "Fail";
                result.ErrorMsgs[0] = ex.Message;
            }
            return result;
        }

        private async Task<List<string>> IsValid(masinvestigator p_inst, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_inst == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_inst.InvestigatorName))
                    ErrorMsgs.Add("Investigator Name is required");

                if (string.IsNullOrEmpty(p_inst.MobileNo))
                    ErrorMsgs.Add("Mobile No is required");
            }
            return ErrorMsgs;
        }
    }
}
