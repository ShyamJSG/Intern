using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnprojecttaskextensioninfo : Model
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExtensionDate { get; set; }
        public string Reason { get; set; }
        public string Approved { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}