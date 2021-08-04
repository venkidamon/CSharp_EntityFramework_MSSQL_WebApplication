using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectAlpha.Models
{
    public class ModelDbContext : DbContext
    {
        public DbSet<UserStudent> UserStudents { get; set; }
        public DbSet<StudentPersonalDetail> StudentPersonalDetails { get; set; }
        public DbSet<StudentAcademicDetail> StudentAcademicDetails { get; set; }
        public DbSet<StudentAchievementDetail> StudentAchievementDetails { get; set; }
        public DbSet<DecisionStatus> DecisionStatuses{ get; set; }
        public object PersonalDetails { get; internal set; }
    }
}