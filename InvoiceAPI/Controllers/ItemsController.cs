using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceAPI.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: customer
        [HttpGet]
        public List<CustomerModel> GetAll()
        {
            return Customer.GetAllCustomers();
        }

        [HttpGet]
        public CustomerModel Get(string name, string phoneNumber)
        {
            return Customer.GetCustomer(name, phoneNumber);
        }

        [HttpGet]
        public CustomerModel GetCustomer(int customerId)
        {
            return Customer.GetCustomer(customerId);
        }

        [HttpPost]
        public async Task Add(CustomerModel customer)
        {
            await Customer.AddCustomer(customer);
        }

        [HttpPut]
        public void Update(CustomerModel customer)
        {
            Customer.UpdateCustomer(customer);
        }
    }
}