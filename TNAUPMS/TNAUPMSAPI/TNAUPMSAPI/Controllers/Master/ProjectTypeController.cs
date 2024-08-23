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
    [Route("api/projecttype")]
    public class ProjectTypeController : TNAUPMSApiController<masprojecttype, ImasprojecttypeRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ProjectTypeController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-projecttypebyid/{p_Id}")]
        public async Task<masprojecttype> GetProjectTypeByIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masprojecttype pro = await this.Repository.GetByIdAsync(fId);
                return pro;
            }
            return null;
        }
        [HttpGet("get-projecttype-all")]
        public async Task<List<masprojecttype>> GetProjectTypeAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masprojecttype> pros = await this.Repository.GetAllProjectType();
                return pros;
            }
            return null;
        }
        [HttpPost("save-projecttype")]
        public async Task<AuthResult> SaveProjectType([FromBody] masprojecttype p_pro)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValid(p_pro);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_pro.id > 0)
                        {
                            masprojecttype mPro = await this.Repository.GetByIdAsync(p_pro.id);
                            if (mPro != null)
                            {
                                mPro.Code = p_pro.Code;
                                mPro.ProjectType = p_pro.ProjectType;
                                mPro.UpdatedOn = System.DateTime.Now;
                                mPro.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(mPro);
                                authResult.result = "Success";
                            }
                        }
                        else
                        {
                            p_pro.CreatedOn = System.DateTime.Now;
                            p_pro.UpdatedOn = System.DateTime.Now;
                            p_pro.CreatedBy = this.SecurityContext.GetUsername();
                            await this.Repository.InsertOneAsync(p_pro);
                            authResult.result = "Success";
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

            }
            return authResult;
        }
        [HttpPost("remove/{p_PTId}")]
        public async Task<TNAUPMSActionResult> RemoveProjectType(string p_PTId)
        {
            int _id = int.Parse(p_PTId);
            TNAUPMSActionResult result = new TNAUPMSActionResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masprojecttype mPro= await this.Repository.GetByIdAsync(_id);
                    mPro.Isactive = isactive;
                    mPro.UpdatedOn = System.DateTime.Now;
                    mPro.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(mPro);
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
        private async Task<List<string>> IsValid(masprojecttype p_pro, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_pro == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_pro.Code))
                    ErrorMsgs.Add("Code is required");

                if (string.IsNullOrEmpty(p_pro.ProjectType))
                    ErrorMsgs.Add("Project Type is required");
            }

            return ErrorMsgs;
        }


        [HttpGet("get-subtypebyid/{p_SubId}")]
        public async Task<masprojectsubtype> GetSubTypeByIdAsync(string p_SubId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int sId = int.Parse(p_SubId);

                ImasprojectsubtypeRepository subTypeRepo = this.Provider.GetService<ImasprojectsubtypeRepository>();
                masprojectsubtype pro = await subTypeRepo.GetProSubTypeById(sId);
                return pro;
            }
            return null;
        }

        [HttpGet("get-subtypebyproject-all")]
        public async Task<List<masprojectsubtype>> GetSubTypeByProjectAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                ImasprojectsubtypeRepository subTypeRepo = this.Provider.GetService<ImasprojectsubtypeRepository>();
                List<masprojectsubtype> pros = await subTypeRepo.GetProSubTypeByProjectAll();
                return pros;
            }
            return null;
        }

        [HttpGet("get-subtypebyprojectid/{p_PTId}")]
        public async Task<List<masprojectsubtype>> GetSubTypeByProjectIdAsync(string p_PTId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int sId = int.Parse(p_PTId);

                ImasprojectsubtypeRepository subTypeRepo = this.Provider.GetService<ImasprojectsubtypeRepository>();
                List<masprojectsubtype> pros = await subTypeRepo.GetProSubTypeByProjectId(sId);
                return pros;
            }
            return null;
        }

        [HttpPost("save-subtype")]
        public async Task<AuthResult> SaveSubType([FromBody] masprojectsubtype p_pro)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValidSubType(p_pro);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        ImasprojectsubtypeRepository subTypeRepo = this.Provider.GetService<ImasprojectsubtypeRepository>();
                        if (p_pro.id > 0)
                        {
                            masprojectsubtype mPro = await subTypeRepo.GetByIdAsync(p_pro.id);
                            if (mPro != null)
                            {
                                mPro.Code = p_pro.Code;
                                mPro.PTId = p_pro.PTId;
                                mPro.SubTypeName = p_pro.SubTypeName;
                                mPro.UpdatedOn = System.DateTime.Now;
                                mPro.UpdatedBy = this.SecurityContext.GetUsername();
                                await subTypeRepo.UpdateOneAsync(mPro);
                                authResult.result = "Success";
                            }
                        }
                        else
                        {
                            p_pro.Isactive = 1;
                            p_pro.CreatedOn = System.DateTime.Now;
                            p_pro.UpdatedOn = System.DateTime.Now;
                            p_pro.CreatedBy = this.SecurityContext.GetUsername();
                            await subTypeRepo.InsertOneAsync(p_pro);
                            authResult.result = "Success";
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

        [HttpPost("remove-subtype/{p_PSTId}")]
        public async Task<AuthResult> RemoveProjectSubType(string p_PSTId)
        {
            int _id = int.Parse(p_PSTId);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    ImasprojectsubtypeRepository subTypeRepo = this.Provider.GetService<ImasprojectsubtypeRepository>();

                    masprojectsubtype mPro = await subTypeRepo.GetByIdAsync(_id);
                    mPro.Isactive = isactive;
                    mPro.UpdatedOn = System.DateTime.Now;
                    mPro.UpdatedBy = this.SecurityContext.GetUsername();

                    await subTypeRepo.UpdateOneAsync(mPro);
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


        private async Task<List<string>> IsValidSubType(masprojectsubtype p_pro, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_pro == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_pro.Code))
                    ErrorMsgs.Add("Code is required");


                if (string.IsNullOrEmpty(p_pro.SubTypeName))
                    ErrorMsgs.Add("Project Sub Type is required");

                if (p_pro.PTId ==0)
                    ErrorMsgs.Add("Project Type is required");

            }

            return ErrorMsgs;
        }


    }
}
