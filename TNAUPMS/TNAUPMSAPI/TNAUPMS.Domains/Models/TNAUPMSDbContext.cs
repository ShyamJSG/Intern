using TNAUPMS.Domains.Models.Config;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;

using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Models
{
    public class TNAUPMSDbContext : IdentityDbContext<ConfigUser, Role, int>
    {
        public TNAUPMSDbContext()
        {

        }

        public TNAUPMSDbContext(DbContextOptions<TNAUPMSDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }
        }

        #region Config

        public virtual DbSet<ConfigRole> ConfigRole { get; set; }
        public virtual DbSet<masdepartment> ConfigUser { get; set; }
        public virtual DbSet<ConfigUserRoles> ConfigUserRoles { get; set; }

        #endregion

        #region Master
        public virtual DbSet<masdepartment> masdepartment { get; set; }
        public virtual DbSet<masfundingagency> masfundingagency { get; set; }
        public virtual DbSet<masinstitute> masinstitute { get; set; }
        public virtual DbSet<masinvestigator> masinvestigator { get; set; }
        public virtual DbSet<masprojectsubtype> masprojectsubtype { get; set; }
        public virtual DbSet<masprojecttype> masprojecttype { get; set; }
        public virtual DbSet<masscience> masscience { get; set; }
        public virtual DbSet<massciencemeet> massciencemeet { get; set; }
        public virtual DbSet<masunits> masunits { get; set; }
        #endregion

        #region Transaction
        public virtual DbSet<trnproject> trnproject { get; set; }
        public virtual DbSet<trnprojectcoinvestigator> trnprojectcoinvestigator { get; set; }
        public virtual DbSet<trnprojectdocuments> trnprojectdocuments { get; set; }
        public virtual DbSet<trnprojecttask> trnprojecttask { get; set; }
        public virtual DbSet<trnprojecttaskextensioninfo> trnprojecttaskextensioninfo { get; set; }
        public virtual DbSet<trnprojecttaskreport> trnprojecttaskreport { get; set; }

        #endregion
    }

    public class BoolToIntConverter : ValueConverter<bool, int>
    {
        public BoolToIntConverter([CanBeNull] ConverterMappingHints mappingHints = null)
            : base(
                  v => Convert.ToInt32(v),
                  v => Convert.ToBoolean(v),
                  mappingHints)
        {
        }

        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
    }
}
