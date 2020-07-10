namespace InvoiceGenerator.Models
{
    public class CustomerDetailsModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string SkinType { get; set; }
        public string HairType { get; set; }
        public bool IsSensitiveSkin { get; set; }
        public string MedicalHistory { get; set; }
        public string Allergies { get; set; }
        public string Problems { get; set; }
        public string CurrentProducts { get; set; }
    }
}
