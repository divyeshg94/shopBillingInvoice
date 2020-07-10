using System;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void GetAllCustomers()
        {
            var Customers = Customer.GetAllCustomers();
        }

        [TestMethod]
        public void AddCustomer()
        {
            var customer = new CustomerModel()
            {
                Name = "Divyesh",
                PhoneNumber = "9566582129",
                RegisteredOn = DateTime.UtcNow
            };
            Customer.AddCustomer(customer);
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            var customer = new CustomerModel()
            {
                Id = 1,
                Name = "Divyesh",
                PhoneNumber = "95665821288",
                RegisteredOn = DateTime.UtcNow
            };
            Customer.UpdateCustomer(customer);
        }

        [TestMethod]
        public void GetCustomerDetails()
        {
            var customer = Customer.GetAllCustomers().FirstOrDefault();
            var Customers = Customer.GetCustomerDetails(customer.Id);
        }

        [TestMethod]
        public void AddOrUpdateCustomerDetails()
        {
            var customer = Customer.GetAllCustomers().FirstOrDefault();
            var customerDetails = new CustomerDetailsModel()
            {
                IsSensitiveSkin = false,
                HairType = "Dry",
                SkinType = "Oily",
                MedicalHistory = "Cosmetic Surgery",
                Problems = "Frizzy Hair",
                CurrentProducts = "Dove shampoo and conditioner",
                Allergies = "Synus",
                CustomerId = customer.Id
            };
            Customer.AddOrUpdateCustomerDetails(customerDetails);
        }

    }
}