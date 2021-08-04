using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class StudentPersonalDetail
    {
        [Key]
        public Guid PersonalDetailID { get; set; }
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FullName { get; set; }
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "DOB Required", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [Range(15, 30)]
        [Required(ErrorMessage = "Age should be between 15 and 30")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Required")]

        public string State { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Nationality { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(10, ErrorMessage = "Phone Number must contain exactly 10 numbers")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Required")]

        public string DependentName { get; set; }
        [Required(ErrorMessage = "Required")]

        public string DependentOccupation { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Annual Income")]
        [Range(10000, 100000, ErrorMessage = "Salary must be between 10000 and 100000")]
        public int AnnualIncome { get; set; }
        [DisplayName("Profile photo")]
        public string ImagePath { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }




        public virtual UserStudent UserStudent { get; set; }

    }
}