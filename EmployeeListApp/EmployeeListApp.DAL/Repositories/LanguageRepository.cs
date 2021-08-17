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
    class LanguageRepository : ILanguageRepository
    {
        private ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Language>> GetAllAsync()
        {
            return await _context.Language.ToListAsync();
        }
    }
}
