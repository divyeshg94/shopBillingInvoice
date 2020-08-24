using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.Service;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("attendance")]
    public class EmployeeAttendanceController : ApiController
    {
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

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<List<EmployeeAttendanceModel>> EmployeeAllAttendance(int employeeId)
        {
            return EmployeeAttendance.GetEmployeeAttendance(employeeId);
        }

        [HttpGet]
        [Route("workingDays")]
        public async Task<EmployeeAttendanceQueryModel> EmployeeWorkingDays(int employeeId, DateTime startTime, DateTime endTime)
        {
            var employeeAttendanceService = new EmployeeService();
            return await employeeAttendanceService.EmployeeWorkingDays(employeeId, startTime, endTime);
        }
    }
}