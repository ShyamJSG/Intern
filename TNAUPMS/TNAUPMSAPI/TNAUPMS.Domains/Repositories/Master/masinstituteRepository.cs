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
    public interface ImasinstituteRepository : IRepository<masinstitute>
    {
        Task<List<masinstitute>> GetAllInstitution();
    }

    public class masinstituteRepository : Repository<masinstitute>, ImasinstituteRepository
    {
        public masinstituteRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }
        public async Task<List<masinstitute>> GetAllInstitution()
        {
            List<Expression<Func<masinstitute, bool>>> filterConditions = new List<Expression<Func<masinstitute, bool>>>();
            Expression<Func<masinstitute, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinstitute>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masinstitute, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;

            return await this.GetManyAsync(filters);
        }

    }
}