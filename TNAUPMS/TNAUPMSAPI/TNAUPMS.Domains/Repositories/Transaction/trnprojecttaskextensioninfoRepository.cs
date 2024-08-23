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
    public interface ItrnprojecttaskextensioninfoRepository : IRepository<trnprojecttaskextensioninfo>
    {
        Task<List<trnprojecttaskextensioninfo>> GetProjectTaskExtentionByProjectId(int ProjectId);
    }

    public class trnprojecttaskextensioninfoRepository : Repository<trnprojecttaskextensioninfo>, ItrnprojecttaskextensioninfoRepository
    {
        public trnprojecttaskextensioninfoRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }


        public async Task<List<trnprojecttaskextensioninfo>> GetProjectTaskExtentionByProjectId(int ProjectId)
        {
            List<Expression<Func<trnprojecttaskextensioninfo, bool>>> filterConditions = new List<Expression<Func<trnprojecttaskextensioninfo, bool>>>();
            Expression<Func<trnprojecttaskextensioninfo, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskextensioninfo>(a => a.ProjectId, OperationExpression.Equals, ProjectId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<trnprojecttaskextensioninfo>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<trnprojecttaskextensioninfo, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;
            return await this.GetManyAsync(filters);
        }


    }
}