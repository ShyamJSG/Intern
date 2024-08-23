using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TNAUPMS.Domains.Models.Master
{

    public class masdepartment : Model
    {
        public string Code { get; set; }
        public string DepartmentName { get; set; }
        public string DirectorName { get; set; }
        public string DirectorEmail { get; set; }
        public string DirectorMobileNo { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        
        [NotMapped]
        public string Password { get; set; }


    }
}