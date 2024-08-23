using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnprojecttask : Model
    {
        public int ProjectId { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string TaskInformation { get; set; }
        public int AssignedTo { get; set; }
        public int? Reviewer { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ActiveStatus { get; set; }
        public DateTime? ExtnDate { get; set; } = DateTime.Now;
        public DateTime? CompletedOn { get; set; }
        public string ReportDetails { get; set; } = "";
        public string ReportFile { get; set; } = "";
        public string ReviewerNotes { get; set; } = "";
        public int? ReviewerMark { get; set; } = 0;
        public DateTime? ReviewDate { get; set; } = DateTime.Now;
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; } = "";
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "";
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;


        [NotMapped]
        public string ProjectCode { get; set; }
        [NotMapped]
        public string ProjectName { get; set; }
        [NotMapped]
        public string TaskOwner { get; set; }
    }
}