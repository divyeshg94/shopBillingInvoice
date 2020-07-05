using System;

namespace InvoiceGenerator.Models
{
    public class EmployeeAttendanceModel
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public bool IsPresent { get; set; }

       public decimal Duration { get; set; }
    }


    public class EmployeeAttendanceQueryModel
    {
        public int EmployeeId { get; set; }
        public int TotalWorkingDays { get; set; }
        public DateTime QueryStartTime { get; set; }
        public DateTime QueryEndTime { get; set; }
    }
}