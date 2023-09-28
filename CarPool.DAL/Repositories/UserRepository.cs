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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CarPoolDbContext context) : base(context) {
            
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await Db.Users.FirstOrDefaultAsync(user => user.Email == email);

        }

    }
}
