namespace InvoiceGenerator.Models
{
    public class ExpenditureItemsModel
    {
        public int Id { get; set; }
        public int ExpenditureId { get; set; }
        public ExpenditureModel Expenditure { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
            set { }
        }
    }
}
