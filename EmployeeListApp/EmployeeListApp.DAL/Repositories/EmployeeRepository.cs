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
    public class EmployeeRepository : IEmployeeRepository
    {
        ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> FindAsync(Guid id)
        {
            return await _context.Employees
                .Include(p => p.Department)
                .Include(p => p.WorkExperiences)
                .ThenInclude(p => p.Language)
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(p => p.Department)
                .Include(p => p.WorkExperiences)
                .ThenInclude(p => p.Language)
                    .Where(p => !p.IsDelete)
                    .ToListAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is not null)
            {
                employee.IsDelete = true;
                await UpdateAsync(employee);
            }
        }

        public async Task<List<Employee>> SearchAsync(string searchString)
        {
            return await _context.Employees
                .Include(p => p.Department)
                .Include(p => p.WorkExperiences)
                .ThenInclude(p => p.Language)
                    .Where(p => p.SecondName.ToUpper().StartsWith(searchString.ToUpper()) && !p.IsDelete).ToListAsync();
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }


    }
}
