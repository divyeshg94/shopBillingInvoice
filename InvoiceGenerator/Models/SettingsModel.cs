using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace InvoiceGenerator.Models
{
    public class SettingModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public SettingModel()
        {
            CreatedOn = SqlDateTime.MinValue.Value;
            UpdatedOn = SqlDateTime.MinValue.Value;
        }
    }
}
