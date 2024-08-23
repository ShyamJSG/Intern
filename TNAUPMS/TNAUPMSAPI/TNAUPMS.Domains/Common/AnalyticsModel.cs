using TNAUPMS.Domains.Models.Config;
//using TNAUPMS.Domains.Models.Master.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class AnalyticsModel
    {
        public int KeyId { get; set; }
        public string KeyCode { get; set; }
        public string KeyName { get; set; }
        public int ProjectCount { get; set; }
        public decimal ProjectValue { get; set; }
    }
    
}
