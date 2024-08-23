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
    [Route("api/project")]
    public class ProjectController : TNAUPMSApiController<trnproject, ItrnprojectRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public ProjectController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }

        [HttpPost("save-project")]
        public async Task<TNAUPMSActionResult> SaveProject([FromBody] trnproject p_project)
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
                            trnproject pro = await this.Repository.GetByIdAsync(p_project.id);
                            if (pro != null)
                            {
                                pro.ProjectCode = p_project.ProjectCode;
                                pro.ProjectName= p_project.ProjectName;
                                pro.DepartmentId = p_project.DepartmentId;
                                pro.InstituteId = p_project.InstituteId;
                                pro.ScienceId = p_project.ScienceId;
                                pro.ScienceMeetId = p_project.ScienceMeetId;
                                pro.ProjectTypeId = p_project.ProjectTypeId;
                                pro.ProjectSubTypeId = p_project.ProjectSubTypeId;
                                pro.FundingAgencyId = p_project.FundingAgencyId;
                                pro.StartDate = p_project.StartDate;
                                pro.EndDate = p_project.EndDate;
                                pro.Budget = p_project.Budget;
                                pro.PrincipalInvestigator = p_project.PrincipalInvestigator;
                                pro.Objective = p_project.Objective;
                                pro.Methodology = p_project.Methodology;
                                pro.MethodologyFile = p_project.MethodologyFile;
                                pro.Output = p_project.Output;
                                pro.UpdatedOn = System.DateTime.Now;
                                pro.StartDate = p_project.StartDate;
                                pro.EndDate = p_project.EndDate;
                                pro.CreatedOn = p_project.CreatedOn;
                                pro.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(pro);
                            }
                        }
                        else
                        {
                            p_project.ActiveStatus = "New";
                            p_project.CreatedOn = System.DateTime.Now;
                            p_project.UpdatedOn = System.DateTime.Now;
                            //p_project.StartDate = System.DateTime.Now;
                            //p_project.EndDate = System.DateTime.Now;
                            p_project.CompletedOn = null;
                            p_project.CreatedBy = this.SecurityContext.GetUsername();
                            p_project.UpdatedBy = "";
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

        [HttpPost("project-completion")]
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
                            trnproject pro = await this.Repository.GetByIdAsync(p_project.ProjectId);
                            if (pro != null)
                            {
                                pro.CompletedOn = System.DateTime.Now;
                                pro.ReviewerNotes = p_project.ReviewerNotes;
                                pro.ReviewerMark = p_project.ReviewerMark;
                                pro.UpdatedOn = System.DateTime.Now;
                                pro.UpdatedBy = this.SecurityContext.GetUsername();
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

        [HttpPost("remove/{p_Pd}")]
        public async Task<AuthResult> RemoveCategory(string p_Pd)
        {
            int _id = int.Parse(p_Pd);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    trnproject pro = await this.Repository.GetByIdAsync(_id);
                    pro.Isactive = isactive;
                    pro.UpdatedOn = System.DateTime.Now;
                    pro.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(pro);
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

        [HttpGet("get-projects-all")]
        public async Task<List<trnproject>> GetAllProjects()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<trnproject> projects = await this.Repository.GetProjectsAll();
                return projects;
            }
            return null;
        }


        [HttpGet("get-project-byid/{p_Id}")]
        public async Task<trnproject> GetProjectByIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                trnproject inst = await this.Repository.GetProjectById(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-project-byDept/{p_Id}")]
        public async Task<List<trnproject>> GetProjectByDeptIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnproject> inst = await this.Repository.GetProjectByDeptId(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-project-byInstitute/{p_Id}")]
        public async Task<List<trnproject>> GetProjectByInstIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnproject> inst = await this.Repository.GetProjectByInstitutionId(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-project-byInvestigator/{p_Id}")]
        public async Task<List<trnproject>> GetProjectByInvestigatorIdAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_Id);
                List<trnproject> inst = await this.Repository.GetProjectByInvestigatorId(pId);
                return inst;
            }
            return null;
        }


        private async Task<List<string>> IsValid(trnproject p_project, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_project == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_project.ProjectCode))
                    ErrorMsgs.Add("Project Code is required");

                if (string.IsNullOrEmpty(p_project.ProjectName))
                    ErrorMsgs.Add("Project Name is required");

                if (p_project.DepartmentId ==0)
                    ErrorMsgs.Add("Directorate Name is required");

                if (p_project.InstituteId == 0)
                    ErrorMsgs.Add("Institution is required");

                if (p_project.ScienceId == 0)
                    ErrorMsgs.Add("Science is required");

                if (p_project.ScienceMeetId == 0)
                    ErrorMsgs.Add("Science Meet is required");

                if (p_project.ProjectTypeId == 0)
                    ErrorMsgs.Add("Project Type Meet is required");

                if (p_project.FundingAgencyId == 0)
                    ErrorMsgs.Add("Funding Agency is required");

                if (p_project.PrincipalInvestigator == 0)
                    ErrorMsgs.Add("Principal Investigator is required");

                if (p_project.Budget == 0)
                    ErrorMsgs.Add("Budget is required");

                if (string.IsNullOrEmpty(p_project.Objective))
                    ErrorMsgs.Add("Objective is required");

                if (string.IsNullOrEmpty(p_project.Methodology))
                    ErrorMsgs.Add("Methodology is required");

                if (string.IsNullOrEmpty(p_project.Output))
                    ErrorMsgs.Add("Output is required");
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
                if (string.IsNullOrEmpty(p_project.ReviewerNotes))
                    ErrorMsgs.Add("Project Code is required");

            }
            return ErrorMsgs;
        }


        [HttpPost("upload-methodology-file")]
        [RequestSizeLimit(int.MaxValue)]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
           ValueLengthLimit = int.MaxValue,
           KeyLengthLimit = int.MaxValue,
           ValueCountLimit = int.MaxValue,
           MultipartHeadersCountLimit = int.MaxValue,
           MultipartHeadersLengthLimit = int.MaxValue,
           MultipartBoundaryLengthLimit = int.MaxValue)]
        public async Task<TNAUPMSActionResult> UploadMethodologyFile(FileUploadRequest p_FileUploadRequest)
        {
            TNAUPMSActionResult authResult = new TNAUPMSActionResult();

            if (p_FileUploadRequest != null && Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files.Sum(file => file.Length) > 0)
            {
                trnproject pro = await this.Repository.GetByIdAsync(p_FileUploadRequest.Id);
                if (pro != null)
                {
                    string FilesUploadPath = GetUploadPath(p_FileUploadRequest.Id.ToString());

                   // IMasInstitutionApplnDocumentsRepository filesRepo = this.Provider.GetService<IMasInstitutionApplnDocumentsRepository>();
                    int errorFileCount = 0;

                    foreach (var instFile in Request.Form.Files)
                    {
                        if (instFile.Length > 0)
                        {
                            string newFile = "Methodology_" + pro.id.ToString() + ".txt";
                            try
                            {
                                string newFilePath = IO.Path.Combine(FilesUploadPath, newFile);

                                if (IO.File.Exists(newFilePath))
                                    IO.File.Delete(newFilePath);

                                using (IO.FileStream stream = new IO.FileStream(newFilePath, IO.FileMode.Create))
                                {
                                    await instFile.CopyToAsync(stream);
                                }

                                //MasVendorDocuments existingFile = await filesRepo.GetVendorDocumentsbyfile(vendor.Id, vendorFile.FileName);
                                //if (p_FileUploadRequest.FileType == "Logo")
                                //{
                                //    MasVendors mv = await this.Repository.GetByIdAsync(p_FileUploadRequest.Id);
                                //    mv.ImageFile = vendorFile.FileName;
                                //    await this.Repository.UpdateOneAsync(mv);
                                //}


                                //MasInstitutionApplnDocuments appDocs = new MasInstitutionApplnDocuments()
                                //{
                                //    IAFId = app.id,
                                //    DocumentName = p_FileUploadRequest.DocumentName,
                                //    FileName = instFile.FileName,

                                //};
                                //await filesRepo.InsertOneAsync(appDocs);


                            }
                            catch (Exception ex)
                            {
                                errorFileCount += 1;
                            }
                        }
                    }
                    if (errorFileCount == 0)
                        authResult.result = "Success";
                    else
                        authResult.ErrorMsgs.Add(string.Format("Error processing {0}/{1} files",
                            errorFileCount, Request.Form.Files.Count));
                }
                else
                    authResult.ErrorMsgs.Add("Invalid Vendor");
            }
            else
                authResult.ErrorMsgs.Add("Invalid input");

            return authResult;
        }

        private string GetUploadPath(string p_id)
        {
            IOptions<AppSettings> appSettings = this.Provider.GetService<IOptions<AppSettings>>();

            if (string.IsNullOrEmpty(appSettings.Value.UploadFilesPath))
                return string.Empty;

            string projectFolderPath = IO.Path.Combine(appSettings.Value.UploadFilesPath, "Projects", p_id);

            if (!IO.Directory.Exists(projectFolderPath))
                IO.Directory.CreateDirectory(projectFolderPath);

            return projectFolderPath;
        }


        #region Reports & Analytics
        [HttpGet("get-Inst-wise-Projectcount/{p_DeptId}")]
        public async Task<List<AnalyticsModel>> get_Inst_wise_Projectcount(string p_DeptId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_DeptId);
                List<AnalyticsModel> inst = await this.Repository.RNA_InstitutionWiseProjectCount(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-fund-wise-Projectcount/{p_DeptId}")]
        public async Task<List<AnalyticsModel>> get_fund_wise_Projectcount(string p_DeptId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_DeptId);
                List<AnalyticsModel> inst = await this.Repository.RNA_FundWiseProjectVal(pId);
                return inst;
            }
            return null;
        }

        [HttpGet("get-science-wise-Projectcount/{p_DeptId}")]
        public async Task<List<AnalyticsModel>> get_science_wise_Projectcount(string p_DeptId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int pId = int.Parse(p_DeptId);
                List<AnalyticsModel> inst = await this.Repository.RNA_ScienceWiseProjectVal(pId);
                return inst;
            }
            return null;
        }
        #endregion
    }
}
