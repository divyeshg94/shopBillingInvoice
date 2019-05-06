namespace InvoiceGenerator.Models
{
    public class InvoiceItems
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }

        public int ItemId { get; set; }
        public ItemsModel Item { get; set; }

        public int Cost { get; set; }

        public int Quantity { get; set; }
    }
}
