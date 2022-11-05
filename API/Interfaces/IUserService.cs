using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUnpAsync(string unp);
        Task<bool> AddUserAsync(AppUser user);
        Task<List<AppUser>> GetAllUsers();
    }
}