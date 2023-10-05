using CarPool.BL.Interfaces;
using CarPool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<ApplicationUser> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<ApplicationUser> Add(ApplicationUser user)
        {
            if (_userRepository.Search(u => u.Email == user.Email).Result.Any())
                return null;

            await _userRepository.Add(user);
            return user;
        }

        public async Task<ApplicationUser?> Update(ApplicationUser user)
        {
            //TODO
            if (_userRepository.Search(u => u.Email == user.Email).Result.Any())
                return null;

            await _userRepository.Update(user);
            return user;
        }

        public async Task<bool> Remove(ApplicationUser user)
        {
            await _userRepository.Remove(user);
            return true;
        }

        public async Task<ApplicationUser?> Search(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
