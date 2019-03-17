using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Customer: Repository
    {
        public static List<CustomerModel> GetAllCustomers()
        {
            try
            {
                var getAllCustomersSql = @"SELECT Id, Name, PhoneNumber, RegisteredOn FROM [Customers]";
                using (var connection = OpenConnection())
                {
                    var allCustomers = connection.Query<CustomerModel>(getAllCustomersSql).ToList();
                    return allCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomerModel GetCustomer(int customerId)
        {
            var getCustomerSql = @"SELECT Id, Name, PhonenUmber, RegisteredOn FROM [Customers] WHERE Id = @customerId";
            using (var connection = OpenConnection())
            {
                var customer = connection.QueryFirstOrDefault<CustomerModel>(getCustomerSql, new {@customerId = customerId});
                return customer;
            }
        }

        public static CustomerModel GetCustomer(string name, string phoneNumber)
        {
            var getCustomerSql = @"SELECT Id, Name, PhoneNumber, RegisteredOn FROM [Customers] WHERE ";
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                getCustomerSql += string.IsNullOrEmpty(name) ? "" : " Name = @name and ";
                getCustomerSql += @" PhoneNumber = @phoneNumber";
            }
            else if(!string.IsNullOrEmpty(name))
            {
                getCustomerSql += @" Name = @name";
            }

            using (var connection = OpenConnection())
            {
                var customer = connection.QueryFirstOrDefault<CustomerModel>(getCustomerSql, new
                {
                    @name = name,
                    @phoneNumber = phoneNumber
                });
                return customer;
            }

        }

        public static async Task<int> AddCustomer(CustomerModel customer)
        {
            try
            {
                var addCustomerSql =
                    @"INSERT INTO [Customers] (Name, PhoneNumber, RegisteredOn)
                                        VALUES (@Name, @PhoneNumber, @RegisteredOn);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                                    
                using (var connection = OpenConnection())
                {
                    var alreadyExists = GetCustomer(customer.Name, customer.PhoneNumber);
                    if (alreadyExists != null)
                    {
                        return alreadyExists.Id;
                    }
                    var id = connection.Query<int>(addCustomerSql, customer).Single();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateCustomer(CustomerModel customer)
        {
            var updateCustomerSql = @"Update [Customers] SET Name = @Name, PhoneNumber = @PhoneNumber, RegisteredOn = @RegisteredOn
                                        WHERE Id = @Id";
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(updateCustomerSql, customer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
