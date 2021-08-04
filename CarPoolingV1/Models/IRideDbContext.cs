using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public interface IRideDbContext
    {
        DbSet<Ride> Rides { get; set; }
        DbSet<RideManagement> RideManagements { get; set; }
        DbSet<Rider> Riders { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
