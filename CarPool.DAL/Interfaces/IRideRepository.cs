using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Interfaces
{
    public interface IRideRepository : IRepository<Ride>
    {
        Task<IEnumerable<Ride>> GetRidesByLocationsAndStartTime(string startLocation, string endLocation, DateTime startTime);
        Task<IEnumerable<Ride>> GetRidesByDriver(Guid driverId);
        Task<IEnumerable<Ride>> GetRidesByPassenger(ApplicationUser passenger);

    }
}

