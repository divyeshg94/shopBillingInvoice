﻿using System;
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

        public double DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double TaxPercentage { get; set; }
        public string Notes { get; set; }

        public ICollection<InvoiceItems> InvoiceItemses { get; set; }
    }
}
