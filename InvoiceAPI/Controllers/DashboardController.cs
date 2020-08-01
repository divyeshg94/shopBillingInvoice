using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using InvoiceGenerator.Service.Service;

namespace InvoiceAPI.Controllers
{
    [RoutePrefix("dashboard")]
    public class DashboardController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<Dictionary<string, string>> DashboardData()
        {
            var dashboardService = new DashboardService();
            return await dashboardService.GetDashboardData();
        }
    }
}