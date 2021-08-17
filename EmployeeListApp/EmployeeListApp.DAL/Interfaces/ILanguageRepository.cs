using EmployeeListApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Interfaces
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetAllAsync();
    }
}