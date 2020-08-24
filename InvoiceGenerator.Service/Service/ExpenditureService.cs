using System.Threading.Tasks;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Service.Service
{
    public class ExpenditureService
    {
        public async Task<DataTableResponse<ExpenditureModel>> GetPaginatedExpenditure(DataTableQuery dataTableQuery)
        {
            var skip = dataTableQuery != null && dataTableQuery.Skip.HasValue ? dataTableQuery?.Skip.Value : 0;
            var take = dataTableQuery != null && dataTableQuery.Take.HasValue ? dataTableQuery?.Take.Value : 10;

            var expenditure = await Expenditure.GetAllExpenditure(skip.Value, take.Value);
            return new DataTableResponse<ExpenditureModel>
            {
                data = expenditure,
                draw = 0,
                recordsFiltered = expenditure.Count,
                recordsTotal = expenditure.Count
            };
        }
    }
}
