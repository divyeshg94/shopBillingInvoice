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
        public static List<InvoiceModel> GetAllInvoices(DateTime from, DateTime to)
        {
            try
            {
                var getInvoicesSql = @"SELECT * FROM [Invoice] i WHERE i.[SaleDate] >= @from AND i.[SaleDate] <= @to";
                var getInvoiceItemsSql = @"SELECT * FROM [InvoiceItems] WHERE InvoiceId = @InvoiceId";

                using (var connection = OpenConnection())
                {
                    var allInvoice = connection.Query<InvoiceModel>(getInvoicesSql, new { @from = from, @to = to}).ToList();
                    foreach (var invoice in allInvoice)
                    {
                        var invoiceItems =
                            connection.Query<InvoiceItems>(getInvoiceItemsSql, new {@InvoiceId = invoice.Id}).ToList();
                        invoice.InvoiceItemses = invoiceItems;

                        var customer = Customer.GetCustomer(invoice.CustomerId);
                        invoice.Customer = customer;

                        var employee = Employee.GetEmployee(invoice.EmployeeId);
                        invoice.Employee = employee;

                        foreach (var item in invoiceItems)
                        {
                            var i = Item.GetItem(item.ItemId);
                            item.Item = i;
                        }
                    }
                    return allInvoice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<int> AddInvoice(InvoiceModel invoice)
        {
            try
            {
                var addInvoiceSql =
                    @"INSERT INTO [Invoice] (EmployeeId, CustomerId, TotalAmount, SaleDate)
                                        VALUES (@EmployeeId, @CustomerId, @TotalAmount, @SaleDate);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                var addInvoiceItemsSql = @"INSERT INTO [InvoiceItems] (InvoiceId, ItemId, Cost, Quantity)
                                        VALUES (@InvoiceId, @ItemId, @Cost, @Quantity)";

                using (var connection = OpenConnection())
                {
                    var invoiceId = connection.Query<int>(addInvoiceSql, invoice).Single();
                    foreach (var invoiceItem in invoice.InvoiceItemses)
                    {
                        invoiceItem.InvoiceId = invoiceId;
                        connection.Execute(addInvoiceItemsSql, invoiceItem);
                    }
                    return invoiceId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
