using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CarPool.Repository
{
    public class RideRepository : IRideRepository
    {
        private RideDbContext db;
        public RideRepository(RideDbContext context)
        {
            this.db = context;
        }
        public int AddRide(Ride ride)
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.Rides.Add(ride);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
          
            return 1;
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public int Disable(string rideCode)
        {
            Ride ride = db.Rides.Find(rideCode);
            if(ride != null)
            {
                db.Rides.Remove(ride);
                return 1;
            }
            return -1;
           
        }
        public List<Ride> GetActiveRides()
        {
            return db.Rides.Where(x => x.Status == "New").ToList();
        }
        public List<Ride> GetActiveRidesByRider(string rider)
        {
            return db.Rides.Where(x => (x.CreatedBy == rider) && (x.Status == "New")).ToList();
        }
        public Ride GetRideByCode(string rideCode)
        {
            return db.Rides.Find(rideCode);
        }
        public int UpdateRide(Ride c)
        {
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return 1;

        }
        public int? UpdateRideManagementCount(string RideCode)
        {
            throw new NotImplementedException();
        }
    }
}