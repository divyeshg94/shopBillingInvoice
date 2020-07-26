using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Invoice: Repository
    {
        private const string  getInvoiceItemsSql = @"SELECT * FROM [InvoiceItems] WHERE InvoiceId = @InvoiceId";

        public static async Task<List<InvoiceModel>> GetAllInvoices(DateTime from, DateTime to)
        {
            try
            {
                var getInvoicesSql = @"SELECT * FROM [Invoice] i WHERE i.[SaleDate] >= @from AND i.[SaleDate] <= @to";
                var invoices = new List<InvoiceModel>();

                using (var connection = OpenConnection())
                {
                    var allInvoice = connection.Query<InvoiceModel>(getInvoicesSql, new { @from = from, @to = to}).ToList();
                    foreach (var invoice in allInvoice)
                    {
                        var i = await GetInvoiceElements(connection, invoice);
                        invoices.Add(i);
                    }
                    return invoices;
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
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, i.ModeOfPaymentString, i.ModeOfPayment,
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice, iitem.ServicedBy, iitem.DiscountPercentage, iitem.DiscountAmount
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.Id = @Id";
                using (var connection = OpenConnection())
                {
                    var invoice = connection.Query<InvoiceModel>(getInvoiceSql, new { @Id = invoiceId}).Single();
                    return await GetInvoiceElements(connection, invoice);
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
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, i.ModeOfPaymentString, i.ModeOfPayment,
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice, iitem.ServicedBy, iitem.DiscountPercentage, iitem.DiscountAmount
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.CustomerId = @Id";
                using (var connection = OpenConnection())
                {
                    var invoices = connection.Query<InvoiceModel>(getInvoiceSql, new { @Id = customerId });
                    var allCustomerInvoices = invoices.Select(i => GetInvoiceElements(connection, i).Result);
                    return allCustomerInvoices.ToList();
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
                var getInvoiceSql = @"SELECT i.CustomerId, i.EmployeeId, i.Id, i.TotalAmount, i.DiscountAmount, i.DiscountPercent, i.ModeOfPaymentString, i.ModeOfPayment, 
		                                i.Notes, i.SaleDate, i.Tax, iitem.ItemId, iitem.Quantity, iitem.TotalPrice, iitem.UnitPrice, iitem.ServicedBy, iitem.DiscountPercentage, iitem.DiscountAmount
                                        FROM Invoice i INNER JOIN InvoiceItems iitem on i.Id = iitem.InvoiceId where i.EmployeeId = @Id";
                using (var connection = OpenConnection())
                {
                    var invoices = connection.Query<InvoiceModel>(getInvoiceSql, new { @Id = employeeId });
                    var allEmployeeInvoices = invoices.Select(i => GetInvoiceElements(connection, i).Result);
                    return allEmployeeInvoices.ToList();
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
                    @"INSERT INTO [Invoice] (EmployeeId, CustomerId, TotalAmount, SaleDate, DiscountPercent, DiscountAmount, Tax, Notes, ModeOfPayment, ModeOfPaymentString)
                                        VALUES (@EmployeeId, @CustomerId, @TotalAmount, @SaleDate, @DiscountPercent, @DiscountAmount, @Tax, @Notes, @ModeOfPayment, @ModeOfPaymentString);
                                        SELECT CAST(SCOPE_IDENTITY() as int)";
                var addInvoiceItemsSql = @"INSERT INTO [InvoiceItems] (InvoiceId, ItemId, UnitPrice, Quantity, TotalPrice, ServicedBy, DiscountPercent, DiscountAmount)
                                        VALUES (@InvoiceId, @ItemId, @UnitPrice, @Quantity, @TotalPrice, @ServicedBy, @DiscountPercent, @DiscountAmount)";

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

        private static async Task<InvoiceModel> GetInvoiceElements(IDbConnection connection, InvoiceModel invoice)
        {
            var invoiceItems =
                           connection.Query<InvoiceItems>(getInvoiceItemsSql, new { @InvoiceId = invoice.Id }).ToList();
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
            return invoice;
        }
    }
}
