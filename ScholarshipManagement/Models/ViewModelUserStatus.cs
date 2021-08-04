using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class ViewModelUserStatus
    {
        public string ApprovalStatus { get; set; }
        public string FullName { get; set; }
        public Guid UserID { get; set; }
        public string EmailID { get; set; }
    }
}