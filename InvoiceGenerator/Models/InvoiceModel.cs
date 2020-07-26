using System;
using System.Collections.Generic;

namespace InvoiceGenerator.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }

        public string TotalAmount { get; set; }

        public DateTime SaleDate { get; set; }

        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public double Tax { get; set; }
        public string Notes { get; set; }

        public ModeOfPaymentEnum ModeOfPayment {get;set;}
        public string ModeOfPaymentString {get;set;}

        public ICollection<InvoiceItems> InvoiceItemses { get; set; }
    }

    public enum ModeOfPaymentEnum
    {
        Cash = 0,
        Card = 10,
        UPI = 20
    }
}
