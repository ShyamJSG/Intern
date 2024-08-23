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
    [Route("api/projecttaskextention")]
    public class ProjectTaskExtentionController : TNAUPMSApiController<trnprojecttaskextensioninfo, ItrnprojecttaskextensioninfoRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ProjectTaskExtentionController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }

        [HttpPost("save-extention-request")]
        public async Task<TNAUPMSActionResult> SaveProjectTask([FromBody] trnprojecttaskextensioninfo p_project)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();
            try
            {
                List<string> validationErrors = await IsValid(p_project);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_project.id > 0)
                        {
                            trnprojecttaskextensioninfo pro = await this.Repository.GetByIdAsync(p_project.id);
                            if (pro != null)
                            {
                                pro.Reason= p_project.Reason;
                                pro.UpdatedBy = this.SecurityContext.GetUsername();
                                pro.UpdatedOn= System.DateTime.Now;
                                await this.Repository.UpdateOneAsync(pro);
                            }
                        }
                        else
                        {
                            p_project.Approved = "Yes";
                            p_project.CreatedOn = System.DateTime.Now;
                            p_project.CreatedBy = this.SecurityContext.GetUsername();
                            await this.Repository.InsertOneAsync(p_project);

                            ItrnprojecttaskRepository taskRepo = this.Provider.GetService<ItrnprojecttaskRepository>();
                            trnprojecttask task = await taskRepo.GetByIdAsync(p_project.TaskId);
                            if (task != null)
                            {
                                task.ActiveStatus = "Extended";
                                task.UpdatedBy = this.SecurityContext.GetUsername();
                                task.UpdatedOn = System.DateTime.Now;
                                await taskRepo.UpdateOneAsync(task);
                            }

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
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);
            }
            return authResult;
        }

        [HttpGet("get-task-extention-by-projectId/{p_Id}")]
        public async Task<List<trnprojecttaskextensioninfo>> GetTaskExtentionkByProIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnprojecttaskextensioninfo> protask = await this.Repository.GetProjectTaskExtentionByProjectId(pId);
                return protask;
            }
            return null;
        }

        private async Task<List<string>> IsValid(trnprojecttaskextensioninfo p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (p_project.TaskId == 0)
                    ErrorMsgs.Add("Task Id is required");
                if (p_project.ExtensionDate<=System.DateTime.Now)
                    ErrorMsgs.Add("Extention date is not valid");
                if (string.IsNullOrEmpty(p_project.Reason))
                    ErrorMsgs.Add("Reason is required");
            }
            return ErrorMsgs;
        }
    }
}
