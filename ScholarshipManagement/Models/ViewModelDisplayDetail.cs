using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class ViewModelDisplayDetail
    {
        public StudentPersonalDetail PersonalDetail { get; set; }
        public StudentAcademicDetail AcademicDetail { get; set; }
        public DecisionStatus DecisionStatus { get; set; }
        public StudentAchievementDetail AchievementDetail { get; set; }
    }
}