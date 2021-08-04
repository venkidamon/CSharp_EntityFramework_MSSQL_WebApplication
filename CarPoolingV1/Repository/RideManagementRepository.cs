using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CarPool.Repository
{
    public class RideManagementRepository
    {
        private RideDbContext db = new RideDbContext();


        public List<RideManagement> GetManagementByRider(string id)
        {
            return db.RideManagements.Where(x => (x.RideCode == id) && (x.Ride.Status == "Booked")).ToList();
        }

        public List<RideManagement> GetBookedRides(string userName)
        {
            return db.RideManagements.Where(x => (x.Email == userName) && (x.Ride.Status == "Booked")).ToList();
        }

        public bool MarkBooked(string rideCode, string userName)
        {
            var data = (from x in db.RideManagements
                       where ((x.RideCode == rideCode) && (x.Email == userName))
                       select x).First();
            data.Ride.Status = "Booked";
            data.Ride.BookedCount += 1;
            db.Entry(data).CurrentValues.SetValues(userName);
            db.SaveChanges();
            return true;
          /*  foreach (var item in data)
            {
                item.Ride.Status = "Booked";
                item.Ride.BookedCount += 1;
                if (item.Ride.BookedCount > item.Ride.SeatCount)
                {
                    return false;
                }

                db.SaveChanges();
                return true;

            }
            return false;
*/
        }
        public bool MarkCompleteRide(string rideCode, string userName)
        {
            var data = (from x in db.RideManagements
                        where ((x.RideCode == rideCode) && (x.Email == userName))
                        select x).First();
            data.Ride.Status = "Completed";
            var dte = DateTime.Now;
            data.CompletedDate = dte.Date;
            db.Entry(data).CurrentValues.SetValues(userName);
            db.SaveChanges();
            return true;
        }

        public int AddRideManagement(RideManagement cu)
        {
            
                db.RideManagements.Add(cu);
                db.SaveChanges();
         
           
                return 1;
          
        }
        public void Dispose()
        {
            db.Dispose();
        }

    }
}
