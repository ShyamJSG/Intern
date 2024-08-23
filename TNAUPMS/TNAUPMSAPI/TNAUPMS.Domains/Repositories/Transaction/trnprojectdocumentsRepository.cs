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
    public interface ItrnprojectdocumentsRepository : IRepository<trnprojectdocuments>
    {
    }

    public class trnprojectdocumentsRepository : Repository<trnprojectdocuments>, ItrnprojectdocumentsRepository
    {
        public trnprojectdocumentsRepository(IServiceProvider p_provider, TNAUPMSDbContext p_dataContext) : base(p_provider, p_dataContext)
        {

        }

    }
}