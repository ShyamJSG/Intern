using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnprojecttaskreport : Model
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public int ReportedBy { get; set; }
        public DateTime ReportedDate { get; set; }
        public string ReportDetails { get; set; }
        public string ReportFiles { get; set; }
        public int Isactive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string ReportedUserName { get; set; }


    }
}