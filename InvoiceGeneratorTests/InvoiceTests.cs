using System;
using System.Collections.Generic;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
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
                Cost = 10,
            };
            invoiceItems.Add(item);
            invoice.InvoiceItemses = invoiceItems;
            var totalCost = 0;
            foreach (var i in invoiceItems)
            {
                totalCost += i.Cost;
            }

            invoice.TotalAmount = totalCost.ToString();
            Invoice.AddInvoice(invoice);
        }

        //[TestMethod]
        //public void UpdateCustomer()
        //{
        //    var customer = new CustomerModel()
        //    {
        //        Id = 1,
        //        Name = "Divyesh",
        //        PhoneNumber = "95665821288",
        //    };
        //    Customer.UpdateCustomer(customer);
        //}
    }
}