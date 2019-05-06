using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using InvoiceGenerator.Models;

namespace invoiceGenerator.PersistenceSql
{
    public class Stock: Repository
    {
        public static List<StockModel> GetAllStocks()
        {
            try
            {
                var getAllItemsSql = @"SELECT Id, ProductId, CustomerId, Quantity, GivenOn, IsPaid, TotalAmount FROM [Stock]";
                using (var connection = OpenConnection())
                {
                    var allCustomers = connection.Query<StockModel>(getAllItemsSql).ToList();
                    return allCustomers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StockModel GetStock(int productId)
        {
            try
            {
                var getItemSql = @"SELECT Id, ProductId, CustomerId, Quantity, GivenOn, IsPaid, TotalAmount FROM [Stock] WHERE ProductId = @productId";
                using (var connection = OpenConnection())
                {
                    var item = connection.QueryFirstOrDefault<StockModel>(getItemSql, new { @productId = productId });
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
