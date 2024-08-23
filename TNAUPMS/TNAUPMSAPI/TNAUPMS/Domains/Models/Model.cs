﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace TNAUPMS.Domains.Models
{
    public abstract class NoIdModel
    {
        /// <summary>
        /// This function is called after an object is get from DB
        /// </summary>
        public virtual Task OnInitAsync(IServiceProvider p_provider)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// This function validate model, default to true
        /// </summary>
        public virtual Task<bool> ValidateAsync(IServiceProvider p_provider)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// This function is called before an object is persisted to DB
        /// </summary>
        public virtual Task<bool> OnPersistingAsync(IServiceProvider p_provider)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// This function is called after an object is persisted to DB
        /// </summary>
        public virtual Task OnPersistedAsync(IServiceProvider p_provider)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// This function will be called before an object is inserted to DB
        /// </summary>
        /// <param name="p_provider"></param>
        /// <returns>
        /// true: Continue with inserting
        /// false: Exit updating
        /// </returns>
        public virtual Task<bool> OnInsertingAsync(IServiceProvider p_provider)
        {
            return OnPersistingAsync(p_provider);
        }

        /// <summary>
        /// This function will be called after an object is inserted to DB
        /// </summary>
        public virtual Task OnInsertedAsync(IServiceProvider p_provider)
        {
            return OnPersistedAsync(p_provider);
        }

        /// <summary>
        /// This function will be called before updating a model
        /// </summary>
        /// <param name="p_provider"></param>
        /// <returns>
        /// true: Continue with updating
        /// false: Exit updating
        /// </returns>
        public virtual Task<bool> OnUpdatingAsync(Model p_current, IServiceProvider p_provider)
        {
            return OnPersistingAsync(p_provider);
        }

        /// <summary>
        /// This function will be called after updating a model
        /// </summary>
        public virtual Task OnUpdatedAsync(Model p_current, IServiceProvider p_provider)
        {
            return OnPersistedAsync(p_provider);
        }

        /// <summary>
        /// This function will be called before deleting a model
        /// </summary>
        /// <param name="p_provider"></param>
        /// <returns>
        /// true: Continue with deleting
        /// false: Exit deleting
        /// </returns>
        public virtual Task<bool> OnDeletingAsync(IServiceProvider p_provider)
        {
            return OnPersistingAsync(p_provider);
        }

        /// <summary>
        /// This function will be called after deleting a model
        /// </summary>
        public virtual Task OnDeletedAsync(IServiceProvider p_provider)
        {
            return OnPersistedAsync(p_provider);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Task ResetAsync(IServiceProvider provider)
        {
            return Task.CompletedTask;
        }
    }

    public abstract class NoIdLogModel : NoIdModel
    {
        public bool IsValid { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override async Task ResetAsync(IServiceProvider provider)
        {
            await base.ResetAsync(provider);
            this.IsValid = false;
            this.CreatedBy = null;
            this.CreatedDate = null;
            this.UpdatedBy = null;
            this.UpdatedDate = null;
        }
    }

    public abstract class Model : NoIdModel
    {
        [Column("id")]
        public int id { get; set; }

       
        public override async Task ResetAsync(IServiceProvider provider)
        {
            await base.ResetAsync(provider);
        }
    }

    public abstract class LogModel : Model
    {
        [Column("createdby")] 
        public string CreatedBy { get; set; }
        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        public override async Task ResetAsync(IServiceProvider provider)
        {
            await base.ResetAsync(provider);
            this.CreatedBy = null;
            this.UpdatedBy = null;
            this.UpdatedDate = null;
        }
    }
}
