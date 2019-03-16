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
        public async void AddEmployee()
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
            await Employee.AddEmployee(employee);
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
    }
}
