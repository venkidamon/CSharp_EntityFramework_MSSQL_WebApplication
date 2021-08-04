using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class StudentAchievementDetail
    {
        [Key]
        public Guid AchievementDetailID { get; set; }
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Required")]

        public string AcademicExcellence { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Achievements { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Hobbies { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Skills { get; set; }
        [Required(ErrorMessage = "Select Proof")]
        [Display(Name = "Proof")]

        public string Title { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }


        public virtual UserStudent UserStudent { get; set; }

    }
}