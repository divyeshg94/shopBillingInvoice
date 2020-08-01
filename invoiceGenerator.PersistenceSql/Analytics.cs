using System;
using System.Collections.Generic;
using Dapper;

namespace invoiceGenerator.PersistenceSql
{
    public class Analytics : Repository
    {
        #region ProfitLoss
        public static Dictionary<string, string> GetProfitLossForMonth(DateTime startDate, DateTime endDate)
        {
            try
            {
                var getInvoicesInRange = @"select * from Invoice where SaleDate between @startDate and @endDate";
                using (var connection = OpenConnection())
                {
                    //var allCustomers = connection.Query<Dictionary<string, string>>(getProfitLoss);
                    //return allCustomers;
                    return new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ProfitLoss
    }
}
