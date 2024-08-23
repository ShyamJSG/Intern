using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TNAUPMS.Domains.Models.Transaction
{

    public class trnprojectdocuments : Model
    {
        public int ProjectId { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public int Isactive { get; set; }


    }
}