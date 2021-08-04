using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class ViewModelAdminDashboard
    {
        public string FullName { get; set; }
        public string ApprovalStatus { get; set; }
        public decimal SSLCPercentage { get; set; }
        public int AnnualIncome { get; set; }
        public Guid UserID { get; set; }
    }
}