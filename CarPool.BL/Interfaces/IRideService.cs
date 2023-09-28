using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Interfaces
{
    public interface IRideService : IDisposable
    {
        Task<IEnumerable<Ride>> GetAll();
        Task<Ride> GetById(int id);
        Task<Ride> Add(Ride ride);
        Task<bool> Remove(Ride ride);
        Task<IEnumerable<Ride>> GetRidesByDriver(int driverId);
        Task<IEnumerable<Ride>> GetRidesByPassenger(int passengerId);
        Task<IEnumerable<Ride>> GetRidesByLocationsAndStartTime(string startLocation, string endLocation, DateTime startTime);


    }
}
