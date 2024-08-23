using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Master
{

    public class masinvestigator : Model
    {
        public string InvestigatorName { get; set; }
        public string Qualification { get; set; }
        public string Designation { get; set; }
        public int DepartmentId { get; set; }
        public int InstituteId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string DeptName { get; set; }
        [NotMapped]
        public string InstName { get; set; }

    }
}