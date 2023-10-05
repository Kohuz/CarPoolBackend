using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(Guid id);
        Task<ApplicationUser> Add(ApplicationUser user);
        Task<ApplicationUser> Update(ApplicationUser user);
        Task<bool> Remove(ApplicationUser user);
        Task<ApplicationUser> Search(string email);

    }
}
