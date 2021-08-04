using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public class Rider
    {
        [Key]
        public string RiderName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}