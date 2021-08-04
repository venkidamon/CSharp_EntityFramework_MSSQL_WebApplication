using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CarPool.Models
{
    public partial class Rider
    {
        /*
        * Fields - RiderName(key),Email,Password,
        */

        [Key]
        [Required(ErrorMessage = "Provide Rider Name")]
        public string RiderName { get; set; }
        [Required(ErrorMessage = "Provide EmailID")]
        [EmailAddress(ErrorMessage = "Provide valid EmailID")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Provide Password")]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1,
        MinNonAlphanumericCharactersError = "Your password must contain atleast one special character",
        MinRequiredPasswordLength = 8,
        MinPasswordLengthError = "Your Password must contain minimum of length 8")]
        public string Password { get; set; }

      

    }
}