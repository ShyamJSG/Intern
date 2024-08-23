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
    [Route("api/projecttask")]
    public class ProjectTaskController : TNAUPMSApiController<trnprojecttask, ItrnprojecttaskRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ProjectTaskController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }

        [HttpPost("save-project-task")]
        public async Task<TNAUPMSActionResult> SaveProjectTask([FromBody] trnprojecttask p_project)
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
                            trnprojecttask pro = await this.Repository.GetByIdAsync(p_project.id);
                            if (pro != null)
                            {
                                pro.TaskCode = p_project.TaskCode;
                                pro.TaskName= p_project.TaskName;
                                pro.TaskInformation= p_project.TaskInformation;
                                pro.AssignedTo = p_project.AssignedTo;
                                pro.Reviewer = p_project.Reviewer;
                                pro.StartDate = p_project.StartDate;
                                pro.EndDate= p_project.EndDate;
                                await this.Repository.UpdateOneAsync(pro);
                            }
                        }
                        else
                        {
                            //p_project.CompletedOn = DateTime.Now;
                            p_project.CompletedOn = null;
                            p_project.ExtnDate = DateTime.Now;
                            p_project.ReviewDate = DateTime.Now;
                            p_project.ActiveStatus = "New";
                            await this.Repository.InsertOneAsync(p_project);
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

        [HttpPost("project-task-completion")]
        public async Task<TNAUPMSActionResult> ProjectCompletion([FromBody] ProjectCompletion p_project)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();
            try
            {
                List<string> validationErrors = await IsValidCompletion(p_project);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_project.ProjectId > 0)
                        {
                            trnprojecttask pro = await this.Repository.GetByIdAsync(p_project.TaskId);
                            if (pro != null)
                            {
                                pro.CompletedOn = p_project.CompletedOn; //change
                                pro.ReportDetails = p_project.ReportDetails;
                                pro.ReportFile = p_project.ReportFile;
                                pro.ActiveStatus = "Completed";
                                await this.Repository.UpdateOneAsync(pro);
                                authResult.result = "Success";
                            }
                            else
                            {
                                authResult.result = "Fail";
                                authResult.ErrorMsgs.Add("Invalid Request");
                            }
                        }
                        else
                        {
                            authResult.result = "Fail";
                            authResult.ErrorMsgs.Add("Invalid Request");
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

        [HttpPost("project-task-review")]
        public async Task<TNAUPMSActionResult> ProjectTaskReview([FromBody] ProjectCompletion p_project)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();
            try
            {
                List<string> validationErrors = await IsValidCompletion(p_project);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_project.ProjectId > 0)
                        {
                            trnprojecttask pro = await this.Repository.GetByIdAsync(p_project.TaskId);
                            if (pro != null)
                            {
                                pro.ReviewDate = System.DateTime.Now;
                                pro.ReportDetails = p_project.ReportDetails;
                                pro.ReportFile = p_project.ReportFile;
                                pro.ActiveStatus = "Completed";
                                await this.Repository.UpdateOneAsync(pro);
                                authResult.result = "Success";
                            }
                            else
                            {
                                authResult.result = "Fail";
                                authResult.ErrorMsgs.Add("Invalid Request");
                            }
                        }
                        else
                        {
                            authResult.result = "Fail";
                            authResult.ErrorMsgs.Add("Invalid Request");
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

        [HttpPost("remove/{p_PTd}")]
        public async Task<AuthResult> RemoveCategory(string p_PTd)
        {
            int _id = int.Parse(p_PTd);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    trnprojecttask ptask = await this.Repository.GetByIdAsync(_id);
                    ptask.Isactive = isactive;
                    ptask.UpdatedOn = System.DateTime.Now;
                    ptask.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(ptask);
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

        [HttpGet("get-protask-byid/{p_Id}")]
        public async Task<trnprojecttask> GetProjectTaskByIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                trnprojecttask inst = await this.Repository.GetProjectTaskById(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-protask-byProId/{p_Id}")]
        public async Task<List<trnprojecttask>> GetProjTaskByProIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnprojecttask> protask = await this.Repository.GetProjectTaskByProjectId(pId);
                return protask;
            }
            return null;
        }


        private async Task<List<string>> IsValid(trnprojecttask p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_project.TaskCode))
                    ErrorMsgs.Add("Task Code is required");

                if (string.IsNullOrEmpty(p_project.TaskName))
                    ErrorMsgs.Add("Task Name is required");

                if (p_project.AssignedTo ==0)
                    ErrorMsgs.Add("Investigator is required");

                if (string.IsNullOrEmpty(p_project.TaskInformation))
                    ErrorMsgs.Add("Task Information is required");
             
            }
            return ErrorMsgs;
        }
        private async Task<List<string>> IsValidCompletion(ProjectCompletion p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (p_project.TaskId== 0)
                    ErrorMsgs.Add("Task Id is required");

                if (string.IsNullOrEmpty(p_project.ReportDetails))
                    ErrorMsgs.Add("Report Details is required");

            }
            return ErrorMsgs;
        }

        private async Task<List<string>> IsValidReview(ProjectCompletion p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (p_project.TaskId == 0)
                    ErrorMsgs.Add("Task Id is required");

                if (string.IsNullOrEmpty(p_project.ReviewerNotes))
                    ErrorMsgs.Add("Reviewer Notes is required");

            }
            return ErrorMsgs;
        }

        //[HttpPost("upload-methodology-file")]
        //[RequestSizeLimit(int.MaxValue)]
        //[RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        //   ValueLengthLimit = int.MaxValue,
        //   KeyLengthLimit = int.MaxValue,
        //   ValueCountLimit = int.MaxValue,
        //   MultipartHeadersCountLimit = int.MaxValue,
        //   MultipartHeadersLengthLimit = int.MaxValue,
        //   MultipartBoundaryLengthLimit = int.MaxValue)]
        //public async Task<TNAUPMSActionResult> UploadMethodologyFile(FileUploadRequest p_FileUploadRequest)
        //{
        //    TNAUPMSActionResult authResult = new TNAUPMSActionResult();

        //    if (p_FileUploadRequest != null && Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files.Sum(file => file.Length) > 0)
        //    {
        //        trnproject pro = await this.Repository.GetByIdAsync(p_FileUploadRequest.Id);
        //        if (pro != null)
        //        {
        //            string FilesUploadPath = GetUploadPath(p_FileUploadRequest.Id.ToString());

        //           // IMasInstitutionApplnDocumentsRepository filesRepo = this.Provider.GetService<IMasInstitutionApplnDocumentsRepository>();
        //            int errorFileCount = 0;

        //            foreach (var instFile in Request.Form.Files)
        //            {
        //                if (instFile.Length > 0)
        //                {
        //                    string newFile = "Methodology_" + pro.id.ToString() + ".txt";
        //                    try
        //                    {
        //                        string newFilePath = IO.Path.Combine(FilesUploadPath, newFile);

        //                        if (IO.File.Exists(newFilePath))
        //                            IO.File.Delete(newFilePath);

        //                        using (IO.FileStream stream = new IO.FileStream(newFilePath, IO.FileMode.Create))
        //                        {
        //                            await instFile.CopyToAsync(stream);
        //                        }

        //                        //MasVendorDocuments existingFile = await filesRepo.GetVendorDocumentsbyfile(vendor.Id, vendorFile.FileName);
        //                        //if (p_FileUploadRequest.FileType == "Logo")
        //                        //{
        //                        //    MasVendors mv = await this.Repository.GetByIdAsync(p_FileUploadRequest.Id);
        //                        //    mv.ImageFile = vendorFile.FileName;
        //                        //    await this.Repository.UpdateOneAsync(mv);
        //                        //}


        //                        //MasInstitutionApplnDocuments appDocs = new MasInstitutionApplnDocuments()
        //                        //{
        //                        //    IAFId = app.id,
        //                        //    DocumentName = p_FileUploadRequest.DocumentName,
        //                        //    FileName = instFile.FileName,

        //                        //};
        //                        //await filesRepo.InsertOneAsync(appDocs);


        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        errorFileCount += 1;
        //                    }
        //                }
        //            }
        //            if (errorFileCount == 0)
        //                authResult.result = "Success";
        //            else
        //                authResult.ErrorMsgs.Add(string.Format("Error processing {0}/{1} files",
        //                    errorFileCount, Request.Form.Files.Count));
        //        }
        //        else
        //            authResult.ErrorMsgs.Add("Invalid Vendor");
        //    }
        //    else
        //        authResult.ErrorMsgs.Add("Invalid input");

        //    return authResult;
        //}

        //private string GetUploadPath(string p_id)
        //{
        //    IOptions<AppSettings> appSettings = this.Provider.GetService<IOptions<AppSettings>>();

        //    if (string.IsNullOrEmpty(appSettings.Value.UploadFilesPath))
        //        return string.Empty;

        //    string projectFolderPath = IO.Path.Combine(appSettings.Value.UploadFilesPath, "Projects", p_id);

        //    if (!IO.Directory.Exists(projectFolderPath))
        //        IO.Directory.CreateDirectory(projectFolderPath);

        //    return projectFolderPath;
        //}

    }
}
