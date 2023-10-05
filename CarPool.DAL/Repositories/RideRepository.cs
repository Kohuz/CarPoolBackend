using CarPool.BL.Interfaces;
using CarPool.DAL.Context;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Repositories
{
    public class RideRepository : Repository<Ride>, IRideRepository
    {
        public RideRepository(CarPoolDbContext context) : base(context) { }

        public async Task<IEnumerable<Ride>> GetRidesByPassenger(ApplicationUser passenger)
        {
            return await Db.Rides
                .Include(r => r.Driver) 
                .Include(r => r.UsedCar) 
                .Where(r => r.Passengers.Contains(passenger))
                .ToListAsync();
        }
        public async Task<IEnumerable<Ride>> GetRidesByDriver(Guid driverId)
        {
            return await Db.Rides
               .Include(r => r.Driver) 
               .Include(r => r.UsedCar) 
               .Where(r => r.DriverId == driverId)
               .ToListAsync();
        }
        public async Task<IEnumerable<Ride>> GetRidesByLocationsAndStartTime(string startLocation, string endLocation, DateTime startTime)
        {
            return await Search(r =>
                r.StartLocation == startLocation &&
                r.EndLocation == endLocation &&
                r.StartTime >= startTime);
        }
    }
}
