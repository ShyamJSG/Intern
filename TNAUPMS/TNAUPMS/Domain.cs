using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TNAUPMS
{
    public class SignInDataModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class masdepartment
    {
        public int id { get; set; }
        public string code { get; set; }
        public string departmentName { get; set; }
        public string directorName { get; set; }
        public string directorEmail { get; set; }
        public string directorMobileNo { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [NotMapped]
        public string Password { get; set; }



    }
    public class masfundingagency
    {
        public int id { get; set; } = 0;
        public string code { get; set; }
        public string fundingAgencyName { get; set; }

    }


    public class masinstitute
    {
        public int id { get; set; }
        public string code { get; set; }
        public string instituteName { get; set; }
        public string shortName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string pincode { get; set; }
        public string principalName { get; set; }
        public string principalEmail { get; set; }

        public string principalMobileNo { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string Password { get; set; }

    }
    public class massciencemeet
    {
        public int id { get; set; }
        public string scienceMeetName { get; set; }
        public string code { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string Password { get; set; }

    }
    public class masscience
    {
        public int id { get; set; }
        public string code { get; set; }
        public string scienceName { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    
    }
    public class masscienceedit
    {
        public int id { get; set; } = 0;
        public string scienceName { get; set; }
        public int departmentId { get; set; }
        public List<SelectListItem> DeptList { get; set; }

    }
    public class masinvestigator
    {
        public int id { get; set; }
        public string investigatorName { get; set; }
        public string qualification { get; set; }
        public string designation { get; set; }
        public int departmentId { get; set; }
        public int instituteId { get; set; }
        public string emailId { get; set; }
        public string mobileNo { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string deptName { get; set; }
        [NotMapped]
        public string InstName { get; set; }

    }
    public class masinvestigatorEdit
    {
        public int id { get; set; }
        public string investigatorName { get; set; }
        public string qualification { get; set; }
        public string designation { get; set; }
        public int departmentId { get; set; }
        public int instituteId { get; set; }
        public string mobileNo { get; set; }
        public int Isactive { get; set; } = 1;

        [NotMapped]
        public string deptName { get; set; }
        [NotMapped]
        public string InstName { get; set; }
        public List<SelectListItem> InstList { get; set; }
        public List<SelectListItem> UnitList { get; set; }

    }
    public class masunits
    {
        public int id { get; set; }
        public string code { get; set; }
        public string unitName { get; set; }
        public string adminEmail { get; set; }
        public int instituteId { get; set; }
        public int Isactive { get; set; } = 1;
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string instName { get; set; }
    }
    public class masunitedit
    {
        public int id { get; set; } = 0;
        public string code { get; set; }
        public string unitName { get; set; }
        public int instituteId { get; set; }
        public List<SelectListItem> InstList { get; set; }

    }

    public class masprojecttype
    {
        public int id { get; set; }
        public string code { get; set; }
        public string projectType { get; set; }
        public int Isactive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }



    }
    public class masprojectsubtype
    {
        public int id { get; set; }
        public string code { get; set; }
        public int ptId { get; set; }
        public string subTypeName { get; set; }
        public int Isactive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [NotMapped]
        public string projectTypeName { get; set; }
    }
    public class masprojectsubtypeedit
    {
        public int id { get; set; } = 0;
        public int ptId { get; set; } 
        public string code { get; set; }
        public string subTypeName { get; set; }
        public List<SelectListItem> ProjectList { get; set; }

    }
    public class trnproject
    {
        public int id { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public int departmentId { get; set; }
        public int instituteId { get; set; }
        public int scienceId { get; set; }
        public int scienceMeetId { get; set; }
        public int projectTypeId { get; set; }
        public int projectSubTypeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? startDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? endDate { get; set; }
        public int fundingAgencyId { get; set; }
        public int principalInvestigator { get; set; }
        public decimal budget { get; set; }
        public string objective { get; set; }
        public string methodology { get; set; }
        public string output { get; set; }
        public int Isactive { get; set; } = 1;
        public string ActiveStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CompletedOn { get; set; }
        public string ReviewerNotes { get; set; }
        public int ReviewerMark { get; set; }
        public string ReportFile { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [NotMapped]
        public string deptName { get; set; }
        [NotMapped]
        public string instName { get; set; }
        [NotMapped]
        public string scienceName { get; set; }
        [NotMapped]
        public string scienceMeetName { get; set; }
        [NotMapped]
        public string faName { get; set; }
        [NotMapped]
        public string investigatorName { get; set; }
        [NotMapped]
        public string projectTypeName { get; set; }
        [NotMapped]
        public string projectSubTypeName { get; set; }
    }
    public class trnprojecttask
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string taskCode { get; set; }
        public string taskName { get; set; }
        public string taskInformation { get; set; }
        public int assignedTo { get; set; }
        public int reviewer { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? startDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? endDate { get; set; }
        public string activeStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? extnDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? completedOn { get; set; }
        public string reportDetails { get; set; }
        public string reportFile { get; set; }
        public string reviewerNotes { get; set; }
        public int reviewerMark { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? reviewDate { get; set; }
        public int isactive { get; set; } = 1;

        [NotMapped]
        public string projectCode { get; set; }
        [NotMapped]
        public string projectName { get; set; }
        [NotMapped]
        public string taskOwner { get; set; }

    }
    public class trnprojecttaskEdit
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string taskCode { get; set; }
        public string taskName { get; set; }
        public string taskInformation { get; set; }
        public int assignedTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? startDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? endDate { get; set; }
        public string activeStatus { get; set; }
        public List<SelectListItem> OfficerList { get; set; }

    }
    public class trnprojecttaskreport 
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int taskId { get; set; }
        public int reportedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] 
        public DateTime? reportedDate { get; set; }
        public DateTime? completedOn { get; set; }
        public string reportDetails { get; set; }
        public string reportFiles { get; set; }
        public int isactive { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }

        [NotMapped]
        public string ReportedUserName { get; set; }


    }
    public class reportingModel
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int taskId { get; set; }
        public DateTime? reportedDate { get; set; }
        public string reportDetails { get; set; }
        public string reportFiles { get; set; }
        public string taskName { get; set; }
    }

    public class extentionModel
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int taskId { get; set; }
        public DateTime? extendedDate { get; set; }
        public string reason { get; set; }
        public string taskName { get; set; }
    }

    public class trnprojecttaskextensioninfo 
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int taskId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? requestDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime? extensionDate { get; set; }
        public string reason { get; set; }
        public string approved { get; set; }
        public int isactive { get; set; } = 1;
        public string createdBy { get; set; }
        public DateTime? createdOn { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedOn { get; set; }
    }

    public class AnalyticsModel
    {
        public int keyId { get; set; }
        public string keyCode { get; set; }
        public string keyName { get; set; }
        public int projectCount { get; set; }
        public decimal projectValue { get; set; }
    }
}