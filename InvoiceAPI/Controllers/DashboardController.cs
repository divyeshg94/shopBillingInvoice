using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using InvoiceGenerator.Service.Service;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("dashboard")]
    public class DashboardController : ApiController
    {
        // GET: DashboardData
        //[HttpGet]
        //[Route("")]
        //public List<CustomerModel> GetAll()
        //{
        //    var dashboardService = new DashboardService();
        //    dashboardService.GetDashboardData();
        //}
    }
}