using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnproject : Model
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int DepartmentId { get; set; }
        public int InstituteId { get; set; }
        public int ScienceId { get; set; }
        public int? ScienceMeetId { get; set; }
        public int? ProjectTypeId { get; set; }
        public int? ProjectSubTypeId { get; set; }
        public int FundingAgencyId { get; set; }
        public int PrincipalInvestigator { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public string Objective { get; set; }
        public string Methodology { get; set; }
        public string MethodologyFile { get; set; }
        public string Output { get; set; }
        public int Isactive { get; set; } = 1;
        public string ActiveStatus { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string ReviewerNotes { get; set; }
        public int? ReviewerMark { get; set; }
        public string ReportFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string DeptName { get; set; }
        [NotMapped]
        public string InstName { get; set; }
        [NotMapped] public string FAName { get; set; }
        [NotMapped]
        public string SciName { get; set; }
        [NotMapped]
        public string SciMeetName { get; set; }
        [NotMapped]
        public string ProjectTypeName { get; set; }
        [NotMapped]
        public string ProjectSubTypeName { get; set; }
        [NotMapped]
        public string InvestigatorName { get; set; }

    }
}