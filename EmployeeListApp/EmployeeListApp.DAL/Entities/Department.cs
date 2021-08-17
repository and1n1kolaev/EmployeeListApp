using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
