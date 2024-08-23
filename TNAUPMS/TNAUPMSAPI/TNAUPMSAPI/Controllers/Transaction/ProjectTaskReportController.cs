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
    [Route("api/projecttaskreport")]
    public class ProjectTaskReportController : TNAUPMSApiController<trnprojecttaskreport, ItrnprojecttaskreportRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ProjectTaskReportController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }

        [HttpPost("save-task-report")]
        public async Task<TNAUPMSActionResult> SaveProjectReport([FromBody] trnprojecttaskreport p_project)
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
                            trnprojecttaskreport pro = await this.Repository.GetByIdAsync(p_project.id);
                            if (pro != null)
                            {
                                pro.ReportDetails = p_project.ReportDetails;
                                pro.ReportedBy = p_project.ReportedBy;
                                pro.UpdatedBy= this.SecurityContext.GetUsername();
                                pro.UpdatedOn= System.DateTime.Now;
                                await this.Repository.UpdateOneAsync(pro);
                            }
                        }
                        else
                        {
                            p_project.CreatedBy = this.SecurityContext.GetUsername();
                            p_project.CreatedOn = System.DateTime.Now;
                            await this.Repository.InsertOneAsync(p_project);

                            ItrnprojecttaskRepository taskRepo = this.Provider.GetService<ItrnprojecttaskRepository>();
                            trnprojecttask task = await taskRepo.GetByIdAsync(p_project.TaskId);
                            if (task != null)
                            {
                                if (task.ActiveStatus =="New")
                                {
                                    task.ActiveStatus = "Process";
                                    task.UpdatedBy = this.SecurityContext.GetUsername();
                                    task.UpdatedOn = System.DateTime.Now;
                                    await taskRepo.UpdateOneAsync(task);
                                }
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
                    trnprojecttaskreport ptask = await this.Repository.GetByIdAsync(_id);
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

        [HttpGet("get-task-report-byid/{p_Id}")]
        public async Task<trnprojecttaskreport> GetTaskReportByIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                trnprojecttaskreport inst = await this.Repository.GetProjectTaskReportById(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-task-report-by-taskId/{p_Id}")]
        public async Task<List<trnprojecttaskreport>> GetTaskReportByTaskIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnprojecttaskreport> protask = await this.Repository.GetProjectTaskReportByTaskId(pId);
                return protask;
            }
            return null;
        }

        [HttpGet("get-task-report-by-projectId/{p_Id}")]
        public async Task<List<trnprojecttaskreport>> GetTaskReportkByProIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnprojecttaskreport> protask = await this.Repository.GetProjectTaskReportByProjectId(pId);
                return protask;
            }
            return null;
        }

        private async Task<List<string>> IsValid(trnprojecttaskreport p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_project.ReportDetails))
                    ErrorMsgs.Add("Report Details is required");

                if (p_project.ReportedBy== 0)
                    ErrorMsgs.Add("Reported User Name is required");

                if (p_project.TaskId ==0)
                    ErrorMsgs.Add("Task is required");

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
