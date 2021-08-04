using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class StudentAcademicDetail
    {
        [Key]
        public Guid AcademicDetailID { get; set; }
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "School Name")]
        public string SSLCSchoolName { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Board Of Education")]
        public string SSLCBoard { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Year Of Passing")]
        public Nullable<int> SSLCYearOfPassing { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Percentage Scored")]
        [Range(typeof(decimal), "0", "100")]
        public decimal SSLCPercentage { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "School Name")]
        public string HSCSchoolName { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Board Of Education")]
        public string HSCBoard { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Specialization")]
        public string HSCSpecialization { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Year Of Passing")]
        public Nullable<int> HSCYearOfPassing { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Percentage Scored")]
        [Range(typeof(decimal), "0", "100")]
        public Nullable<decimal> HSCPercentage { get; set; }
        [Display(Name = "Institution Name")]
        public string UGInstitutionName { get; set; }
        [Display(Name = "University")]
        public string UGUniversity { get; set; }
        [Display(Name = "Specialization")]
        public string UGSpecialization { get; set; }
        [Display(Name = "Mode Of Education")]
        public string UGModeOfEducation { get; set; }
        [Display(Name = "Year Of Passing")]
        public int UGYearOfPassing { get; set; }
        [Display(Name = "Percentage/CGPA")]
        public decimal UGPercentage { get; set; }

        public virtual UserStudent UserStudent { get; set; }


    }
}