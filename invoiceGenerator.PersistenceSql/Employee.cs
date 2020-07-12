using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Employee: Repository
    {
        public static List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                var getAllEmployeesSql = @"SELECT Id, EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists, AadharNo, Photo, AadharImage, Address, EmailId FROM [Employees]";
                using (var connection = OpenConnection())
                {
                    var allEmployee = connection.Query<EmployeeModel>(getAllEmployeesSql).ToList();
                    return allEmployee;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmployeeModel GetEmployee(int employeeId)
        {
            var getEmployeeSql = @"SELECT Id, EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists, AadharNo, Photo, AadharImage, Address, EmailId FROM [EMPLOYEES] WHERE Id = @Id";
            using (var connection = OpenConnection())
            {
                var employee = connection.QueryFirstOrDefault<EmployeeModel>(getEmployeeSql, new {@Id = employeeId });
                return employee;
            }
        }

        public static EmployeeModel GetEmployee(string name, string phoneNumber)
        {
            var getEmployeeSql = @"SELECT Id, EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists, AadharNo, Photo, AadharImage, Address, EmailId FROM [EMPLOYEES] WHERE ";

            if (!string.IsNullOrEmpty(phoneNumber) || !string.IsNullOrEmpty(name))
            {
                getEmployeeSql += string.IsNullOrEmpty(name) ? "" : " Name like @name ";
                getEmployeeSql += !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phoneNumber) ? " and " : "";
                getEmployeeSql += @" PhoneNumber = @phoneNumber";
            }

            using (var connection = OpenConnection())
            {
                var employee = connection.QueryFirstOrDefault<EmployeeModel>(getEmployeeSql, new
                {
                    @name = name,
                    @phoneNumber = phoneNumber
                });
                return employee;
            }
        }

        public static async Task<int> AddEmployee(EmployeeModel employee)
        {
            try
            {
                var addEmployeeSql =
                    @"INSERT INTO [EMPLOYEES] (EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists, AadharNo, Photo, AadharImage, Address, EmailId)
                                        VALUES (@EmployeeId, @Name, @PhoneNumber, @JoinedOn, @ReleavedOn, @IsExists, @AadharNo, @Photo, @AadharImage, @Address, @EmailId);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                                    
                using (var connection = OpenConnection())
                {
                    var alreadyExists = GetEmployee(employee.Name, employee.PhoneNumber);
                    if (alreadyExists != null)
                    {
                        return alreadyExists.Id;
                    }
                    var id = connection.Query<int>(addEmployeeSql, employee).Single();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateEmployee(EmployeeModel employee)
        {
            var updateEmployeeSql = @"Update EMPLOYEES SET EmployeeId = @EmployeeId, Name = @Name, PhoneNumber = @PhoneNumber, 
                                        JoinedOn  = @JoinedOn, ReleavedOn = @ReleavedOn, IsExists = @IsExists, AadharNo = @AadharNo,
                                        Photo = @Photo, AadharImage = @AadharImage, Address = @Address, EmailId = @EmailId
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateEmployeeSql, employee);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteEmployee(int employeeId)
        {
            var updateEmployeeSql = @"Update EMPLOYEES SET IsExists = 'false', ReleavedOn = GETUTCDATE() 
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateEmployeeSql, new { @Id = employeeId});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
