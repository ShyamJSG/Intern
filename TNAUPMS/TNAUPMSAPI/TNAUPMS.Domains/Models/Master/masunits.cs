using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNAUPMS.Domains.Models.Master
{
    public class masunits : Model
    {
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(100)]
        public string UnitName { get; set; } = null;

        [EmailAddress]
        public string AdminEmail { get; set; } = null;

        public int? InstituteId { get; set; }
        public int Isactive { get; set; } = 1;

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string UpdatedBy { get; set; } = null;
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string InstName { get; set; } = null;
    }
}
