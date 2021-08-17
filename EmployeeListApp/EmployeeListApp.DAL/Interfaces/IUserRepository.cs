using EmployeeListApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> UpdateAsync(User user);
        Task<User> AddAsync(User user);
        Task RemoveAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByLoginAsync(string login);
    }
}