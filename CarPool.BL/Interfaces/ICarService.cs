using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Interfaces
{
    public interface ICarService: IDisposable
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetById(Guid id);
        Task<Car> Add(Car car);
        Task<Car> Update(Car car);
        Task<bool> Remove(Car car);

        Task<IEnumerable<Car>> GetCarsByOwner(Guid ownerId);

        Task<IEnumerable<Car>> Search(string RegistrationNumber);

    }
}
