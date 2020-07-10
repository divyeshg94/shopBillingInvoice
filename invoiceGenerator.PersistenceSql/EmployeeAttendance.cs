using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class EmployeeAttendance: Repository
    {
        public static List<EmployeeAttendanceModel> GetEmployeeAttendance(int employeeId)
        {
            try
            {
                var getEmployeeAttendanceSql = @"SELECT Id, EmployeeId, CheckIn, Checkout, Duration, IsPresent FROM [EmployeeAttendance] WHERE EmployeeId = @EmployeeId";
                using (var connection = OpenConnection())
                {
                    var employeeAttendance = connection.Query<EmployeeAttendanceModel>(getEmployeeAttendanceSql, new { @EmployeeId = employeeId}).ToList();
                    return employeeAttendance;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<int> CheckinEmployee(int employeeId)
        {
            try
            {
                var addEmployeeAttendanceSql =
                    @"INSERT INTO [EmployeeAttendance] (EmployeeId, CheckIn, IsPresent)
                                        VALUES (@EmployeeId, @CheckIn, @IsPresent);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                                    
                using (var connection = OpenConnection())
                {
                    var id = connection.Query<int>(addEmployeeAttendanceSql, new { @EmployeeId = employeeId, @CheckIn = DateTime.UtcNow, @IsPresent = 1 }).Single();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task CheckoutEmployee(int employeeId)
        {
            var updateEmployeeSql = @"Update EmployeeAttendance SET Checkout = @Checkout, Duration = @Duration
                                        WHERE Id = @Id";

            var getTodayCheckIn = @"SELECT Id, CheckIn FROM EmployeeAttendance WHERE EmployeeId = @EmployeeId and Checkout IS NULL";
            try
            {
                using (var connection = OpenConnection())
                {
                    var checkIn = await connection.QueryFirstOrDefaultAsync<EmployeeAttendanceModel>(getTodayCheckIn, new { @EmployeeId = employeeId});
                    TimeSpan duration = DateTime.UtcNow.Subtract(checkIn.CheckIn);
                    connection.Execute(updateEmployeeSql, new { @Id = checkIn.Id, @Checkout = DateTime.UtcNow, @Duration  = duration });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<EmployeeAttendanceModel>> GetEmployeeAttendanceForDuration(int employeeId, DateTime startTime, DateTime endTime)
        {
            var attendanceSql = @"SELECT Id, EmployeeId, CheckIn, Checkout, Duration, IsPresent FROM [EmployeeAttendance] WHERE EmployeeId = @EmployeeId and IsPresent = 1";
            try
            {
                using (var connection = OpenConnection())
                {
                    var attendance = connection.Query<EmployeeAttendanceModel>(attendanceSql, new { @EmployeeId = employeeId }).ToList();
                    return attendance;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
