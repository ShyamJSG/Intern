using TNAUPMS.Domains.Models.Config;
//using TNAUPMS.Domains.Models.Master.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace TNAUPMS.Domains.Common
{
    public class ProjectCompletion
    {
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }

        public DateTime? CompletedOn { get; set; }
        public string ReportDetails { get; set; }
        public string ReportFile { get; set; }

        public string ReviewerNotes { get; set; }
        public Int16? ReviewerMark { get; set; }
        public DateTime? ReviewDate { get; set; }

    }
}
