using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Linq.Expressions;
using TNAUPMS.Domains.Extensions;

namespace TNAUPMS.Domains.Repositories.Transaction
{
    public interface ItrnprojecttaskreportRepository : IRepository<trnprojecttaskreport>
    {
        Task<trnprojecttaskreport> GetProjectTaskReportById(int ReportId);
        Task<List<trnprojecttaskreport>> GetProjectTaskReportByTaskId(int TaskId);
        Task<List<trnprojecttaskreport>> GetProjectTaskReportByProjectId(int ProjectId);
    }

    public class trnprojecttaskreportRepository : Repository<trnprojecttaskreport>, ItrnprojecttaskreportRepository
    {
        public trnprojecttaskreportRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        public async Task<trnprojecttaskreport> GetProjectTaskReportById(int ReportId)
        {
            List<Expression<Func<trnprojecttaskreport, bool>>> filterConditions = new List<Expression<Func<trnprojecttaskreport, bool>>>();
            Expression<Func<trnprojecttaskreport, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.id, OperationExpression.Equals, ReportId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttaskreport, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from rpt in this.GetQueryable(filters)
                               join owner in DataContext.masinvestigator on rpt.ReportedBy equals owner.id
                               select new trnprojecttaskreport()
                               {
                                   id = rpt.id,
                                   ProjectId = rpt.ProjectId,
                                   TaskId=rpt.TaskId,
                                   ReportedBy = rpt.ReportedBy,
                                   ReportedUserName = owner.InvestigatorName,
                                   ReportedDate= rpt.ReportedDate,
                                   ReportFiles= rpt.ReportFiles,
                                   ReportDetails = rpt.ReportDetails,
                                   Isactive=rpt.Isactive
                               });

            return searchQuery.First();
        }
        public async Task<List<trnprojecttaskreport>> GetProjectTaskReportByTaskId(int TaskId)
        {
            List<Expression<Func<trnprojecttaskreport, bool>>> filterConditions = new List<Expression<Func<trnprojecttaskreport, bool>>>();
            Expression<Func<trnprojecttaskreport, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.TaskId, OperationExpression.Equals, TaskId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttaskreport, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from rpt in this.GetQueryable(filters)
                               join owner in DataContext.masinvestigator on rpt.ReportedBy equals owner.id
                               select new trnprojecttaskreport()
                               {
                                   id = rpt.id,
                                   ProjectId = rpt.ProjectId,
                                   TaskId = rpt.TaskId,
                                   ReportedBy = rpt.ReportedBy,
                                   ReportedUserName = owner.InvestigatorName,
                                   ReportedDate = rpt.ReportedDate,
                                   ReportFiles = rpt.ReportFiles,
                                   ReportDetails = rpt.ReportDetails,
                                   Isactive = rpt.Isactive
                               });

            return searchQuery.ToList();
        }

        public async Task<List<trnprojecttaskreport>> GetProjectTaskReportByProjectId(int ProjectId)
        {
            List<Expression<Func<trnprojecttaskreport, bool>>> filterConditions = new List<Expression<Func<trnprojecttaskreport, bool>>>();
            Expression<Func<trnprojecttaskreport, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.ProjectId, OperationExpression.Equals, ProjectId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskreport>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttaskreport, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from rpt in this.GetQueryable(filters)
                               join owner in DataContext.masinvestigator on rpt.ReportedBy equals owner.id
                               select new trnprojecttaskreport()
                               {
                                   id = rpt.id,
                                   ProjectId = rpt.ProjectId,
                                   TaskId = rpt.TaskId,
                                   ReportedBy = rpt.ReportedBy,
                                   ReportedUserName = owner.InvestigatorName,
                                   ReportedDate = rpt.ReportedDate,
                                   ReportFiles = rpt.ReportFiles,
                                   ReportDetails = rpt.ReportDetails,
                                   Isactive = rpt.Isactive
                               });

            return searchQuery.ToList();
        }

    }
}