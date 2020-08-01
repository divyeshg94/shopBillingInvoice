using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGenerator.Service.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class DashboardTests
    {
        [TestMethod]
        public void GetDashboardData()
        {
            var dashboardService = new DashboardService();
            var dashboardData = dashboardService.GetDashboardData();
        }
    }
}
