using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TNAUPMS.Domains.Extensions;

namespace TNAUPMS.Domains.Repositories.Master
{
    public interface ImasdepartmentRepository : IRepository<masdepartment>
    {
        Task<List<masdepartment>> GetAllDepartments();
    }

    public class masdepartmentRepository : Repository<masdepartment>, ImasdepartmentRepository
    {
        public masdepartmentRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        public async Task<List<masdepartment>> GetAllDepartments()
        {
            List<Expression<Func<masdepartment, bool>>> filterConditions = new List<Expression<Func<masdepartment, bool>>>();
            Expression<Func<masdepartment, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masdepartment>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masdepartment, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;

            return await this.GetManyAsync(filters);
        }
    }
}
