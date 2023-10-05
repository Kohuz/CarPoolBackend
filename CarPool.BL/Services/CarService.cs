using CarPool.BL.Interfaces;
using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _carRepository.GetAll();
        }

        public async Task<Car> GetById(Guid id)
        {
            return await _carRepository.GetById(id);
        }

        public async Task<Car> Add(Car car)
        {
            if (_carRepository.Search(c => c.RegistrationNumber == car.RegistrationNumber).Result.Any())
                return null;

            await _carRepository.Add(car);
            return car;
        }

        public async Task<Car> Update(Car car)
        {
            if (_carRepository.Search(c => c.RegistrationNumber == car.RegistrationNumber).Result.Any())
                return null;

            await _carRepository.Update(car);
            return car;
        }

        public async Task<bool> Remove(Car car)
        {

            await _carRepository.Remove(car);
            return true;
        }

        public async Task<IEnumerable<Car>> Search(string registrationNumber)
        {
            return await _carRepository.Search(c => c.RegistrationNumber == registrationNumber);
        }


        public void Dispose()
        {
            _carRepository?.Dispose();
        }

        public async Task<IEnumerable<Car>> GetCarsByOwner(Guid ownerId)
        {
            return await _carRepository.Search(c => c.OwnerId == ownerId);
        }
    }   
}
