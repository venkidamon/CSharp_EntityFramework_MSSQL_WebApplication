using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectAlpha.Models
{
    public class UserStudent

    {
        [Key]
        public Guid UserID { get; set; }                    //guid is Global Unique Identifier
        public string UserName { get; set; }

        [Required(ErrorMessage = "Provide EmailID")]
        [EmailAddress(ErrorMessage = "Provide valid EmailID")] 
        public string Email { get; set; }
        [Required(ErrorMessage = "Provide Password")]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1,
          MinNonAlphanumericCharactersError = "Your password must contain atleast one special character",
          MinRequiredPasswordLength = 8,
          MinPasswordLengthError = "Your Password must contain minimum of length 8")]
        public string Password { get; set; }



        public virtual ICollection<StudentPersonalDetail> StudentPersonalDetails { get; set; }
        public virtual ICollection<StudentAcademicDetail> StudentAcademicDetails{ get; set; }
        public virtual ICollection<StudentAchievementDetail> StudentAchievementDetails{ get; set; }
        public virtual ICollection<DecisionStatus> DecisionStatuses { get; set; }

    }
}