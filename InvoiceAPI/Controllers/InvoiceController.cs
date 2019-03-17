using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceAPI.Controllers
{
    public class invoiceController : Controller
    {
        // GET: customer
        public List<InvoiceModel> GetAllInvoices(DateTime from, DateTime to)
        {
            return Invoice.GetAllInvoices(from, to);
        }

        public void AddInvoice(InvoiceModel invoice)
        {
            Invoice.AddInvoice(invoice);
        }
    }
}