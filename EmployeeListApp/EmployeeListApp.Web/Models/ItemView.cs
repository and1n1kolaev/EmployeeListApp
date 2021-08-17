using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.WEB.Models
{
    public class ItemView
    {
        public Guid Id { get; }
        public string Label { get;  }
        public ItemView(Guid id, string label)
        {
            Id = id;
            Label = label;
        }
    }
}
