using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TNAUPMS.Domains.Extensions;

namespace TNAUPMS.Domains.Repositories.Master
{
    public interface ImassciencemeetRepository : IRepository<massciencemeet>
    {
        Task<List<massciencemeet>> GetAllScienceMeet();
    }

    public class massciencemeetRepository : Repository<massciencemeet>, ImassciencemeetRepository
    {
        public massciencemeetRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }
        //public async Task<List<massciencemeet>> GetAllScienceMeet()
        //{
        //    List<Expression<Func<massciencemeet, bool>>> filterConditions = new List<Expression<Func<massciencemeet, bool>>>();
        //    Expression<Func<massciencemeet, bool>> filters = null;
        //    Int16 isactive = 1;
        //    filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<massciencemeet>(a => a.Isactive, OperationExpression.Equals, isactive));

        //    if (filterConditions.Count > 0)
        //    {
        //        foreach (Expression<Func<massciencemeet, bool>> filterCondition in filterConditions)
        //            filters = (filters == null ? filterCondition : filters.And(filterCondition));
        //    }

        //    if (filters == null)
        //        return default;

        //    return await this.GetManyAsync(filters);
        //}
        public async Task<List<massciencemeet>> GetAllScienceMeet()
        {
            List<Expression<Func<massciencemeet, bool>>> filterConditions = new List<Expression<Func<massciencemeet, bool>>>();
            Expression<Func<massciencemeet, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<massciencemeet>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<massciencemeet, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;

            return await this.GetManyAsync(filters);
        }


    }
}