using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;
using TNAUPMS.Domains.Extensions;
using TNAUPMS.Domains.Common;


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TNAUPMS.Domains.Repositories.Transaction
{
    public interface ItrnprojectRepository : IRepository<trnproject>
    {
        Task<List<trnproject>> GetProjectsAll();
        Task<trnproject> GetProjectById(int projectId);
        Task<List<trnproject>> GetProjectByDeptId(int DeptId);
        Task<List<trnproject>> GetProjectByInstitutionId(int InstId);
        Task<List<trnproject>> GetProjectByInvestigatorId(int InvestigatorId);
        Task<List<AnalyticsModel>> RNA_InstitutionWiseProjectCount(int p_DeptId);
        Task<List<AnalyticsModel>> RNA_FundWiseProjectVal(int p_DeptId);
        Task<List<AnalyticsModel>> RNA_ScienceWiseProjectVal(int p_DeptId);
    }
    public class trnprojectRepository : Repository<trnproject>, ItrnprojectRepository
    {
        protected TNAUPMSDbContext Context;
        public trnprojectRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {
            Context = p_dataContext;
        }

        public async Task<List<trnproject>> GetProjectsAll()
        {
            int isactive = 1;
            var searchQuery = from proj in Context.trnproject.Where(p => p.Isactive == isactive)
                              join dept in DataContext.masdepartment on proj.DepartmentId equals dept.id
                              join inst in DataContext.masinstitute on proj.InstituteId equals inst.id
                              join invest in DataContext.masinvestigator on proj.PrincipalInvestigator equals invest.id
                              join FAgency in DataContext.masfundingagency on proj.FundingAgencyId equals FAgency.id into vFALookup
                              from vFA in vFALookup.DefaultIfEmpty()
                              join SC in DataContext.masscience on proj.ScienceId equals SC.id into vSCLookup
                              from vSC in vSCLookup.DefaultIfEmpty()
                              join SCM in DataContext.massciencemeet on proj.ScienceMeetId equals SCM.id into vSCMLookup
                              from vSCM in vSCMLookup.DefaultIfEmpty()
                              join PT in DataContext.masprojecttype on proj.ProjectTypeId equals PT.id into vPTLookup
                              from vPT in vPTLookup.DefaultIfEmpty()
                              join PST in DataContext.masprojectsubtype on proj.ProjectSubTypeId equals PST.id into vPSTLookup
                              from vPST in vPSTLookup.DefaultIfEmpty()
                              select new trnproject()
                              {
                                  id = proj.id,
                                  ProjectCode = proj.ProjectCode,
                                  ProjectName = proj.ProjectName,
                                  DepartmentId = proj.DepartmentId,
                                  DeptName = dept.DepartmentName,
                                  InstituteId = proj.InstituteId,
                                  InstName = inst.InstituteName,
                                  ScienceId = proj.ScienceId,
                                  SciName = ((vSC != null) ? vSC.ScienceName : ""),
                                  ScienceMeetId = proj.ScienceMeetId,
                                  SciMeetName = ((vSCM != null) ? vSCM.ScienceMeetName : ""),
                                  ProjectTypeId = proj.ProjectTypeId,
                                  ProjectTypeName = ((vPT != null) ? vPT.ProjectType : ""),
                                  ProjectSubTypeId = proj.ProjectSubTypeId,
                                  ProjectSubTypeName = ((vPST != null) ? vPST.ProjectTypeName : ""),
                                  FundingAgencyId = proj.FundingAgencyId,
                                  FAName = ((vFA != null) ? vFA.FundingAgencyName : ""),
                                  PrincipalInvestigator = proj.PrincipalInvestigator,
                                  InvestigatorName = invest.InvestigatorName,
                                  StartDate = proj.StartDate,
                                  EndDate = proj.EndDate,
                                  Budget = proj.Budget,
                                  Objective = proj.Objective,
                                  Methodology = proj.Methodology,
                                  MethodologyFile = proj.MethodologyFile,
                                  Output = proj.Output,
                                  ActiveStatus = proj.ActiveStatus,
                                  CompletedOn = proj.CompletedOn,
                                  ReviewerNotes = proj.ReviewerNotes,
                                  ReviewerMark = proj.ReviewerMark,
                                  ReportFile = proj.ReportFile,
                                  CreatedBy = proj.CreatedBy,
                                  CreatedOn = proj.CreatedOn,
                                  UpdatedBy = proj.UpdatedBy,
                                  UpdatedOn = proj.UpdatedOn,
                              };
            return await searchQuery.ToListAsync();
        }
        public async Task<trnproject> GetProjectById(int projectId)
        {
            List<Expression<Func<trnproject, bool>>> filterConditions = new List<Expression<Func<trnproject, bool>>>();
            Expression<Func<trnproject, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.id, OperationExpression.Equals, projectId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnproject, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from proj in this.GetQueryable(filters)
                               join dept in DataContext.masdepartment on proj.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on proj.InstituteId equals inst.id
                               join invest in DataContext.masinvestigator on proj.PrincipalInvestigator equals invest.id
                               join FAgency in DataContext.masfundingagency on proj.FundingAgencyId equals FAgency.id into vFALookup
                               from vFA in vFALookup.DefaultIfEmpty()
                               join SC in DataContext.masscience on proj.ScienceId equals SC.id into vSCLookup
                               from vSC in vSCLookup.DefaultIfEmpty()
                               join SCM in DataContext.massciencemeet on proj.ScienceMeetId equals SCM.id into vSCMLookup
                               from vSCM in vSCMLookup.DefaultIfEmpty()
                               join PT in DataContext.masprojecttype on proj.ProjectTypeId equals PT.id into vPTLookup
                               from vPT in vPTLookup.DefaultIfEmpty()
                               join PST in DataContext.masprojectsubtype on proj.ProjectSubTypeId equals PST.id into vPSTLookup
                               from vPST in vPSTLookup.DefaultIfEmpty()
                               select new trnproject()
                               {
                                   id = proj.id,
                                   ProjectCode = proj.ProjectCode,
                                   ProjectName = proj.ProjectName,
                                   DepartmentId = proj.DepartmentId,
                                   DeptName = dept.DepartmentName,
                                   InstituteId = proj.InstituteId,
                                   InstName = inst.InstituteName,
                                   ScienceId = proj.ScienceId,
                                   SciName = ((vSC != null) ? vSC.ScienceName: ""),
                                   ScienceMeetId = proj.ScienceMeetId,
                                   SciMeetName = ((vSCM != null) ? vSCM.ScienceMeetName : ""),
                                   ProjectTypeId = proj.ProjectTypeId,
                                   ProjectTypeName = ((vPT != null) ? vPT.ProjectType: ""),
                                   ProjectSubTypeId = proj.ProjectSubTypeId,
                                   ProjectSubTypeName = ((vPST != null) ? vPST.ProjectTypeName: ""),
                                   FundingAgencyId = proj.FundingAgencyId,
                                   FAName = ((vFA != null) ? vFA.FundingAgencyName : ""),
                                   PrincipalInvestigator = proj.PrincipalInvestigator,
                                   InvestigatorName = invest.InvestigatorName,
                                   StartDate = proj.StartDate,
                                   EndDate = proj.EndDate,
                                   Budget = proj.Budget,
                                   Objective = proj.Objective,
                                   Methodology = proj.Methodology,
                                   MethodologyFile = proj.MethodologyFile,
                                   Output = proj.Output,
                                   ActiveStatus = proj.ActiveStatus,
                                   CompletedOn = proj.CompletedOn,
                                   ReviewerNotes = proj.ReviewerNotes,
                                   ReviewerMark = proj.ReviewerMark,
                                   ReportFile = proj.ReportFile,
                                   CreatedBy = proj.CreatedBy,
                                   CreatedOn = proj.CreatedOn,
                                   UpdatedBy = proj.UpdatedBy,
                                   UpdatedOn = proj.UpdatedOn,
                               });

            return searchQuery.First();
        }
        public async Task<List<trnproject>> GetProjectByDeptId(int DeptId)
        {
            List<Expression<Func<trnproject, bool>>> filterConditions = new List<Expression<Func<trnproject, bool>>>();
            Expression<Func<trnproject, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.DepartmentId, OperationExpression.Equals, DeptId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnproject, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from proj in this.GetQueryable(filters)
                               join dept in DataContext.masdepartment on proj.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on proj.InstituteId equals inst.id
                               join invest in DataContext.masinvestigator on proj.PrincipalInvestigator equals invest.id
                               join FAgency in DataContext.masfundingagency on proj.FundingAgencyId equals FAgency.id into vFALookup
                               from vFA in vFALookup.DefaultIfEmpty()
                               join SC in DataContext.masscience on proj.ScienceId equals SC.id into vSCLookup
                               from vSC in vSCLookup.DefaultIfEmpty()
                               join SCM in DataContext.massciencemeet on proj.ScienceMeetId equals SCM.id into vSCMLookup
                               from vSCM in vSCMLookup.DefaultIfEmpty()
                               join PT in DataContext.masprojecttype on proj.ProjectTypeId equals PT.id into vPTLookup
                               from vPT in vPTLookup.DefaultIfEmpty()
                               join PST in DataContext.masprojectsubtype on proj.ProjectSubTypeId equals PST.id into vPSTLookup
                               from vPST in vPSTLookup.DefaultIfEmpty()
                               select new trnproject()
                               {
                                   id = proj.id,
                                   ProjectCode = proj.ProjectCode,
                                   ProjectName = proj.ProjectName,
                                   DepartmentId = proj.DepartmentId,
                                   DeptName = dept.Code,
                                   InstituteId = proj.InstituteId,
                                   InstName = inst.Code,
                                   ScienceId = proj.ScienceId,
                                   SciName = ((vSC != null) ? vSC.Code : ""),
                                   ScienceMeetId = proj.ScienceMeetId,
                                   SciMeetName = ((vSCM != null) ? vSCM.Code : ""),
                                   ProjectTypeId = proj.ProjectTypeId,
                                   ProjectTypeName = ((vPT != null) ? vPT.Code : ""),
                                   ProjectSubTypeId = proj.ProjectSubTypeId,
                                   ProjectSubTypeName = ((vPST != null) ? vPST.Code : ""),
                                   FundingAgencyId = proj.FundingAgencyId,
                                   FAName = ((vFA != null) ? vFA.Code : ""),
                                   PrincipalInvestigator = proj.PrincipalInvestigator,
                                   InvestigatorName = invest.InvestigatorName,
                                   StartDate = proj.StartDate,
                                   EndDate = proj.EndDate,
                                   Budget = proj.Budget,
                                   Objective = proj.Objective,
                                   Methodology = proj.Methodology,
                                   MethodologyFile = proj.MethodologyFile,
                                   Output = proj.Output,
                                   ActiveStatus = proj.ActiveStatus,
                                   CompletedOn = proj.CompletedOn,
                                   ReviewerNotes = proj.ReviewerNotes,
                                   ReviewerMark = proj.ReviewerMark,
                                   ReportFile = proj.ReportFile,
                                   CreatedBy = proj.CreatedBy,
                                   CreatedOn = proj.CreatedOn,
                                   UpdatedBy = proj.UpdatedBy,
                                   UpdatedOn = proj.UpdatedOn,
                               });

            return searchQuery.ToList();
        }
        public async Task<List<trnproject>> GetProjectByInstitutionId(int InstId)
        {
            List<Expression<Func<trnproject, bool>>> filterConditions = new List<Expression<Func<trnproject, bool>>>();
            Expression<Func<trnproject, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.InstituteId, OperationExpression.Equals, InstId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnproject, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from proj in this.GetQueryable(filters)
                               join dept in DataContext.masdepartment on proj.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on proj.InstituteId equals inst.id
                               join invest in DataContext.masinvestigator on proj.PrincipalInvestigator equals invest.id
                               join FAgency in DataContext.masfundingagency on proj.FundingAgencyId equals FAgency.id into vFALookup
                               from vFA in vFALookup.DefaultIfEmpty()
                               join SC in DataContext.masscience on proj.ScienceId equals SC.id into vSCLookup
                               from vSC in vSCLookup.DefaultIfEmpty()
                               join SCM in DataContext.massciencemeet on proj.ScienceMeetId equals SCM.id into vSCMLookup
                               from vSCM in vSCMLookup.DefaultIfEmpty()
                               join PT in DataContext.masprojecttype on proj.ProjectTypeId equals PT.id into vPTLookup
                               from vPT in vPTLookup.DefaultIfEmpty()
                               join PST in DataContext.masprojectsubtype on proj.ProjectSubTypeId equals PST.id into vPSTLookup
                               from vPST in vPSTLookup.DefaultIfEmpty()
                               select new trnproject()
                               {
                                   id = proj.id,
                                   ProjectCode = proj.ProjectCode,
                                   ProjectName = proj.ProjectName,
                                   DepartmentId = proj.DepartmentId,
                                   DeptName = dept.DepartmentName,
                                   InstituteId = proj.InstituteId,
                                   InstName = inst.InstituteName,
                                   ScienceId = proj.ScienceId,
                                   SciName = ((vSC != null) ? vSC.ScienceName : ""),
                                   ScienceMeetId = proj.ScienceMeetId,
                                   SciMeetName = ((vSCM != null) ? vSCM.ScienceMeetName : ""),
                                   ProjectTypeId = proj.ProjectTypeId,
                                   ProjectTypeName = ((vPT != null) ? vPT.ProjectType : ""),
                                   ProjectSubTypeId = proj.ProjectSubTypeId,
                                   ProjectSubTypeName = ((vPST != null) ? vPST.ProjectTypeName : ""),
                                   FundingAgencyId = proj.FundingAgencyId,
                                   FAName = ((vFA != null) ? vFA.FundingAgencyName : ""),
                                   PrincipalInvestigator = proj.PrincipalInvestigator,
                                   InvestigatorName = invest.InvestigatorName,
                                   StartDate = proj.StartDate,
                                   EndDate = proj.EndDate,
                                   Budget = proj.Budget,
                                   Objective = proj.Objective,
                                   Methodology = proj.Methodology,
                                   MethodologyFile = proj.MethodologyFile,
                                   Output = proj.Output,
                                   ActiveStatus = proj.ActiveStatus,
                                   CompletedOn = proj.CompletedOn,
                                   ReviewerNotes = proj.ReviewerNotes,
                                   ReviewerMark = proj.ReviewerMark,
                                   ReportFile = proj.ReportFile,
                                   CreatedBy = proj.CreatedBy,
                                   CreatedOn = proj.CreatedOn,
                                   UpdatedBy = proj.UpdatedBy,
                                   UpdatedOn = proj.UpdatedOn,
                               });

            return searchQuery.ToList();
        }
        public async Task<List<trnproject>> GetProjectByInvestigatorId(int InvestigatorId)
        {
            List<Expression<Func<trnproject, bool>>> filterConditions = new List<Expression<Func<trnproject, bool>>>();
            Expression<Func<trnproject, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.PrincipalInvestigator, OperationExpression.Equals, InvestigatorId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnproject>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnproject, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from proj in this.GetQueryable(filters)
                               join dept in DataContext.masdepartment on proj.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on proj.InstituteId equals inst.id
                               join invest in DataContext.masinvestigator on proj.PrincipalInvestigator equals invest.id
                               join FAgency in DataContext.masfundingagency on proj.FundingAgencyId equals FAgency.id into vFALookup
                               from vFA in vFALookup.DefaultIfEmpty()
                               join SC in DataContext.masscience on proj.ScienceId equals SC.id into vSCLookup
                               from vSC in vSCLookup.DefaultIfEmpty()
                               join SCM in DataContext.massciencemeet on proj.ScienceMeetId equals SCM.id into vSCMLookup
                               from vSCM in vSCMLookup.DefaultIfEmpty()
                               join PT in DataContext.masprojecttype on proj.ProjectTypeId equals PT.id into vPTLookup
                               from vPT in vPTLookup.DefaultIfEmpty()
                               join PST in DataContext.masprojectsubtype on proj.ProjectSubTypeId equals PST.id into vPSTLookup
                               from vPST in vPSTLookup.DefaultIfEmpty()
                               select new trnproject()
                               {
                                   id = proj.id,
                                   ProjectCode = proj.ProjectCode,
                                   ProjectName = proj.ProjectName,
                                   DepartmentId = proj.DepartmentId,
                                   DeptName = dept.DepartmentName,
                                   InstituteId = proj.InstituteId,
                                   InstName = inst.InstituteName,
                                   ScienceId = proj.ScienceId,
                                   SciName = ((vSC != null) ? vSC.ScienceName : ""),
                                   ScienceMeetId = proj.ScienceMeetId,
                                   SciMeetName = ((vSCM != null) ? vSCM.ScienceMeetName : ""),
                                   ProjectTypeId = proj.ProjectTypeId,
                                   ProjectTypeName = ((vPT != null) ? vPT.ProjectType : ""),
                                   ProjectSubTypeId = proj.ProjectSubTypeId,
                                   ProjectSubTypeName = ((vPST != null) ? vPST.ProjectTypeName : ""),
                                   FundingAgencyId = proj.FundingAgencyId,
                                   FAName = ((vFA != null) ? vFA.FundingAgencyName : ""),
                                   PrincipalInvestigator = proj.PrincipalInvestigator,
                                   InvestigatorName = invest.InvestigatorName,
                                   StartDate = proj.StartDate,
                                   EndDate = proj.EndDate,
                                   Budget = proj.Budget,
                                   Objective = proj.Objective,
                                   Methodology = proj.Methodology,
                                   MethodologyFile = proj.MethodologyFile,
                                   Output = proj.Output,
                                   ActiveStatus = proj.ActiveStatus,
                                   CompletedOn = proj.CompletedOn,
                                   ReviewerNotes = proj.ReviewerNotes,
                                   ReviewerMark = proj.ReviewerMark,
                                   ReportFile = proj.ReportFile,
                                   CreatedBy = proj.CreatedBy,
                                   CreatedOn = proj.CreatedOn,
                                   UpdatedBy = proj.UpdatedBy,
                                   UpdatedOn = proj.UpdatedOn,
                               });

            return searchQuery.ToList();
        }

        public async Task<List<AnalyticsModel>> RNA_InstitutionWiseProjectCount(int p_DeptId)
        {
            int isactive = 1;
            var record = Context.trnproject.Where(t => t.DepartmentId == p_DeptId && t.Isactive == isactive && (t.ActiveStatus != "Cancel" || t.ActiveStatus != "Archive"));


            var GroupByQS = from std in record
                            group std by std.InstituteId into stdGroup
                            orderby stdGroup.Key descending
                            select new AnalyticsModel
                            {
                                KeyId = stdGroup.Key,
                                ProjectCount = stdGroup.Count(),
                                ProjectValue = stdGroup.Sum(x => x.Budget)
                            };

            var result = (from proj in GroupByQS
                               join inst in DataContext.masinstitute on proj.KeyId equals inst.id
                               select new AnalyticsModel()
                               { 
                                   KeyId = proj.KeyId,
                                   KeyCode = inst.Code,
                                   KeyName = inst.InstituteName,
                                   ProjectCount = proj.ProjectCount,
                                   ProjectValue=proj.ProjectValue
                               });


            if (result == null) { return default; }
            return result.ToList();
        }

        public async Task<List<AnalyticsModel>> RNA_FundWiseProjectVal(int p_DeptId)
        {
            int isactive = 1;
            var record = Context.trnproject.Where(t => t.DepartmentId == p_DeptId && t.Isactive == isactive && (t.ActiveStatus != "Cancel" || t.ActiveStatus != "Archive"));


            var GroupByQS = from std in record
                            group std by std.FundingAgencyId into stdGroup
                            orderby stdGroup.Key descending
                            select new AnalyticsModel
                            {
                                KeyId = stdGroup.Key,
                                ProjectCount = stdGroup.Count(),
                                ProjectValue = stdGroup.Sum(x => x.Budget)
                            };

            var result = (from proj in GroupByQS
                          join inst in DataContext.masfundingagency on proj.KeyId equals inst.id
                          select new AnalyticsModel()
                          {
                              KeyId = proj.KeyId,
                              KeyCode = inst.Code,
                              KeyName = inst.FundingAgencyName,
                              ProjectCount = proj.ProjectCount,
                              ProjectValue = proj.ProjectValue
                          });


            if (result == null) { return default; }
            return result.ToList();
        }

        public async Task<List<AnalyticsModel>> RNA_ScienceWiseProjectVal(int p_DeptId)
        {
            int isactive = 1;
            var record = Context.trnproject.Where(t => t.DepartmentId == p_DeptId && t.Isactive == isactive && (t.ActiveStatus != "Cancel" || t.ActiveStatus != "Archive"));


            var GroupByQS = from std in record
                            group std by std.ScienceId into stdGroup
                            orderby stdGroup.Key descending
                            select new AnalyticsModel
                            {
                                KeyId = stdGroup.Key,
                                ProjectCount = stdGroup.Count(),
                                ProjectValue = stdGroup.Sum(x => x.Budget)
                            };

            var result = (from proj in GroupByQS
                          join inst in DataContext.masscience on proj.KeyId equals inst.id
                          select new AnalyticsModel()
                          {
                              KeyId = proj.KeyId,
                              KeyCode = inst.Code,
                              KeyName = inst.ScienceName,
                              ProjectCount = proj.ProjectCount,
                              ProjectValue = proj.ProjectValue
                          });


            if (result == null) { return default; }
            return result.ToList();
        }
    }
}