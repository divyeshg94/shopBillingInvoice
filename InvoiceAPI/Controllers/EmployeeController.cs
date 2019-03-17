using System.Collections.Generic;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace InvoiceAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        [HttpGet]
        public List<EmployeeModel> GetAllEmployees()
        {
            var employees = Employee.GetAllEmployees();
            return employees;
        }

        [HttpGet]
        public EmployeeModel GetEmployee(string name, string phoneNumber)
        {
            return Employee.GetEmployee(name, phoneNumber);
        }

        [HttpGet]
        public EmployeeModel GetEmployee(int employeeId)
        {
            return Employee.GetEmployee(employeeId);
        }

        [HttpPost]
        public void AddEmployee(EmployeeModel employee)
        {
            Employee.AddEmployee(employee);
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