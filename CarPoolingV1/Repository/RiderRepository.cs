using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPool.Repository
{
    public class RiderRepository : IRiderRepository
    {
        private RideDbContext db;

        public RiderRepository(RideDbContext context)
        {
            this.db = context;
        }

        public int CreateRider(Rider rider)
        {
            if (!RiderExists(rider))
            {
                db.Riders.Add(rider);
                db.SaveChanges();
                return 1;
            }
            return -1;
        }
        public string Validate(Rider rider)
        {

            Rider riderObject = db.Riders.Find(rider.RiderName);
            if (riderObject == null)
            {
                return null;
            }
            if (rider.Password == riderObject.Password)
            {
                return rider.RiderName;
            }
            return null;
        }

        public bool RiderExists(Rider rider)
        {
            Rider riderObject = db.Riders.Find(rider.RiderName);
            if (riderObject != null)
            {
                return true;
            }
            return false;
        }
    }
}