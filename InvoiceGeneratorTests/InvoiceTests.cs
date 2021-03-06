﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.EmailService;
using InvoiceGenerator.Service.Service;
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
            var to = new DateTime(2020, 12, 30);
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
            var invoiceService = new InvoiceService();
            var invoicePath = invoiceService.ConstructInvoicePdf(invoice);

            var emailHelper = new EmailHelper();
            emailHelper.SendInvoiceMail(invoice, invoicePath);
        }

        [TestMethod]
        public void GetInvoiceByCustomer()
        {
            var customers = Customer.GetAllCustomers();
            var customer = customers.FirstOrDefault(); 
            var invoice = Invoice.GetInvoiceByCustomer(customer.Id).Result;
        }

        [TestMethod]
        public void GetInvoiceByEmployee()
        {
            var employees = Employee.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var invoice = Invoice.GetInvoiceByEmployee(employee.Id).Result;
        }

        [TestMethod]
        public void GeneratePdf()
        {
            var invoice = Invoice.GetAllInvoices(new DateTime(2020, 01, 01), DateTime.UtcNow).Result;
            var invoiceService = new InvoiceService();
            _ = invoiceService.ConstructInvoicePdf(invoice.LastOrDefault());
        }

        [TestMethod]
        public void SendSms()
        {
            var invoice = Invoice.GetAllInvoices(new DateTime(2020, 01, 01), DateTime.UtcNow).Result;
            var smsService = new SmsService();
            smsService.SendInvoiceSms(invoice.LastOrDefault());
        }
    }
}