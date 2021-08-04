using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class ViewModelAdminView
    {
        [Key]
        public System.Guid UserID { get; set; }
        public System.Guid PersonalDetailID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string DependentName { get; set; }
        public string DependentOccupation { get; set; }
        public int AnnualIncome { get; set; }
        public System.Guid AcademicDetailID { get; set; }
        public string SSLCSchoolName { get; set; }
        public string SSLCBoard { get; set; }
        public int SSLCYearOfPassing { get; set; }
        public decimal SSLCPercentage { get; set; }
        public string HSCSchoolName { get; set; }
        public string HSCBoard { get; set; }
        public string HSCSpecialization { get; set; }
        public int HSCYearOfPassing { get; set; }
        public decimal HSCPercentage { get; set; }
        public string UGInstitutionName { get; set; }
        public string UGUniversity { get; set; }
        public string UGSpecialization { get; set; }
        public string UGModeOfEducation { get; set; }
        public Nullable<int> UGYearOfPassing { get; set; }
        public Nullable<decimal> UGPercentage { get; set; }
        public Guid AchievementDetailID { get; set; }
       
        public string AcademicExcellence { get; set; }
        public string Achievements { get; set; }
        public string Hobbies { get; set; }
        public string Skills { get; set; }
        public System.Guid DecisionStatusID { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }
    }
}