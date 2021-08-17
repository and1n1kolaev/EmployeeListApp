using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Experience> WorkExperiences { get; set; }
        public bool IsDelete { get; set; }
    }
}

