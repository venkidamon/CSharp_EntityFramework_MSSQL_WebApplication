using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public partial class Ride
    {
        /* Fields
         * RideCode(pk,string(25)),Description,CreatedBy,
         * CreatedDate,RideDate,SeatCount,BookedCount,Status(string),
         * 
         */

        [Key]
        [Required(ErrorMessage = "Required")]
        public string RideCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required")]
        public string CreatedBy { get; set; }
        /*  [Display(Name = "Date of Birth")]*/
        /* [Required(ErrorMessage = "DOB Required", AllowEmptyStrings = false)]*/
        [Required(ErrorMessage = "Required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? RideDate { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int SeatCount { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int BookedCount { get; set; }
        public string Status { get; set; }



        public string RiderName { get; set; }
        public virtual Rider Rider { get; set; }
        public virtual ICollection<RideManagement> RideManagements { get; set; }
    }
}