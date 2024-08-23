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
    [Route("api/science")]
    public class ScienceController : TNAUPMSApiController<masscience, ImasscienceRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ScienceController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-byscienceid/{p_Id}")]
        public async Task<masscience> GetScienceAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masscience inst = await this.Repository.GetScienceById(fId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-science-all")]
        public async Task<List<masscience>> GetScienceAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masscience> inst = await this.Repository.GetScienceAll();
                return inst;
            }
            return null;
        }

        [HttpPost("save-science")]
        public async Task<AuthResult> SaveScience([FromBody] masscience p_science)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValid(p_science);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_science.id > 0)
                        {
                            p_science.UpdatedOn = System.DateTime.Now;
                            p_science.UpdatedBy = this.SecurityContext.GetUsername();
                            await this.Repository.UpdateOneAsync(p_science);
                        }
                        else
                        {
                            p_science.CreatedOn = System.DateTime.Now;
                            p_science.CreatedBy = this.SecurityContext.GetUsername();
                            await this.Repository.InsertOneAsync(p_science);
                        }
                        authResult.result = "Success";
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

            }
            return authResult;
        }

        [HttpPost("remove/{p_Sd}")]
        public async Task<AuthResult> RemoveCategory(string p_Sd)
        {
            int _id = int.Parse(p_Sd);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masscience scinence = await this.Repository.GetByIdAsync(_id);
                    scinence.Isactive = isactive;
                    scinence.UpdatedOn = System.DateTime.Now;
                    scinence.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(scinence);
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

        private async Task<List<string>> IsValid(masscience p_fund, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_fund == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_fund.Code))
                    ErrorMsgs.Add("Code is required");

                if (string.IsNullOrEmpty(p_fund.ScienceName))
                    ErrorMsgs.Add("Science Name is required");
            }

            return ErrorMsgs;
        }
    }
}
