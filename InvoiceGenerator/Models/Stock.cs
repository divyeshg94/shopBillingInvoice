using System;

namespace InvoiceGenerator.Models
{
    public class StockModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public ItemsModel Item { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public int Quantity { get; set; }

        public DateTime GivenOn { get; set; }

        public bool IsPaid { get; set; }

        public string TotalAmount { get; set; }
    }
}
