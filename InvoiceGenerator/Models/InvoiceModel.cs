using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }

        public string TotalAmount { get; set; }

        public DateTime SaleDate { get; set; }

        public ICollection<InvoiceItems> InvoiceItemses { get; set; }
    }
}
