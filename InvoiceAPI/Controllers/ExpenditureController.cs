using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.Service;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("expenditure")]
    public class ExpenditureController : ApiController
    {
        [HttpPost]
        [Route("byDate")]
        public async Task<List<ExpenditureModel>> GetAllExpenditure(DateTime fromDate, DateTime toDate)
        {
            var expenditures = await Expenditure.GetAllExpenditure(fromDate, toDate);
            return expenditures;
        }

        [HttpPost]
        [Route("")]
        public async Task<DataTableResponse<ExpenditureModel>> GetExpenditure(DataTableQuery query)
        {
            var expenditureService = new ExpenditureService();
            return await expenditureService.GetPaginatedExpenditure(query);
        }

        //[HttpGet]
        //[Route("id")]
        //public EmployeeModel GetEmployee(int employeeId)
        //{
        //    return Employee.GetEmployee(employeeId);
        //}
        //[HttpDelete]
        //public void DeleteEmployee(int employeeId)
        //{
        //    Employee.DeleteEmployee(employeeId);
        //}
    }
}