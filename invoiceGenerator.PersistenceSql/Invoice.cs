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

        public static async Task<InvoiceModel> GetInvoice(int invoiceId)
        {
            try
            {
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, 
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.Id = @Id";
                using (var connection = OpenConnection())
                {
                    var invoice = connection.Query<InvoiceModel>(getInvoiceSql, new { @Id = invoiceId}).Single();
                    return invoice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<InvoiceModel>> GetInvoiceByCustomer(int customerId)
        {
            try
            {
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, 
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.CustomerId = @Id";
                using (var connection = OpenConnection())
                {
                    var invoices = connection.Query<List<InvoiceModel>>(getInvoiceSql, new { @Id = customerId }).Single();
                    return invoices;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<InvoiceModel>> GetInvoiceByEmployee(int employeeId)
        {
            try
            {
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, 
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.EmployeeId = @Id";
                using (var connection = OpenConnection())
                {
                    var invoices = connection.Query<List<InvoiceModel>>(getInvoiceSql, new { @Id = employeeId }).Single();
                    return invoices;
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
                var addInvoiceItemsSql = @"INSERT INTO [InvoiceItems] (InvoiceId, ItemId, UnitPrice, Quantity, TotalPrice)
                                        VALUES (@InvoiceId, @ItemId, @UnitPrice, @Quantity, @TotalPrice)";

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
