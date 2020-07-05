namespace InvoiceGenerator.Models
{
    public class ItemsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public ItemType Type { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsProduced { get; set; }
    }

    public enum ItemType
    {
        Product = 0,
        Service = 10
    }
}
