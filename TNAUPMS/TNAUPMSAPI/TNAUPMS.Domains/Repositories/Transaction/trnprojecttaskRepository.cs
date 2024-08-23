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
    public interface ItrnprojecttaskRepository : IRepository<trnprojecttask>
    {
        Task<trnprojecttask> GetProjectTaskById(int taskId);
        Task<List<trnprojecttask>> GetProjectTaskByProjectId(int projectId);
    }

    public class trnprojecttaskRepository : Repository<trnprojecttask>, ItrnprojecttaskRepository
    {
        public trnprojecttaskRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }
        public async Task<trnprojecttask> GetProjectTaskById(int taskId)
        {
            List<Expression<Func<trnprojecttask, bool>>> filterConditions = new List<Expression<Func<trnprojecttask, bool>>>();
            Expression<Func<trnprojecttask, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttask>(a => a.id, OperationExpression.Equals, taskId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttask>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttask, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from task in this.GetQueryable(filters)
                               join pro in DataContext.trnproject on task.ProjectId equals pro.id
                               join owner in DataContext.masinvestigator on task.AssignedTo equals owner.id
                               select new trnprojecttask()
                               {
                                   id = task.id,
                                   ProjectId=task.ProjectId,
                                   ProjectCode = pro.ProjectCode,
                                   ProjectName = pro.ProjectName,
                                   TaskCode = task.TaskCode,
                                   TaskName = task.TaskName,
                                   TaskInformation = task.TaskInformation,
                                   AssignedTo = task.AssignedTo,
                                   TaskOwner=owner.InvestigatorName,
                                   Reviewer = task.Reviewer,
                                   StartDate = task.StartDate,
                                   EndDate = task.EndDate,
                                   ActiveStatus = task.ActiveStatus,
                                   ExtnDate = task.ExtnDate,
                                   CompletedOn = task.CompletedOn,
                                   ReportDetails = task.ReportDetails,
                                   ReportFile = task.ReportFile,
                                   ReviewerNotes = task.ReviewerNotes,
                                   ReviewerMark = task.ReviewerMark,
                                   ReviewDate = task.ReviewDate,
                                   CreatedBy = task.CreatedBy,
                                   CreatedOn = task.CreatedOn,
                                   UpdatedBy = task.UpdatedBy,
                                   UpdatedOn = task.UpdatedOn
                               });

            return searchQuery.First();
        }
        public async Task<List<trnprojecttask>> GetProjectTaskByProjectId(int projectId)
        {
            List<Expression<Func<trnprojecttask, bool>>> filterConditions = new List<Expression<Func<trnprojecttask, bool>>>();
            Expression<Func<trnprojecttask, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttask>(a => a.ProjectId, OperationExpression.Equals, projectId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttask>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttask, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from task in this.GetQueryable(filters)
                               join pro in DataContext.trnproject on task.ProjectId equals pro.id
                               join owner in DataContext.masinvestigator on task.AssignedTo equals owner.id
                               select new trnprojecttask()
                               {
                                   id = task.id,
                                   ProjectId = task.ProjectId,
                                   ProjectCode = pro.ProjectCode,
                                   ProjectName = pro.ProjectName,
                                   TaskCode = task.TaskCode,
                                   TaskName = task.TaskName,
                                   TaskInformation = task.TaskInformation,
                                   AssignedTo = task.AssignedTo,
                                   TaskOwner = owner.InvestigatorName,
                                   Reviewer = task.Reviewer,
                                   StartDate = task.StartDate,
                                   EndDate = task.EndDate,
                                   ActiveStatus = task.ActiveStatus,
                                   ExtnDate = task.ExtnDate,
                                   CompletedOn = task.CompletedOn,
                                   ReportDetails = task.ReportDetails,
                                   ReportFile = task.ReportFile,
                                   ReviewerNotes = task.ReviewerNotes,
                                   ReviewerMark = task.ReviewerMark,
                                   ReviewDate = task.ReviewDate,
                                   CreatedBy = task.CreatedBy,
                                   CreatedOn = task.CreatedOn,
                                   UpdatedBy = task.UpdatedBy,
                                   UpdatedOn = task.UpdatedOn
                               });

            return searchQuery.ToList();
        }

    }
}