using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarPool.Repository
{
    public class RideManagementRepository
    {
        private RideDbContext db = new RideDbContext();
        public RideManagementRepository()
        {

        }
        public RideManagementRepository(RideDbContext context)
        {
            this.db = context;
        }
        public List<RideManagement> GetManagementByRider(string RiderName)
        {
            throw new NotImplementedException();
        }

        public List<RideManagement> GetBookedRides(string UserName)
        {
            throw new NotImplementedException();
        }

        public bool MarkBooked(string RideCode, string UserName)
        {
            throw new NotImplementedException();
        }
        public bool MarkCompleteRide(string RideCode, string UserName)
        {
            throw new NotImplementedException();
        }

        public int AddRideManagement(RideManagement cu)
        {
            throw new NotImplementedException();
        }

    }
}