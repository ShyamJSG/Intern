using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

using TNAUPMS.Domains.Common;
using TNAUPMS.Domains.Models.Config;
using TNAUPMS.Domains.Models.Master;
using TNAUPMS.Domains.Models.Transaction;
using TNAUPMS.Domains.Repositories.Config;
using TNAUPMS.Domains.Repositories.Master;
using TNAUPMS.Domains.Repositories.Transaction;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using TNAUPMS.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TNAUPMS.WebApi.Controllers
{
    [Route("api/account")]
    public class AccountController : TNAUPMS.Controllers.Controller
    {
        private readonly SignInManager<ConfigUser> _signInManager;
        private readonly UserManager<ConfigUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ConfigUser> userManager,
            SignInManager<ConfigUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register-admin")]
        public async Task<AuthResult> RegisterAdmin([FromBody] Signupmodel p_user)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterAdminUser(p_user);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_user.EmailId,
                        Name = p_user.Name,
                        PhoneNumber = p_user.MobileNo,
                        UserName = p_user.EmailId
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_user.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapAdmin(user.Id);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";
                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);
            }

            return authResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpPost("register-vc")]
        public async Task<AuthResult> RegisterVCuser([FromBody] Signupmodel p_user)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterAdminUser(p_user);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_user.EmailId,
                        Name = p_user.Name,
                        PhoneNumber = p_user.MobileNo,
                        UserName = p_user.EmailId
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_user.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapVC(user.Id);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";
                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.ErrorMsgs.Add(ex.Message);
            }

            return authResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpPost("register-director")]
        public async Task<AuthResult> RegisterInstitution([FromBody] masdepartment p_dept)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterDeparment(p_dept);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_dept.DirectorEmail,
                        Name = p_dept.DirectorName,
                        PhoneNumber = p_dept.DirectorMobileNo,
                        UserName = p_dept.DirectorEmail
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_dept.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapDirector(user.Id);

                        ImasdepartmentRepository deptRepo = this.Provider.GetService<ImasdepartmentRepository>();
                        masdepartment dept= new();
                        dept.Code = p_dept.Code;
                        dept.DepartmentName= p_dept.DepartmentName;
                        dept.DirectorName= p_dept.DirectorName;
                        dept.DirectorEmail= p_dept.DirectorEmail;
                        dept.DirectorMobileNo= p_dept.DirectorMobileNo;
                        dept.CreatedBy= "Admin";
                        dept.CreatedOn= System.DateTime.Now;
                        await deptRepo.InsertOneAsync(dept);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";
                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);

            }

            return authResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpPost("register-principal")]
        public async Task<AuthResult> RegisterPrincipal([FromBody] masinstitute p_inst)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterPrincipal(p_inst);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_inst.PrincipalEmail,
                        Name = p_inst.PrincipalName,
                        PhoneNumber = p_inst.PrincipalMobileNo,
                        UserName = p_inst.PrincipalEmail
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_inst.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapPrincipal(user.Id);

                        ImasinstituteRepository instRepo = this.Provider.GetService<ImasinstituteRepository>();
                        masinstitute instNew = new();
                        instNew.InstituteName = p_inst.InstituteName;
                        instNew.Code= p_inst.Code;
                        instNew.Address= p_inst.Address;
                        instNew.City= p_inst.City;
                        instNew.District= p_inst.District;
                        instNew.Pincode = p_inst.Pincode;
                        instNew.PrincipalName = p_inst.PrincipalName;
                        instNew.PrincipalEmail = p_inst.PrincipalEmail;
                        instNew.PrincipalMobileNo = p_inst.PrincipalMobileNo;
                        instNew.CreatedBy = "Admin";
                        instNew.CreatedOn = System.DateTime.Now;
                        await instRepo.InsertOneAsync(instNew);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";

                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);

            }

            return authResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpPost("register-unit")]
        public async Task<AuthResult> RegisterUnit([FromBody] masunits p_unit)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterUnit(p_unit);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_unit.AdminEmail,
                        Name = p_unit.UnitName,
                        PhoneNumber = "",
                        UserName = p_unit.AdminEmail
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_unit.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapUnitAdmin(user.Id);

                        ImasunitsRepository unitRepo = this.Provider.GetService<ImasunitsRepository>();
                        masunits unituser = new();
                        unituser.Code = p_unit.Code;
                        unituser.UnitName= p_unit.UnitName;
                        unituser.AdminEmail = p_unit.AdminEmail;
                        unituser.InstituteId= p_unit.InstituteId;
                        unituser.CreatedBy= "Admin";
                        unituser.CreatedOn= System.DateTime.Now;
                        await unitRepo.InsertOneAsync(unituser);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";

                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);

            }

            return authResult;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpPost("register-investigator")]
        public async Task<AuthResult> RegisterInvestigator([FromBody] masinvestigator p_investigator)
        {
            AuthResult authResult = new AuthResult();

            try
            {
                authResult.ErrorMsgs = await CanRegisterInvestigator(p_investigator);

                if (authResult.ErrorMsgs.Count == 0)
                {
                    ConfigUser user = new ConfigUser()
                    {
                        Email = p_investigator.EmailId,
                        Name = p_investigator.InvestigatorName,
                        PhoneNumber = p_investigator.MobileNo,
                        UserName = p_investigator.EmailId
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, p_investigator.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        // Map role
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        await userRoleRepo.MapInvestigator(user.Id);

                        ImasinvestigatorRepository investigatorRepo = this.Provider.GetService<ImasinvestigatorRepository>();
                        masinvestigator investNew = new();
                        investNew.InvestigatorName= p_investigator.InvestigatorName;
                        investNew.Designation= p_investigator.Designation;
                        investNew.Qualification= p_investigator.Qualification;
                        investNew.DepartmentId= p_investigator.DepartmentId;
                        investNew.InstituteId= p_investigator.InstituteId;
                        investNew.EmailId= p_investigator.EmailId;
                        investNew.MobileNo= p_investigator.MobileNo;
                        investNew.CreatedBy = "Admin";
                        investNew.CreatedOn = System.DateTime.Now;
                        await investigatorRepo.InsertOneAsync(investNew);

                        authResult.Token = await GenerateJwtToken(user);
                        authResult.result = "Success";

                    }
                    else
                        authResult.ErrorMsgs.Add("Registration failed");
                }
            }
            catch (Exception ex)
            {
                authResult.result = "Fail";
                authResult.ErrorMsgs.Add(ex.Message);

            }

            return authResult;
        }


        [HttpPost("connect")]
        public async Task<AuthResult> Connect([FromBody] OpenIddictRequest p_request)
        {
            AuthResult authResult = new AuthResult()
            {
                ErrorMsgs = new List<string>()
            };

            #region Validation

            if (p_request == null)
                authResult.ErrorMsgs.Add("Invalid request");
            else
            {
                if (string.IsNullOrEmpty(p_request.Username))
                    authResult.ErrorMsgs.Add("Username is required");

                if (string.IsNullOrEmpty(p_request.Password))
                    authResult.ErrorMsgs.Add("Password is required");
            }

            #endregion

            if (authResult.ErrorMsgs.Count == 0)
            {
                ConfigUser user = await _userManager.FindByNameAsync(p_request.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(p_request.Username, p_request.Password, false, false);

                    if (result.Succeeded)
                    {
                        authResult.Token = await GenerateJwtToken(user);
                    }
                }

                if (string.IsNullOrEmpty(authResult.Token))
                {
                    authResult.ErrorMsgs.Add("Invalid username/password");
                }
                else
                {
                    ISecurityContext securityContext = this.Provider.GetService<ISecurityContext>();
                    ConfigRole role = null;
                    if (user != null)
                    {
                        IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                        role = await userRoleRepo.GetRoleByUserId(user.Id);
                        authResult.UserType = role.RoleName;

                        //IMasAppUsersRepository appUserRepo = this.Provider.GetService<IMasAppUsersRepository>();
                        //MasAppUsers appUser = new MasAppUsers();
                        //appUser = await appUserRepo.GetByEmail(p_request.Username);

                        //ImasinstitutionRepository instRepo = this.Provider.GetService<ImasinstitutionRepository>();
                        //masinstitution inst = new masinstitution();
                        //inst = await instRepo.GetByEmail(p_request.Username);

                        //if (role.RoleName.ToLower() == "institution")
                        //{
                        //    if (inst != null) { authResult.UserId = inst.id; }
                        //}
                        //else
                        //{
                        //    authResult.UserId = appUser.id;
                        //}
                    }
                }
            }
            return authResult;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Query")]
        [HttpGet("get-user")]
        public async Task<UserProfile> GetUser()
        {
            ISecurityContext securityContext = this.Provider.GetService<ISecurityContext>();
            string userName = securityContext.GetUsername();

            if (string.IsNullOrEmpty(userName))
                return default(UserProfile);

            ConfigUser user = await _userManager.FindByNameAsync(userName);
            ConfigRole role = null;

            if (user != null)
            {
                IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
                role = await userRoleRepo.GetRoleByUserId(user.Id);
            }

            UserProfile profile = new UserProfile()
            {
                Role = role,
                User = user
                //ReelerId = securityContext.GetReelerId(),
                //StaffId = securityContext.GetStaffId()
            };
            return profile;
        }


        private async Task<List<string>> CanRegisterAdminUser(Signupmodel Signupmodel)
        {
            List<string> ErrorMsgs = new List<string>();

            if (Signupmodel == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(Signupmodel.EmailId))
                    ErrorMsgs.Add("Email Id is required");
                else
                {
                    ConfigUser user = await _userManager.FindByNameAsync(Signupmodel.EmailId);
                    if (user != null)
                        ErrorMsgs.Add("Email Id already exists");
                }

                if (string.IsNullOrEmpty(Signupmodel.Name))
                    ErrorMsgs.Add("Name is required");

                if (string.IsNullOrEmpty(Signupmodel.MobileNo))
                    ErrorMsgs.Add("Mobile No is required");

                if (string.IsNullOrEmpty(Signupmodel.Password))
                    ErrorMsgs.Add("Password is required");
            }

            return ErrorMsgs;
        }
        private async Task<List<string>> CanRegisterPrincipal(masinstitute p_inst)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_inst == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_inst.PrincipalEmail))
                    ErrorMsgs.Add("Email Id is required");
                else
                {
                    ConfigUser user = await _userManager.FindByNameAsync(p_inst.PrincipalEmail);
                    if (user != null)
                        ErrorMsgs.Add("Email Id already exists");
                }

                if (string.IsNullOrEmpty(p_inst.PrincipalName))
                    ErrorMsgs.Add("Principal Name is required");

                if (string.IsNullOrEmpty(p_inst.Password))
                    ErrorMsgs.Add("Password is required");
            }

            return ErrorMsgs;
        }
        private async Task<List<string>> CanRegisterDeparment(masdepartment p_dept)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_dept == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_dept.DirectorEmail))
                    ErrorMsgs.Add("Email Id is required");
                else
                {
                    ConfigUser user = await _userManager.FindByNameAsync(p_dept.DirectorEmail);
                    if (user != null)
                        ErrorMsgs.Add("Email Id already exists");
                }

                if (string.IsNullOrEmpty(p_dept.DepartmentName))
                    ErrorMsgs.Add("Department Name is required");

                if (string.IsNullOrEmpty(p_dept.DirectorName))
                    ErrorMsgs.Add("Director Name is required");

                if (string.IsNullOrEmpty(p_dept.DirectorMobileNo))
                    ErrorMsgs.Add("Mobile No is required");

                if (string.IsNullOrEmpty(p_dept.Password))
                    ErrorMsgs.Add("Password is required");
            }

            return ErrorMsgs;
        }
        private async Task<List<string>> CanRegisterUnit(masunits p_units)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_units == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_units.AdminEmail))
                    ErrorMsgs.Add("Email Id is required");
                else
                {
                    ConfigUser user = await _userManager.FindByNameAsync(p_units.AdminEmail);
                    if (user != null)
                        ErrorMsgs.Add("Email Id already exists");
                }

                if (string.IsNullOrEmpty(p_units.UnitName))
                    ErrorMsgs.Add("Unit Name is required");

                if (string.IsNullOrEmpty(p_units.Password))
                    ErrorMsgs.Add("Password is required");

            }

            return ErrorMsgs;
        }
        private async Task<List<string>> CanRegisterInvestigator(masinvestigator p_investgator)
        {
            List<string> ErrorMsgs = new List<string>();

            if (p_investgator == null)
                ErrorMsgs.Add("Invalid data");
            else
            {
                if (string.IsNullOrEmpty(p_investgator.EmailId))
                    ErrorMsgs.Add("Email Id is required");
                else
                {
                    ConfigUser user = await _userManager.FindByNameAsync(p_investgator.EmailId);
                    if (user != null)
                        ErrorMsgs.Add("Email Id already exists");
                }

                if (string.IsNullOrEmpty(p_investgator.InvestigatorName))
                    ErrorMsgs.Add("Investigator Name is required");

                if (string.IsNullOrEmpty(p_investgator.MobileNo))
                    ErrorMsgs.Add("Mobile No is required");

                if (string.IsNullOrEmpty(p_investgator.Password))
                    ErrorMsgs.Add("Password is required");

            }

            return ErrorMsgs;
        }
        private async Task<string> GenerateJwtToken(ConfigUser p_user)
        {
            var claims = new List<Claim>(){

            new Claim(OpenIddictConstants.Claims.Subject, p_user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(OpenIddictConstants.Claims.Name, p_user.Name),
                new Claim(OpenIddictConstants.Claims.ClientId, p_user.Id.ToString()),
                new Claim(OpenIddictConstants.Claims.Username,p_user.Email),
                new Claim(OpenIddictConstants.Claims.PhoneNumber,p_user.PhoneNumber)
            };

            IConfigUserRoleRepository userRoleRepo = this.Provider.GetService<IConfigUserRoleRepository>();
            ConfigRole role = await userRoleRepo.GetRoleByUserId(p_user.Id);

            if (role != null)
            {
                string roleName = role.RoleName.ToLower();

                claims.Add(new Claim("Role", roleName));


                switch (roleName)
                {
                    case "admin":
                        //IMasAppUsersRepository appUserRepo = this.Provider.GetService<IMasAppUsersRepository>();
                        //MasAppUsers appUser = new MasAppUsers();
                        //appUser = await appUserRepo.GetByEmail(p_user.Email);
                        claims.Add(new Claim("IsAdmin", true.ToString()));
                        //claims.Add(new Claim("AdminId", appUser.id.ToString()));
                        break;
                    case "director":
                        //IMasAppUsersRepository appUserRepo1 = this.Provider.GetService<IMasAppUsersRepository>();
                        //MasAppUsers appUser1 = new MasAppUsers();
                        //appUser1 = await appUserRepo1.GetByEmail(p_user.Email);
                        claims.Add(new Claim("IsDirector", true.ToString()));
                        //claims.Add(new Claim("UserId", appUser1.id.ToString()));
                        break;
                    case "principal":
                        claims.Add(new Claim("IsPrincipal", true.ToString()));
                        break;
                    case "unitadmin":
                        claims.Add(new Claim("IsUnitAdmin", true.ToString()));
                        break;
                    case "investigator":
                        claims.Add(new Claim("IsInvestigator", true.ToString()));
                        break;

                }
            }

            // Audience
            claims.Add(new Claim(OpenIddictConstants.Claims.Audience, _configuration[JwtBearer.Audience]));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtBearer.JwtKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration[JwtBearer.Authority],
                _configuration[JwtBearer.Audience],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
