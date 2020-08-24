namespace InvoiceGenerator.Models
{
    public class DataTableQuery
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string SortProperty { get; set; }

        public string SortDirection { get; set; }

        public string Filter { get; set; }

        public string AsQueryString { get; }
    }
}
