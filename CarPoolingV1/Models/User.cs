using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CarPool.Models
{
    public partial class User
    {
        /*
       * Fields- Email(key),Password
       */
        [Key]
        [Required(ErrorMessage = "Provide EmailID")]
        [EmailAddress(ErrorMessage = "Provide valid EmailID")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Provide Password")]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1,
        MinNonAlphanumericCharactersError = "Your password must contain atleast one special character",
        MinRequiredPasswordLength = 8,
        MinPasswordLengthError = "Your Password must contain minimum of length 8")]
        public string Password { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}