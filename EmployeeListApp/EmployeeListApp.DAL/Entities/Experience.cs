using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Entities
{
    public class Experience
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
