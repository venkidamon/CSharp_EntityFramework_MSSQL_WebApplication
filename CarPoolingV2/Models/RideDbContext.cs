using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public partial class RideDbContext : DbContext, IRideDbContext
    {
        public RideDbContext() : base("name = RideDbContext")
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<RideManagement> RideManagements { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}