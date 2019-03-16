using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class InvoiceItems
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }

        public int ItemId { get; set; }
        public ItemsModel Item { get; set; }

        public int Cost { get; set; }
    }
}
