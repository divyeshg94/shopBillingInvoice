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
        public double TotalPrice { 
            get {
                var total = UnitPrice * Quantity;
                if(DiscountPercent != 0)
                {
                    total -= (DiscountPercent * 100);
                }
                if(DiscountAmount != 0)
                {
                    total -= DiscountAmount;
                }
                return total; 
            } 
            set {
            
            } 
        }

        public int ServicedBy { get; set; }

        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }

        public int Quantity { get; set; }
    }
}
