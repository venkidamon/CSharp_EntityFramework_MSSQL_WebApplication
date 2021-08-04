using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public class RideManagement
    {
        [Key]
        public Guid RideManagementID { get; set; }


        public string BookedBy { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? BookedDate { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        public string RideCode { get; set; }
        public virtual Ride Ride { get; set; }
        public string Email { get; set; }
        public virtual User User { get; set; }
    }
}