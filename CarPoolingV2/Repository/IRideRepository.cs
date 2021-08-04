using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Repository
{
    public interface IRideRepository
    {
        int AddRide(Ride Ride);
        int Disable(string RideCode);
        List<Ride> GetActiveRides();
        List<Ride> GetActiveRidesByRider(string Rider);
        Ride GetRideByCode(string RideCode);
        int UpdateRide(Ride c);
        int? UpdateRideManagementCount(string RideCode);
        void Save();
    }
}
