using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Repository
{
    public interface IRiderRepository
    {
        int CreateRider(Rider Rider);
        string Validate(Rider Rider);
        bool RiderExists(Rider Rider);
        void Save();
    }
}
