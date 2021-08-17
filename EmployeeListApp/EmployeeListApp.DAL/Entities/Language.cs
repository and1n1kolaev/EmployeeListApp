using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Entities
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Experience> WorkExperience { get; set; }
    }
}
