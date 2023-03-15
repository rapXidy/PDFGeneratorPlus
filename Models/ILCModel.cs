using System.ComponentModel.DataAnnotations;

namespace SterlingPDF.Models
{
    public class ILCModel
    {
        public DateTime TodaysDate { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public string ILCReference { get; set; }
        public string BeneficiaryName { get; set; }
        public string Applicationnumber { get; set; }
        [DataType(DataType.Currency)]
        public decimal ILCAmount { get; set; }
        public List<ILCCharges> LstOfCharges { get; set; } //= new List<ILCCharges>();
        
    }

    public class ILCCharges
    {
        public string ChargeDescription { get; set; }
        [DataType(DataType.Currency)]
        public decimal CommAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal VATAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}
