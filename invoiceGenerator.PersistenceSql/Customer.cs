using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Customer : Repository
    {
        #region Customers
        public static List<CustomerModel> GetAllCustomers()
        {
            try
            {
                var getAllCustomersSql = @"SELECT Id, Name, PhoneNumber, EmailId, RegisteredOn FROM [Customers]";
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
            var getCustomerSql = @"SELECT Id, Name, PhonenUmber, EmailId, RegisteredOn FROM [Customers] WHERE Id = @customerId";
            using (var connection = OpenConnection())
            {
                var customer = connection.QueryFirstOrDefault<CustomerModel>(getCustomerSql, new { @customerId = customerId });
                return customer;
            }
        }

        public static CustomerModel GetCustomer(string name, string phoneNumber)
        {
            var getCustomerSql = @"SELECT Id, Name, PhoneNumber, EmailId, RegisteredOn FROM [Customers] WHERE ";
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                getCustomerSql += string.IsNullOrEmpty(name) ? "" : " Name = @name and ";
                getCustomerSql += @" PhoneNumber = @phoneNumber";
            }
            else if (!string.IsNullOrEmpty(name))
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

        public static async Task<List<CustomerModel>> GetNewCustomers(DateTime fromDate, DateTime toDate)
        {
            var getCustomersSql = @"SELECT Id, Name, PhoneNumber, EmailId, RegisteredOn FROM [Customers] WHERE RegisteredOn Between @fromDate and @toDate";
            try
            {
                using (var connection = OpenConnection())
                {
                    var customers = connection.Query<CustomerModel>(getCustomersSql, new { @fromDate = fromDate, @toDate = toDate}).ToList();
                    return customers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<int> AddCustomer(CustomerModel customer)
        {
            try
            {
                var addCustomerSql =
                    @"INSERT INTO [Customers] (Name, PhoneNumber, EmailId, RegisteredOn)
                                        VALUES (@Name, @PhoneNumber, @EmailId, @RegisteredOn);
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
            var updateCustomerSql = @"Update [Customers] SET Name = @Name, PhoneNumber = @PhoneNumber, EmailId = @EmailId, RegisteredOn = @RegisteredOn
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

        #endregion Customers

        #region Customer Details

        public static CustomerDetailsModel GetCustomerDetails(int customerId)
        {
            try
            {
                var getCustomerDetailsSql = @"SELECT Id, SkinType, HairType, IsSensitiveSkin, MedicalHistory, Allergies, Problems, CurrentProducts
                                                FROM [CustomerDetails] WHERE CustomerId = @CustomerId";
                using (var connection = OpenConnection())
                {
                    var customerDetails = connection.QueryFirst<CustomerDetailsModel>(getCustomerDetailsSql, new { @CustomerId = customerId });
                    return customerDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddOrUpdateCustomerDetails(CustomerDetailsModel customerDetails)
        {
            try
            {
                var addCustomerDetails = @"if exists(SELECT * from [CustomerDetails] where [CustomerId]= @CustomerId)            
                    BEGIN            
                     update [CustomerDetails] set [SkinType]=@SkinType, [HairType] = @HairType,[IsSensitiveSkin] = @IsSensitiveSkin,
                     [MedicalHistory] = @MedicalHistory, [Allergies] = @Allergies, [Problems] = @Problems, [CurrentProducts] = @CurrentProducts 
                     where [CustomerId]= @CustomerId 
                    End                    
                    else            
                    BEGIN  
                    insert into [CustomerDetails] values(@CustomerId, @SkinType, @HairType, @IsSensitiveSkin, @MedicalHistory, @Allergies, @Problems, @CurrentProducts)  
                    end";
                using (var connection = OpenConnection())
                {
                    var inputObj = new
                    {
                        @CustomerId = customerDetails.CustomerId,
                        @SkinType = customerDetails.SkinType,
                        @HairType = customerDetails.HairType,
                        @IsSensitiveSkin = customerDetails.IsSensitiveSkin,
                        @MedicalHistory = customerDetails.MedicalHistory,
                        @Allergies = customerDetails.Allergies,
                        @Problems = customerDetails.Problems,
                        @CurrentProducts = customerDetails.CurrentProducts
                    };
                    connection.Query(addCustomerDetails, inputObj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
