using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using invoiceGenerator.PersistenceSql;

namespace InvoiceGenerator.Service.Service
{
    public class DashboardService
    {
        public async Task<Dictionary<string, string>> GetDashboardData()
        {
            try
            {
                var result = new Dictionary<string, string>();
                var today = DateTime.UtcNow;
                var startDateOfMonth = new DateTime(today.Year, today.Month, 1);
                var minSqlDate = SqlDateTime.MinValue;

                var invoices = await Invoice.GetAllInvoices(startDateOfMonth, today);
                var totalCost = invoices.Sum(i => long.Parse(i.TotalAmount));
                result.Add("Total Month Cost", totalCost.ToString());

                var newCustomers = await Customer.GetNewCustomers(startDateOfMonth, today);
                result.Add("New Customers", newCustomers.Count().ToString());

                var allExpenditures = await Expenditure.GetAllExpenditure(DateTime.Parse(minSqlDate.ToString()), today);
                result.Add("Total Expenses", allExpenditures.Sum(e => e.Amount).ToString());

                var monthExpenses = allExpenditures.Where(x =>
                                                   (x.BillDate >= startDateOfMonth && x.BillDate <= today) ||
                                                   (x.BillDate >= startDateOfMonth && x.BillDate <= today) ||
                                                   (startDateOfMonth >= x.BillDate && startDateOfMonth <= x.BillDate) ||
                                                   (today >= x.BillDate && today <= x.BillDate));
                result.Add("Total Month Expense", monthExpenses.Sum(e => e.Amount).ToString());

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
