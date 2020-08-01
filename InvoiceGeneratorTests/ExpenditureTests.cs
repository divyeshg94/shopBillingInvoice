using System;
using System.Collections.Generic;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class ExpenditureTests
    {
        [TestMethod]
        public void AddExpenditure()
        {
            //var expenditure = new ExpenditureModel()
            //{
            //    Name = "Shop Advance",
            //    Amount = 150000,
            //    BillDate = new DateTime(2020, 06, 01),
            //    Type = ExpenditureTypeEnum.Shop,
            //    Notes = "Advance amount gave to the shop owner"
            //};

            var items = new List<ExpenditureItemsModel>();

            items.Add(new ExpenditureItemsModel()
            {
                Name = "Sun screen lotion",
                Quantity = 5,
                UnitPrice = 10
            });

            var expenditure = new ExpenditureModel()
            {
                Name = "Nykaa",
                BillDate = DateTime.UtcNow,
                Type = ExpenditureTypeEnum.Products,
                ExpenditureItems = items,
                Notes = "Product bought to use for the customers",
                Amount = items.Sum(i => i.TotalPrice)
            };

            Expenditure.AddExpenditure(expenditure);
        }
    }
}
