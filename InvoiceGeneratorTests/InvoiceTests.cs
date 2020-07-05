using System;
using System.Collections.Generic;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.EmailService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class InvoiceTests
    {
        [TestMethod]
        public void GetInvoices()
        {
            var from = new DateTime(2018, 03, 01);
            var to = new DateTime(2019, 03, 30);
            var invoices = Invoice.GetAllInvoices(from, to);
        }

        [TestMethod]
        public void AddInvoice()
        {
            var invoice = new InvoiceModel()
            {
                CustomerId = 1, 
                EmployeeId = 1, 
                SaleDate = DateTime.UtcNow, 
            };
            var invoiceItems = new List<InvoiceItems>();
            var item = new InvoiceItems()
            {
                ItemId = 1,
                UnitPrice = 10,
                Quantity = 2
            };
            invoiceItems.Add(item);
            invoice.InvoiceItemses = invoiceItems;
            var totalCost = 0d;
            foreach (var i in invoiceItems)
            {
                totalCost += i.TotalPrice;
            }

            invoice.TotalAmount = totalCost.ToString();
            Invoice.AddInvoice(invoice);

            var  emailHelper = new EmailHelper();
            emailHelper.SendInvoiceMail(invoice);
        }
    }
}