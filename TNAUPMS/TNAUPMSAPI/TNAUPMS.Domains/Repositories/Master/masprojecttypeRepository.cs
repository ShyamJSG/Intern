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
    public interface ImasprojecttypeRepository : IRepository<masprojecttype>
    {
        Task<List<masprojecttype>> GetAllProjectType();
    }

    public class masprojecttypeRepository : Repository<masprojecttype>, ImasprojecttypeRepository
    {
        public masprojecttypeRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        public async Task<List<masprojecttype>> GetAllProjectType()
        {
            List<Expression<Func<masprojecttype, bool>>> filterConditions = new List<Expression<Func<masprojecttype, bool>>>();
            Expression<Func<masprojecttype, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojecttype>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masprojecttype, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;
            return await this.GetManyAsync(filters);
        }

    }
}