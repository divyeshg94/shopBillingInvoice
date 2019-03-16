using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace invoiceGenerator.PersistenceSql
{
    public class Employee: Repository
    {
        public List<Employee> GetAllEmployees()
        {
            try
            {
                var getAllEmployeesSql = @"SELECT Id, EmployeeId, Name, PhoneNumber, JoinedOn, ReleavedOn, IsExists FROM [Employees]";
                using (var connection = OpenConnection())
                {
                    var allEmployee = connection.Query<Employee>(getAllEmployeesSql).AsList();
                    return allEmployee;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
