using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public class RideViewModel
    {
        public string RideCode { get; set; }
        public string Description { get; set; }
        public DateTime RideDate { get; set; }
        public int SeatCount { get; set; }
    }
}