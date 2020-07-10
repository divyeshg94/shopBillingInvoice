using System;
using System.Linq;
using System.Threading.Tasks;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Service.Service
{
    public class EmployeeService
    {
        public async Task<EmployeeAttendanceQueryModel> EmployeeWorkingDays(int employeeId, DateTime startTime, DateTime endTime)
        {
            var duration = await EmployeeAttendance.GetEmployeeAttendanceForDuration(employeeId, startTime, endTime);
            var workingFullDays = duration.Where(d => d.Duration >= 480).Count();
            var workingHalfDays = duration.Where(d => d.Duration <= 480 && d.Duration >= 240).Count();
            var totalWorkingDays = workingFullDays + (workingHalfDays / 2);
            var employeeAttendanceDUration = new EmployeeAttendanceQueryModel()
            {
                EmployeeId = employeeId,
                QueryEndTime = endTime,
                QueryStartTime = startTime,
                TotalWorkingDays = totalWorkingDays
            };
            return employeeAttendanceDUration;
        }
    }
}
