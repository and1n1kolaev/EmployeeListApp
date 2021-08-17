using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime LastActivityTime { get; set; }
    }
}
