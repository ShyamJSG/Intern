using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Linq.Expressions;
using TNAUPMS.Domains.Extensions;


namespace TNAUPMS.Domains.Repositories.Master
{
    public interface ImasscienceRepository : IRepository<masscience>
    {
        Task<masscience> GetScienceById(int ScienceId);
        Task<List<masscience>> GetScienceAll();
      
    }

    public class masscienceRepository : Repository<masscience>, ImasscienceRepository
    {
        public masscienceRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }
        public async Task<masscience> GetScienceById(int ScienceId)
        {
            List<Expression<Func<masscience, bool>>> filterConditions = new List<Expression<Func<masscience, bool>>>();
            Expression<Func<masscience, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masscience>(a => a.id, OperationExpression.Equals, ScienceId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masscience>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masscience, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }
            if (filters == null)
                return default;
            return await this.GetOneAsync(filters);
        }

        public async Task<List<masscience>> GetScienceAll()
        {
            List<Expression<Func<masscience, bool>>> filterConditions = new List<Expression<Func<masscience, bool>>>();
            Expression<Func<masscience, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masscience>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masscience, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;
            return await this.GetManyAsync(filters);
        }
    }
}