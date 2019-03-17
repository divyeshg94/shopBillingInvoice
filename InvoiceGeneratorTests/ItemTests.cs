using System;
using System.Linq;
using invoiceGenerator.PersistenceSql;
using InvoiceGenerator.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceGeneratorTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void GetAllItems()
        {
            var items = Item.GetAllItems();
        }

        [TestMethod]
        public void AddItem()
        {
            var item = new ItemsModel()
            {
                Name = "TestItem",
                Category = "ItemCategory",
                IsAvailable = true,
                Price = 10
            };
            Item.AddItem(item);
        }

        [TestMethod]
        public void UpdateItem()
        {
            var item = new ItemsModel()
            {
                Name = "TestItem",
                Category = "ItemCategory",
                IsAvailable = true,
                Price = 5
            };
            Item.UpdateItem(item);
        }

        [TestMethod]
        public void DeleteItem()
        {
            var item = Item.GetAllItems().First();
            Item.DeleteItem(item.Id);
        }
    }
}
