using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

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
        [Route("{id}")]
        public async Task<List<EmployeeAttendanceModel>> EmployeeAllAttendance(int employeeId)
        {
            return EmployeeAttendance.GetEmployeeAttendance(employeeId);
        }

        [HttpGet]
        [Route("duration")]
        public async Task<List<EmployeeAttendanceModel>> EmployeeAttendanceDuration(int employeeId, DateTime startTime, DateTime endTime)
        {
            var duration = await EmployeeAttendance.GetEmployeeAttendanceForDuration(employeeId, startTime, endTime);
            return duration;
        }
    }
}