using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Invoice: Repository
    {
        //public static List<EmployeeModel> GetAllInvoices(DateTime from, DateTime to)
        //{
        //    try
        //    {
        //        var getAllEmployeesSql = @"SELECT Id, EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists FROM [Employees]";
        //        using (var connection = OpenConnection())
        //        {
        //            var allEmployee = connection.Query<EmployeeModel>(getAllEmployeesSql).ToList();
        //            return allEmployee;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static EmployeeModel GetEmployee(string name, string phoneNumber)
        //{
        //    var getEmployeeSql = @"SELECT EmployeeId FROM [EMPLOYEES] WHERE Name = @name";
        //    if (!string.IsNullOrEmpty(phoneNumber))
        //    {
        //        getEmployeeSql += @" and PhoneNumber = @phoneNumber";
        //    }

        //    using (var connection = OpenConnection())
        //    {
        //        var employee = connection.QueryFirstOrDefault<EmployeeModel>(getEmployeeSql, new
        //        {
        //            @name = name,
        //            @phoneNumber = phoneNumber
        //        });
        //        return employee;
        //    }

        //}

        //public static async Task<int> AddEmployee(EmployeeModel employee)
        //{
        //    try
        //    {
        //        var addEmployeeSql =
        //            @"INSERT INTO [EMPLOYEES] (EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists)
        //                                VALUES (@EmployeeId, @Name, @PhoneNumber, @JoinedOn, @ReleavedOn, @IsExists)";
                                    
        //        using (var connection = OpenConnection())
        //        {
        //            var alreadyExists = GetEmployee(employee.Name, employee.PhoneNumber);
        //            if (alreadyExists != null)
        //            {
        //                return alreadyExists.Id;
        //            }
        //            var id = connection.Execute(addEmployeeSql, employee);
        //            return id;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void UpdateEmployee(EmployeeModel employee)
        //{
        //    var updateEmployeeSql = @"Update EMPLOYEES SET EmployeeId = @EmployeeId, Name = @Name, PhoneNumber = @PhoneNumber, JoinedOn  = @JoinedOn, ReleavedOn = @ReleavedOn, IsExists = @IsExists
        //                                WHERE Id = @Id";
        //    try
        //    {
        //        using (var connection = OpenConnection())
        //        {
        //            connection.Execute(updateEmployeeSql, employee);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static void DeleteEmployee(int employeeId)
        //{
        //    var updateEmployeeSql = @"Update EMPLOYEES SET IsExists = 'false', ReleavedOn = GETUTCDATE() 
        //                                WHERE Id = @Id";
        //    try
        //    {
        //        using (var connection = OpenConnection())
        //        {
        //            connection.Execute(updateEmployeeSql, new { @Id = employeeId});
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
