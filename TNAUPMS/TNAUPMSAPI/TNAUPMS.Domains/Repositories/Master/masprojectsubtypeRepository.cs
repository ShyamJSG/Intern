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
    public interface ImasprojectsubtypeRepository : IRepository<masprojectsubtype>
    {
        Task<masprojectsubtype> GetProSubTypeById(int PSTId);
        Task<List<masprojectsubtype>> GetProSubTypeByProjectId(int PTId);
        Task<List<masprojectsubtype>> GetProSubTypeByProjectAll();
    }

    public class masprojectsubtypeRepository : Repository<masprojectsubtype>, ImasprojectsubtypeRepository
    {
        public masprojectsubtypeRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }
        public async Task<masprojectsubtype> GetProSubTypeById(int PSTId)
        {
            List<Expression<Func<masprojectsubtype, bool>>> filterConditions = new List<Expression<Func<masprojectsubtype, bool>>>();
            Expression<Func<masprojectsubtype, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojectsubtype>(a => a.id, OperationExpression.Equals, PSTId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojectsubtype>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masprojectsubtype, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from subT in this.GetQueryable(filters)
                               join pro in DataContext.masprojecttype on subT.PTId equals pro.id
                               select new masprojectsubtype()
                               {
                                   id = subT.id,
                                   Code = subT.Code,
                                   SubTypeName = subT.SubTypeName,
                                   PTId = subT.PTId,
                                   ProjectTypeName = pro.ProjectType,
                                   CreatedBy = subT.CreatedBy,
                                   CreatedOn = subT.CreatedOn,
                                   UpdatedBy = subT.UpdatedBy,
                                   UpdatedOn = subT.UpdatedOn,
                                   Isactive = subT.Isactive
                               });

            return searchQuery.First();
        }

        public async Task<List<masprojectsubtype>> GetProSubTypeByProjectId(int PTId)
        {
            List<Expression<Func<masprojectsubtype, bool>>> filterConditions = new List<Expression<Func<masprojectsubtype, bool>>>();
            Expression<Func<masprojectsubtype, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojectsubtype>(a => a.PTId, OperationExpression.Equals, PTId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojectsubtype>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masprojectsubtype, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from subT in this.GetQueryable(filters)
                               join pro in DataContext.masprojecttype on subT.PTId equals pro.id
                               select new masprojectsubtype()
                               {
                                   id = subT.id,
                                   Code = subT.Code,
                                   SubTypeName = subT.SubTypeName,
                                   PTId = subT.PTId,
                                   ProjectTypeName = pro.ProjectType,
                                   CreatedBy = subT.CreatedBy,
                                   CreatedOn = subT.CreatedOn,
                                   UpdatedBy = subT.UpdatedBy,
                                   UpdatedOn = subT.UpdatedOn,
                                   Isactive = subT.Isactive
                               });

            return searchQuery.ToList();
        }
        public async Task<List<masprojectsubtype>> GetProSubTypeByProjectAll()
        {
            List<Expression<Func<masprojectsubtype, bool>>> filterConditions = new List<Expression<Func<masprojectsubtype, bool>>>();
            Expression<Func<masprojectsubtype, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masprojectsubtype>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masprojectsubtype, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from subT in this.GetQueryable(filters)
                               join pro in DataContext.masprojecttype on subT.PTId equals pro.id
                               select new masprojectsubtype()
                               {
                                   id = subT.id,
                                   Code=subT.Code,
                                   SubTypeName = subT.SubTypeName,
                                   PTId = subT.PTId,
                                   ProjectTypeName = pro.ProjectType,
                                   CreatedBy = subT.CreatedBy,
                                   CreatedOn = subT.CreatedOn,
                                   UpdatedBy = subT.UpdatedBy,
                                   UpdatedOn = subT.UpdatedOn,
                                   Isactive = subT.Isactive
                               });

            return searchQuery.ToList();
        }

    }
}