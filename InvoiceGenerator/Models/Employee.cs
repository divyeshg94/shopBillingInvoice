using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime JoinedOn { get; set; }

        public DateTime ReleavedOn { get; set; }

        public bool IsExists { get; set; }
    }
}
