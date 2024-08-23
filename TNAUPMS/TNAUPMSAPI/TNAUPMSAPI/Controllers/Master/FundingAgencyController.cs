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
    [Route("api/fundagency")]
    public class FundingAgencyController : TNAUPMSApiController<masfundingagency, ImasfundingagencyRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public FundingAgencyController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-fundagency/{p_Id}")]
        public async Task<masfundingagency> GetDepartmentAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masfundingagency inst = await this.Repository.GetByIdAsync(fId);
                return inst;
            }
            return null;
        }


        [HttpGet("get-fundingagency-all")]
        public async Task<List<masfundingagency>> GetDepartmentAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masfundingagency> inst = await this.Repository.GetAllFundingAgency();
                    return inst;
            }
            return null;
        }

        [HttpPost("save-fundingagency")]
        public async Task<AuthResult> SaveFundingAgency([FromBody] masfundingagency p_fund)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValid(p_fund);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_fund.id > 0)
                        {
                            masfundingagency mf = await this.Repository.GetByIdAsync(p_fund.id);
                            if (mf != null)
                            {
                                mf.Code = p_fund.Code;
                                mf.FundingAgencyName = p_fund.FundingAgencyName;
                                mf.UpdatedOn = System.DateTime.Now;
                                mf.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(mf);
                            }
                        }
                        else
                        {
                            p_fund.CreatedOn= System.DateTime.Now;
                            p_fund.CreatedBy = this.SecurityContext.GetUsername();
                            p_fund.UpdatedBy = "";
                            p_fund.UpdatedOn = new DateTime(1900,01,01);
                            await this.Repository.InsertOneAsync(p_fund);
                        }
                        authResult.result ="Success";
                    }
                    else
                    {
                        authResult.result = "Fail";
                        authResult.ErrorMsgs.Add( "Invalid Authorization");
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

            }
            return authResult;
        }

        [HttpPost("remove/{p_FundId}")]
        public async Task<AuthResult> RemoveCategory(string p_FundId)
        {
            int _id = int.Parse(p_FundId);
            AuthResult result = new AuthResult(){ErrorMsgs = new List<string>()};
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masfundingagency fund = await this.Repository.GetByIdAsync(_id);
                    fund.Isactive = isactive;
                    fund.UpdatedOn = System.DateTime.Now;
                    fund.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(fund);
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

        private async Task<List<string>> IsValid(masfundingagency p_fund, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_fund == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_fund.Code))
                    ErrorMsgs.Add("Code is required");

                if (string.IsNullOrEmpty(p_fund.FundingAgencyName))
                    ErrorMsgs.Add("Funding Agency Name is required");
            }

            return ErrorMsgs;
        }
    }
}
