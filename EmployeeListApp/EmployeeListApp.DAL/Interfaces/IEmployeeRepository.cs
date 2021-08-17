using EmployeeListApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> SearchAsync(string searchString);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> AddAsync(Employee employee);
        Task RemoveAsync(Guid id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> FindAsync(Guid id);
    }
}