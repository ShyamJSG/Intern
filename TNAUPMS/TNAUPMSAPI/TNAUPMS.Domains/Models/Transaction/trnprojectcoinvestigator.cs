using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnprojectcoinvestigator : Model
    {
        public string UnitName { get; set; }
        public string AdminEmail { get; set; }
        public int IsntituteId { get; set; }
        public int Isactive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}