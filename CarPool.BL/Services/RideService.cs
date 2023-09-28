using CarPool.BL.Interfaces;
using CarPool.DAL.Entities;
using CarPool.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Services
{
    public class RideService : IRideService
    {
        private readonly IRideRepository _rideRepository;
        private readonly IUserRepository _userRepository;


        public RideService(IRideRepository rideRepository, IUserRepository userRepository)
        {
            _rideRepository = rideRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Ride>> GetAll()
        {
            return await _rideRepository.GetAll();
        }

        public async Task<Ride> GetById(int id)
        {
            return await _rideRepository.GetById(id);
        }

        public async Task<Ride> Add(Ride ride)
        {
            //TODO: Check if car isnt in a ride already
            await _rideRepository.Add(ride);
            return ride;
        }

        public async Task<bool> Remove(Ride ride)
        {

            await _rideRepository.Remove(ride);
            return true;
        }
        public async Task<IEnumerable<Ride>> GetRidesByDriver(int driverId)
        {
            return await _rideRepository.GetRidesByDriver(driverId);
        }
        public async Task<IEnumerable<Ride>> GetRidesByPassenger(int passengerId)
        {
            var passenger = await _userRepository.GetById(passengerId);
            var rides = await _rideRepository.GetRidesByPassenger(passenger);
            return rides;
        }

        public async Task<IEnumerable<Ride>> GetRidesByLocationsAndStartTime(string startLocation, string endLocation, DateTime startTime)
        {
            var rides = await _rideRepository.GetRidesByLocationsAndStartTime(startLocation,endLocation, startTime);
            return rides;
        }
        public void Dispose()
        {
            _rideRepository?.Dispose();
        }        
    }
}
