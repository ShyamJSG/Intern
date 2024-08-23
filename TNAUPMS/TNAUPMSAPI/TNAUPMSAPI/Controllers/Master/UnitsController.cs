using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNAUPMS.Domains;
using TNAUPMS.Domains.Common;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;
using TNAUPMS.Domains.Repositories.Master;
using TNAUPMS.Domains.Repositories.Transaction;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using IO = System.IO;
using Microsoft.Extensions.Options;

namespace TNAUPMS.WebApi.Controllers.Master
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
    [Route("api/units")]
    public class UnitsController : TNAUPMSApiController<masunits, ImasunitsRepository>
    {
        private readonly IHostingEnvironment m_hostingEnvironment;
        public UnitsController(IHostingEnvironment p_hostingEnvironment)
        {
            m_hostingEnvironment = p_hostingEnvironment;
        }
        [HttpGet("get-unitsbyid/{p_Id}")]
        public async Task<masunits> GetUnitsAsync(string p_Id)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int fId = int.Parse(p_Id);
                masunits inst = await this.Repository.GetUnitById(fId);
                return inst;
            }
            return null;
        }
        [HttpGet("get-units-all")]
        public async Task<List<masunits>> GetUnitsAllAsync()
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                List<masunits> units = await this.Repository.GetUnitsAll();
                return units;
            }
            return null;
        }

        [HttpGet("get-units-by-institute/{p_InstId}")]
        public async Task<List<masunits>> GetUnitsByInstIdAsync(string p_InstId)
        {
            if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
            {
                int InstId = int.Parse(p_InstId);
                List<masunits> units= await this.Repository.GetUnitByInstId(InstId);
                return units;
            }
            return null;
        }

        [HttpPost("update-units")]
        public async Task<AuthResult> Saveunits([FromBody] masunits p_unit)
        {
            AuthResult authResult = new AuthResult();
            try
            {
                List<string> validationErrors = await IsValid(p_unit);
                if (validationErrors.Count == 0)
                {
                    if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                    {
                        if (p_unit.id > 0)
                        {
                            masunits mUnit = new masunits();
                            mUnit = await this.Repository.GetByIdAsync(p_unit.id);
                            if (mUnit != null)
                            {
                                mUnit.Code = p_unit.Code;
                                mUnit.UnitName = p_unit.UnitName;
                                mUnit.UpdatedOn = System.DateTime.Now;
                                mUnit.UpdatedBy = this.SecurityContext.GetUsername();
                                await this.Repository.UpdateOneAsync(mUnit);
                                authResult.result = "Success";
                            }
                        }
                    }
                    else
                    {
                        authResult.result = "Fail";
                        authResult.ErrorMsgs.Add("Invalid Authorization");
                    }
                }
                else
                {
                    authResult.result = "Fail";
                    authResult.ErrorMsgs = validationErrors;
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add("Invalid Authorization");
            }
            return authResult;
        }


        [HttpPost("remove/{p_Id}")]
        public async Task<AuthResult> RemoveUnit(string p_Id)
        {
            int _id = int.Parse(p_Id);
            AuthResult result = new AuthResult() { ErrorMsgs = new List<string>() };
            try
            {
                if (!string.IsNullOrEmpty(this.SecurityContext.GetUsername()))
                {
                    int isactive = 0;
                    masunits mUnit = await this.Repository.GetByIdAsync(_id);
                    mUnit.Isactive = isactive;
                    mUnit.UpdatedOn = System.DateTime.Now;
                    mUnit.UpdatedBy = this.SecurityContext.GetUsername();

                    await this.Repository.UpdateOneAsync(mUnit);
                    result.result = "Sucess";
                }
                else
                {
                    result.result = "Fail";
                    result.ErrorMsgs.Add("Invalid Authorization");
                }
            }
            catch (Exception ex)
            {
                result.result = "Fail";
                result.ErrorMsgs[0] = ex.Message;
            }
            return result;
        }

        private async Task<List<string>> IsValid(masunits p_unit, bool isNew = true)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_unit== null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_unit.Code))
                    ErrorMsgs.Add("Code is required");

                if (string.IsNullOrEmpty(p_unit.UnitName))
                    ErrorMsgs.Add("Unit Name is required");

                if (p_unit.InstituteId==0)
                    ErrorMsgs.Add("Institute is required");

            }

            return ErrorMsgs;
        }
    }
}
