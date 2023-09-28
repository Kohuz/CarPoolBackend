using CarPool.BL.Interfaces;
using CarPool.DAL.Context;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPool.DAL.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(CarPoolDbContext context) : base(context) { }

        public async override Task<List<Car>> GetAll()
        {
            return await Db.Cars.AsNoTracking().Include(b => b.Owner)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsByOwner(int ownerId)
        {
            return await Search(b => b.OwnerId== ownerId);
        }
    }
}
