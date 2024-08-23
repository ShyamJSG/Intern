using TNAUPMS.Domains.Models.Config;
//using TNAUPMS.Domains.Models.Master.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class Signupmodel
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
    }
    public class FieldUserSignupmodel
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }
}
