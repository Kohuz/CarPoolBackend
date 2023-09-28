using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Interfaces
{
    public interface ICarRepository: IRepository<Car>
    {
        Task<IEnumerable<Car>> GetCarsByOwner(int ownerId); 
        
    }
}
