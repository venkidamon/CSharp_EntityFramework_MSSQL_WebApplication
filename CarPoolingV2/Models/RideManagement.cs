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
        public string RideCode { get; set; }
        public string BookedBy { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public virtual Ride Ride { get; set; }
        public virtual User User { get; set; }
    }
}