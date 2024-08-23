using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TNAUPMS.Domains.Models;
using TNAUPMS.Domains.Repositories.Config;
using TNAUPMS.Domains.Repositories.Master;
using TNAUPMS.Domains.Repositories.Transaction;

namespace TNAUPMS.Domains
{
    public static class Startup
    {
        public static void ConfigureRepositoryServices(IServiceCollection p_services)
        {
            ConfigureUserServices(p_services);
            ConfigureMasterServices(p_services);
            ConfigurTransactionServices(p_services);
        }
        public static void ConfigureUserServices(IServiceCollection p_services)
        {
            p_services.AddScoped<IConfigRoleRepository>(p_provider => new ConfigRoleRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));
            p_services.AddScoped<IConfigUserRoleRepository>(p_provider => new ConfigUserRoleRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));
        }
        public static void ConfigureMasterServices(IServiceCollection p_services)
        {
            p_services.AddScoped<ImasdepartmentRepository>(p_provider => new masdepartmentRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasfundingagencyRepository>(p_provider => new masfundingagencyRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));
            
            p_services.AddScoped<ImasinstituteRepository>(p_provider => new masinstituteRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasinvestigatorRepository>(p_provider => new masinvestigatorRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasprojectsubtypeRepository>(p_provider => new masprojectsubtypeRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasprojecttypeRepository>(p_provider => new masprojecttypeRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImassciencemeetRepository>(p_provider => new massciencemeetRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasscienceRepository>(p_provider => new masscienceRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ImasunitsRepository>(p_provider => new masunitsRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));
        }
        public static void ConfigurTransactionServices(IServiceCollection p_services)
        {
            p_services.AddScoped<ItrnprojectcoinvestigatorRepository>(p_provider => new trnprojectcoinvestigatorRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ItrnprojectdocumentsRepository>(p_provider => new trnprojectdocumentsRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ItrnprojectRepository>(p_provider => new trnprojectRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ItrnprojecttaskextensioninfoRepository>(p_provider => new trnprojecttaskextensioninfoRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ItrnprojecttaskreportRepository>(p_provider => new trnprojecttaskreportRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));

            p_services.AddScoped<ItrnprojecttaskRepository>(p_provider => new trnprojecttaskRepository(p_provider,
                p_provider.GetService<TNAUPMSDbContext>()));
        }

    }
}
