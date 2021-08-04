using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class DecisionStatus
    {
        public System.Guid DecisionStatusID { get; set; }
        public System.Guid UserID { get; set; }
        public string ApprovalStatus { get; set; }
        public string Comments { get; set; }

        public virtual UserStudent UserStudent { get; set; }
    }
}