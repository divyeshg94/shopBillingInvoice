using System;
using System.Collections.Generic;

namespace InvoiceGenerator.Models
{
    public class ExpenditureModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ExpenditureTypeEnum Type { get; set; }
        public float Amount { get; set; }
        public DateTime BillDate { get; set; }
        public string Notes { get; set; }
        public ICollection<ExpenditureItemsModel> ExpenditureItems { get; set; }
    }

    public enum ExpenditureTypeEnum
    {
        Shop = 1,
        EmployeeSalary = 2,
        Products = 3,
        Misc = 100
    }
}
