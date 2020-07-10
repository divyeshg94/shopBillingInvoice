using System;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void GetAllEmployees()
        {
            var employees = Employee.GetAllEmployees();
        }

        [TestMethod]
        public void AddEmployee()
        {
            var employee = new EmployeeModel()
            {
                EmployeeId = "100",
                IsExists = true,
                Name = "Divyesh",
                PhoneNumber = "9566582129",
                JoinedOn = DateTime.UtcNow,
                ReleavedOn = null
            };
            Employee.AddEmployee(employee);
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            var employee = new EmployeeModel()
            {
                Id = 1,
                EmployeeId = "100",
                IsExists = true,
                Name = "Divyesh",
                PhoneNumber = "95665821288",
                JoinedOn = DateTime.UtcNow,
                ReleavedOn = null
            };
            Employee.UpdateEmployee(employee);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            var employee = Employee.GetAllEmployees().First();
            Employee.DeleteEmployee(employee.Id);
        }

        [TestMethod]
        public void CheckInEmployee()
        {
            var employees = Employee.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var checkIn = EmployeeAttendance.CheckinEmployee(employee.Id);
        }

        [TestMethod]
        public void CHeckOutEmployee()
        {
            var employees = Employee.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var checkout = EmployeeAttendance.CheckoutEmployee(employee.Id);
        }

        [TestMethod]
        public void GetEmployeeAttendance()
        {
            var employees = Employee.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var attendance = EmployeeAttendance.GetEmployeeAttendance(employee.Id);
        }

        [TestMethod]
        public void GetEmployeeAttendanceDuration()
        {
            var employees = Employee.GetAllEmployees();
            var employee = employees.FirstOrDefault();
            var endDate = DateTime.UtcNow;
            var startDate = new DateTime(endDate.Year, endDate.Month, 1);
            var duration = EmployeeAttendance.GetEmployeeAttendanceForDuration(employee.Id, startDate, endDate).Result;
        }
    }
}
