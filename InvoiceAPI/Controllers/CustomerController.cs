using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceAPI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: customer
        public List<CustomerModel> GetAllCustomers()
        {
            return Customer.GetAllCustomers();
        }

        public CustomerModel Getcustomer(string name, string phoneNumber)
        {
            return Customer.GetCustomer(name, phoneNumber);
        }

        public CustomerModel GetCustomer(int customerId)
        {
            return Customer.GetCustomer(customerId);
        }

        public void AddCustomer(CustomerModel customer)
        {
            Customer.AddCustomer(customer);
        }

        public void UpdateCustomer(CustomerModel customer)
        {
            Customer.UpdateCustomer(customer);
        }
    }
}