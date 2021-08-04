using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public class Ride
    {
        [Key]
        public string RideCode { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedDate { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime RideDate { get; set; }
        public int SeatCount { get; set; }
        public int BookedCount { get; set; }
        public string Status { get; set; }
        public virtual Rider Rider { get; set; }
        public virtual ICollection<RideManagement> RideManagements { get; set; }

    }
}