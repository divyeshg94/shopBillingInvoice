using System;

namespace InvoiceGenerator.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime? ReleavedOn { get; set; }
        public bool IsExists { get; set; }
        public string AadharNo { get; set; }
        public string Photo { get; set; }
        public string AadharImage { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
    }
}
