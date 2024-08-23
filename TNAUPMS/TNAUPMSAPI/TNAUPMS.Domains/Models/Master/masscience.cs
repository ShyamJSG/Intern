using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Master
{

    public class masscience : Model
    {
        public string Code { get; set; }
        public string ScienceName { get; set; }
        public int Isactive { get; set; }=1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; } = null; 
        public DateTime? UpdatedOn { get; set; }

    }
}