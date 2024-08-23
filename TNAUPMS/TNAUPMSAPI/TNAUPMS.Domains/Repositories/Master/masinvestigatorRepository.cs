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
    public interface ImasinvestigatorRepository : IRepository<masinvestigator>
    {
        Task<masinvestigator> GetInvestigatorById(int investigatorId);
        Task<List<masinvestigator>> GetInvestigatorByDeptId(int DeptId);
        Task<List<masinvestigator>> GetInvestigatorByInstId(int InstId);
        Task<List<masinvestigator>> GetInvestigatorAll();
    }

    public class masinvestigatorRepository : Repository<masinvestigator>, ImasinvestigatorRepository
    {
        public masinvestigatorRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

        public async Task<masinvestigator> GetInvestigatorById(int investigatorId)
        {
            List<Expression<Func<masinvestigator, bool>>> filterConditions = new List<Expression<Func<masinvestigator, bool>>>();
            Expression<Func<masinvestigator, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.id, OperationExpression.Equals, investigatorId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masinvestigator, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from invs in this.GetQueryable(filters)
                               join dept in DataContext.masunits on invs.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on invs.InstituteId equals inst.id
                               select new masinvestigator()
                               {
                                   id = invs.id,
                                   InvestigatorName = invs.InvestigatorName,
                                   DepartmentId = invs.DepartmentId,
                                   DeptName = dept.UnitName,
                                   InstituteId =invs.InstituteId,
                                   InstName = inst.InstituteName,
                                   Qualification = invs.Qualification,
                                   Designation=invs.Designation,
                                   EmailId = invs.EmailId,
                                   MobileNo = invs.MobileNo,
                                   CreatedBy = invs.CreatedBy,
                                   CreatedOn = invs.CreatedOn,
                                   UpdatedBy = invs.UpdatedBy,
                                   UpdatedOn = invs.UpdatedOn,
                                   Isactive = invs.Isactive
                               });

            return searchQuery.First();
        }

        public async Task<List<masinvestigator>> GetInvestigatorByDeptId(int DeptId)
        {
            List<Expression<Func<masinvestigator, bool>>> filterConditions = new List<Expression<Func<masinvestigator, bool>>>();
            Expression<Func<masinvestigator, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.DepartmentId, OperationExpression.Equals, DeptId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masinvestigator, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from invs in this.GetQueryable(filters)
                               join dept in DataContext.masunits on invs.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on invs.InstituteId equals inst.id
                               select new masinvestigator()
                               {
                                   id = invs.id,
                                   InvestigatorName = invs.InvestigatorName,
                                   DepartmentId = invs.DepartmentId,
                                   DeptName = dept.UnitName,
                                   InstituteId = invs.InstituteId,
                                   InstName = inst.InstituteName,
                                   Qualification = invs.Qualification,
                                   Designation = invs.Designation,
                                   EmailId = invs.EmailId,
                                   MobileNo = invs.MobileNo,
                                   CreatedBy = invs.CreatedBy,
                                   CreatedOn = invs.CreatedOn,
                                   UpdatedBy = invs.UpdatedBy,
                                   UpdatedOn = invs.UpdatedOn,
                                   Isactive = invs.Isactive
                               });

            return searchQuery.ToList();
        }

        public async Task<List<masinvestigator>> GetInvestigatorByInstId(int InstId)
        {
            List<Expression<Func<masinvestigator, bool>>> filterConditions = new List<Expression<Func<masinvestigator, bool>>>();
            Expression<Func<masinvestigator, bool>> filters = null;
            int isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.InstituteId, OperationExpression.Equals, InstId));
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.Isactive, OperationExpression.Equals, isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masinvestigator, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from invs in this.GetQueryable(filters)
                               join dept in DataContext.masunits on invs.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on invs.InstituteId equals inst.id
                               select new masinvestigator()
                               {
                                   id = invs.id,
                                   InvestigatorName = invs.InvestigatorName,
                                   DepartmentId = invs.DepartmentId,
                                   DeptName = dept.UnitName,
                                   InstituteId = invs.InstituteId,
                                   InstName = inst.InstituteName,
                                   Qualification = invs.Qualification,
                                   Designation = invs.Designation,
                                   EmailId = invs.EmailId,
                                   MobileNo = invs.MobileNo,
                                   CreatedBy = invs.CreatedBy,
                                   CreatedOn = invs.CreatedOn,
                                   UpdatedBy = invs.UpdatedBy,
                                   UpdatedOn = invs.UpdatedOn,
                                   Isactive = invs.Isactive
                               });

            return searchQuery.ToList();
        }

        public async Task<List<masinvestigator>> GetInvestigatorAll()
        {
            List<Expression<Func<masinvestigator, bool>>> filterConditions = new List<Expression<Func<masinvestigator, bool>>>();
            Expression<Func<masinvestigator, bool>> filters = null;
            int Isactive = 1;
            filterConditions.Add(Extensions.ExpressionHelper.GetCriteriaWhere<masinvestigator>(a => a.Isactive, OperationExpression.Equals, Isactive));

            if (filterConditions.Count > 0)
            {
                foreach (Expression<Func<masinvestigator, bool>> filterCondition in filterConditions)
                    filters = (filters == null ? filterCondition : filters.And(filterCondition));
            }

            var searchQuery = (from invs in this.GetQueryable(filters)
                               join dept in DataContext.masunits on invs.DepartmentId equals dept.id
                               join inst in DataContext.masinstitute on invs.InstituteId equals inst.id
                               select new masinvestigator()
                               {
                                   id = invs.id,
                                   InvestigatorName = invs.InvestigatorName,
                                   DepartmentId = invs.DepartmentId,
                                   DeptName = dept.UnitName,
                                   InstituteId = invs.InstituteId,
                                   InstName = inst.InstituteName,
                                   Qualification = invs.Qualification,
                                   Designation = invs.Designation,
                                   EmailId = invs.EmailId,
                                   MobileNo = invs.MobileNo,
                                   CreatedBy = invs.CreatedBy,
                                   CreatedOn = invs.CreatedOn,
                                   UpdatedBy = invs.UpdatedBy,
                                   UpdatedOn = invs.UpdatedOn,
                                   Isactive = invs.Isactive
                               });

            return searchQuery.ToList();
        }
    }
}