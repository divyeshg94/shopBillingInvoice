using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : ApiController
    {
        // GET: Employee
        [HttpGet]
        [Route("")]
        public List<EmployeeModel> GetAllEmployees()
        {
            var employees = Employee.GetAllEmployees();
            return employees;
        }

        [HttpGet]
        [Route("name")]
        public EmployeeModel GetEmployee(string name = "", string phoneNumber = "")
        {
            return Employee.GetEmployee(name, phoneNumber);
        }

        [HttpGet]
        [Route("id")]
        public EmployeeModel GetEmployee(int employeeId)
        {
            return Employee.GetEmployee(employeeId);
        }

        [HttpPost]
        public async Task AddEmployee(EmployeeModel employee)
        {
            await Employee.AddEmployee(employee);
        }

        [HttpPut]
        public void UpdateEmployee(EmployeeModel employee)
        {
            Employee.UpdateEmployee(employee);
        }

        [HttpDelete]
        public void DeleteEmployee(int employeeId)
        {
            Employee.DeleteEmployee(employeeId);
        }
    }
}