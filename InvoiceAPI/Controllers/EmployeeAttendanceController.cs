using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("attendance")]
    public class EmployeeAttendanceController : ApiController
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
        [Route("checkin")]
        public async Task CheckInEmployee(int employeeId)
        {
            await EmployeeAttendance.CheckinEmployee(employeeId);
        } 
        
        [HttpPost]
        [Route("checkout")]
        public async Task CheckOutEmployee(int employeeId)
        {
            await EmployeeAttendance.CheckoutEmployee(employeeId);
        }
    }
}