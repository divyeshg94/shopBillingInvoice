using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.Service;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("invoice")]
    public class invoiceController : ApiController
    {
        // GET: customer
        [HttpGet]
        [Route("")]
        public List<InvoiceModel> GetAllInvoices(DateTime from, DateTime to)
        {
            return Invoice.GetAllInvoices(from, to);
        }
        
        [HttpGet]
        [Route("id")]
        public async Task<InvoiceModel> GetInvoiceById(int id)
        {
            return await Invoice.GetInvoice(id);
        }

        [HttpGet]
        [Route("customer")]
        public async Task<List<InvoiceModel>> GetInvoiceByCustomer(int customerId)
        {
            return await Invoice.GetInvoiceByCustomer(customerId);
        }

        [HttpGet]
        [Route("employee")]
        public async Task<List<InvoiceModel>> GetInvoiceByEmployee(int employeeId)
        {
            return await Invoice.GetInvoiceByEmployee(employeeId);
        }

        [HttpGet]
        [Route("SendMail")]
        public async Task SendEmailForInvoice(int invoiceId)
        {
            var invoiceService = new InvoiceService();
            await invoiceService.SendEmailForInvoice(invoiceId);
        }

        [HttpPost]
        public async Task AddInvoice(InvoiceModel invoice)
        {
            var invoiceService = new InvoiceService();
            await invoiceService.AddInvoice(invoice);
        }
    }
}