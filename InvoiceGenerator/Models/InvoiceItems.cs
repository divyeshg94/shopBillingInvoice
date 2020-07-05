namespace InvoiceGenerator.Models
{
    public class InvoiceItems
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }

        public int ItemId { get; set; }
        public ItemsModel Item { get; set; }

        public double UnitPrice { get; set; }
        public double TotalPrice { get { return UnitPrice * Quantity; } set { } }

        public int Quantity { get; set; }
    }
}
