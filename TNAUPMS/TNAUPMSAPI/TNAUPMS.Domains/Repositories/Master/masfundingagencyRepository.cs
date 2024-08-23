using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Extensions;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TNAUPMS.Domains.Repositories.Master
{
    public interface ImasfundingagencyRepository : IRepository<masfundingagency>
    {
        Task<List<masfundingagency>> GetAllFundingAgency();
    }

    public class masfundingagencyRepository : Repository<masfundingagency>, ImasfundingagencyRepository
    {
        public masfundingagencyRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        public async Task<List<masfundingagency>> GetAllFundingAgency()
        {
            List<Expression<Func<masfundingagency, bool>>> filterConditions = new List<Expression<Func<masfundingagency, bool>>>();
            Expression<Func<masfundingagency, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masfundingagency>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masfundingagency, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            if (filters == null)
                return default;

            return await this.GetManyAsync(filters);
        }
    }
}
