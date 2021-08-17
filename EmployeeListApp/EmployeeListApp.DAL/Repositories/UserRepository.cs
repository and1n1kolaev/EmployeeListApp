using EmployeeListApp.DAL.Data;
using EmployeeListApp.DAL.Entities;
using EmployeeListApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Repositories
{
    class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            //user.Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id;
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            var local = _context.Users.Local.FirstOrDefault(entity => entity.Id == user.Id);
            if (local is not null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Login == login);
        }
    }
}
