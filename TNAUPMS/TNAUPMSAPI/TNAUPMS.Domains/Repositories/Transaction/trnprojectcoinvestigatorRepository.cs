using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Models.Transaction;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TNAUPMS.Domains.Extensions;

namespace TNAUPMS.Domains.Repositories.Transaction
{
    public interface ItrnprojectcoinvestigatorRepository : IRepository<trnprojectcoinvestigator>
    {
    }

    public class trnprojectcoinvestigatorRepository : Repository<trnprojectcoinvestigator>, ItrnprojectcoinvestigatorRepository
    {
        public trnprojectcoinvestigatorRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

    }
}