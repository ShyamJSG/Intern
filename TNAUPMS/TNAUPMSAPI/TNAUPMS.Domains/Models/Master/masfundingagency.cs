using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Master
{

    public class masfundingagency : Model
    {
        public string Code { get; set; }
        public string FundingAgencyName { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FundingAgencyName) ;
        }
    }
}