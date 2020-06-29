using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceAPI.Controllers
{
    public class invoiceController : ApiController
    {
        // GET: customer
        [HttpGet]
        public List<InvoiceModel> GetAllInvoices(DateTime from, DateTime to)
        {
            return Invoice.GetAllInvoices(from, to);
        }

        [HttpPost]
        public async Task AddInvoice(InvoiceModel invoice)
        {
            await Invoice.AddInvoice(invoice);
        }
    }
}