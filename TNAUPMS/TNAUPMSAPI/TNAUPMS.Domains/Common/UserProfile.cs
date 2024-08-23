using TNAUPMS.Domains.Models.Config;
//using TNAUPMS.Domains.Models.Master.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class UserProfile
    {
        public ConfigUser User { get; set; }
        public ConfigRole Role { get; set; }
        public int? ReelerId { get; set; }
        public int? StaffId { get; set; }
    }
}