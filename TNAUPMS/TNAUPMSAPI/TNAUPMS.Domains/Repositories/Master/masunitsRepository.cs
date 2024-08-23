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
using Microsoft.EntityFrameworkCore;


namespace TNAUPMS.Domains.Repositories.Master
{
    public interface ImasunitsRepository : IRepository<masunits>
    {
        Task<List<masunits>> GetUnitByInstId(int InstId);
        Task<masunits> GetUnitById(int unitId);
        Task<List<masunits>> GetUnitsAll();
    }

    public class masunitsRepository : Repository<masunits>, ImasunitsRepository
    {
        public masunitsRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        //public async Task<masunits> GetUnitById(int unitId)
        //{
        //    List<Expression<Func<masunits, bool>>> filterConditions = new List<Expression<Func<masunits, bool>>>();
        //    Expression<Func<masunits, bool>> filters = null;
        //    Int16 isactive = 1;
        //    filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.id, OperationExpression.Equals, unitId));
        //    filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.Isactive, OperationExpression.Equals, isactive));

        //    if (filterConditions.Count > 0)
        //    {
        //        foreach (Expression<Func<masunits, bool>> filterCondition in filterConditions)
        //            filters = (filters == null ? filterCondition : filters.And(filterCondition));
        //    }

        //    var searchQuery = (from unit in this.GetQueryable(filters)
        //                       join inst in DataContext.masinstitute on unit.InstituteId equals inst.id
        //                       select new masunits()
        //                       {
        //                           id = unit.id,
        //                           Code = unit.Code,
        //                           UnitName = unit.UnitName,
        //                           AdminEmail = unit.AdminEmail,
        //                           InstituteId = unit.InstituteId,
        //                           InstName = inst.InstituteName,
        //                           CreatedBy = unit.CreatedBy,
        //                           CreatedOn = unit.CreatedOn,
        //                           UpdatedBy = unit.UpdatedBy,
        //                           UpdatedOn = unit.UpdatedOn,
        //                           Isactive = unit.Isactive
        //                       });

        //    return searchQuery.First();
        //}

        public async Task<List<masunits>> GetUnitByInstId(int InstId)
        {
            List<Expression<Func<masunits, bool>>> filterConditions = new List<Expression<Func<masunits, bool>>>();
            Expression<Func<masunits, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.InstituteId, OperationExpression.Equals, InstId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masunits, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from unit in this.GetQueryable(filters)
                               join inst in DataContext.masinstitute on unit.InstituteId equals inst.id
                               select new masunits()
                               {
                                   id = unit.id,
                                   Code = unit.Code,
                                   UnitName = unit.UnitName,
                                   AdminEmail = unit.AdminEmail,
                                   InstituteId = unit.InstituteId,
                                   InstName = inst.InstituteName,
                                   CreatedBy = unit.CreatedBy,
                                   CreatedOn = unit.CreatedOn,
                                   UpdatedBy = unit.UpdatedBy,
                                   UpdatedOn = unit.UpdatedOn,
                                   Isactive = unit.Isactive
                               });

            searchQuery = searchQuery.OrderBy(result => result.id).Distinct();
            return searchQuery.ToList();
        }

        //public async Task<List<masunits>> GetUnitsAll()
        //{
        //    List<Expression<Func<masunits, bool>>> filterConditions = new List<Expression<Func<masunits, bool>>>();
        //    Expression<Func<masunits, bool>> filters = null;
        //    Int16 isactive = 1;
        //    filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.Isactive, OperationExpression.Equals, isactive));

        //    if (filterConditions.Count > 0)
        //    {
        //        foreach (Expression<Func<masunits, bool>> filterCondition in filterConditions)
        //            filters = (filters == null ? filterCondition : filters.And(filterCondition));
        //    }

        //    var searchQuery = (from unit in this.GetQueryable(filters)
        //                       join inst in DataContext.masinstitute on unit.InstituteId equals inst.id
        //                       select new masunits()
        //                       {
        //                           id = unit.id,
        //                           Code = unit.Code,
        //                           UnitName = unit.UnitName,
        //                           AdminEmail = unit.AdminEmail,
        //                           InstituteId = unit.InstituteId,
        //                           InstName = inst.InstituteName,
        //                           CreatedBy = unit.CreatedBy,
        //                           CreatedOn = unit.CreatedOn,
        //                           UpdatedBy = unit.UpdatedBy,
        //                           UpdatedOn = unit.UpdatedOn,
        //                           Isactive = unit.Isactive
        //                       });

        //    searchQuery = searchQuery.OrderBy(result => result.id).Distinct();
        //    return searchQuery.ToList();
        //}
        public async Task<List<masunits>> GetUnitsAll()
        {
            List<Expression<Func<masunits, bool>>> filterConditions = new List<Expression<Func<masunits, bool>>>();
            Expression<Func<masunits, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masunits, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = from unit in this.GetQueryable(filters)
                              join inst in DataContext.masinstitute on unit.InstituteId equals inst.id
                              select new masunits()
                              {
                                  id = unit.id,
                                  Code = unit.Code,
                                  UnitName = unit.UnitName,
                                  AdminEmail = unit.AdminEmail,
                                  InstituteId = unit.InstituteId,
                                  InstName = inst.InstituteName,
                                  CreatedBy = unit.CreatedBy,
                                  CreatedOn = unit.CreatedOn,
                                  UpdatedBy = unit.UpdatedBy,
                                  UpdatedOn = unit.UpdatedOn,
                                  Isactive = unit.Isactive // This should match the database column type
                              };

            searchQuery = searchQuery.OrderBy(result => result.id).Distinct();
            return searchQuery.ToList();
        }


        public async Task<masunits> GetUnitById(int unitId)
        {
            List<Expression<Func<masunits, bool>>> filterConditions = new List<Expression<Func<masunits, bool>>>();
            Expression<Func<masunits, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.id, OperationExpression.Equals, unitId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masunits>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masunits, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = from unit in this.GetQueryable(filters)
                              join inst in DataContext.masinstitute on unit.InstituteId equals inst.id
                              select new masunits()
                              {
                                  id = unit.id,
                                  Code = unit.Code,
                                  UnitName = unit.UnitName,
                                  AdminEmail = unit.AdminEmail,
                                  InstituteId = unit.InstituteId,
                                  InstName = inst.InstituteName,
                                  CreatedBy = unit.CreatedBy,
                                  CreatedOn = unit.CreatedOn,
                                  UpdatedBy = unit.UpdatedBy,
                                  UpdatedOn = unit.UpdatedOn,
                                  Isactive = unit.Isactive
                              };

            var result = await searchQuery.FirstOrDefaultAsync();
            if (result == null)
            {
                Console.WriteLine($"No unit found with ID: {unitId}");
                return null; // Or throw an exception if appropriate
            }

            return result;
        }


    }
}