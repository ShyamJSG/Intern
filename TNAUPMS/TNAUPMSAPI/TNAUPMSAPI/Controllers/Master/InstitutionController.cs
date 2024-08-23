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
    [Route("api/institution")]
    public class InstitutionController : TNAUPMSApiController<masinstitute, ImasinstituteRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public InstitutionController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-institution-byid/{p_Id}")]
        public async Task<masinstitute> GetInstitutionAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masinstitute inst = await this.Repository.GetByIdAsync(fId);
                return inst;
            }
            return null;
        }
            
        [HttpGet("get-institution-all")]
        public async Task<List<masinstitute>> GetIntitutionAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masinstitute> inst = await this.Repository.GetAllInstitution();
                return inst;
            }
            return null;
        }

        [HttpPost("update-institution")]
        public async Task<TNAUPMSActionResult> UpdateInstitution([FromBody] masinstitute p_inst)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();
            try
            {
                List<string> validationErrors = await IsValid(p_inst);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_inst.id > 0)
                        {
                            masinstitute mInst = new masinstitute();
                            mInst = await this.Repository.GetByIdAsync(p_inst.id);
                            if (mInst != null)
                            {
                                mInst.Code = p_inst.Code;
                                mInst.InstituteName = p_inst.InstituteName;
                                mInst.PrincipalName = p_inst.PrincipalName;
                                mInst.Address = p_inst.Address;
                                mInst.City = p_inst.City;
                                mInst.District = p_inst.District;
                                mInst.Pincode = p_inst.Pincode;
                                mInst.PrincipalMobileNo = p_inst.PrincipalMobileNo;
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
        public async Task<AuthResult> RemoveInstitution(string p_Id)
        {
            int _id = int.Parse(p_Id);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masinstitute scinence = await this.Repository.GetByIdAsync(_id);
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

        private async Task<List<string>> IsValid(masinstitute p_inst, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_inst == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_inst.Code))
                    ErrorMsgs.Add("Code is required");

                if (string.IsNullOrEmpty(p_inst.InstituteName))
                    ErrorMsgs.Add("Institution Name is required");

                if (string.IsNullOrEmpty(p_inst.PrincipalName))
                    ErrorMsgs.Add("Principal Name is required");


                if (string.IsNullOrEmpty(p_inst.PrincipalMobileNo))
                    ErrorMsgs.Add("Mobile No is required");
            }

            return ErrorMsgs;
        }
    }
}
