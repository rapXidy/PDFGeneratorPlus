using SterlingPDF.Models;

namespace SterlingPDF.Repository.Interfaces
{
    public interface IPDFGeneratorCore
    {
        string CreateReceiptPDf(bool isDraft);
        string CreateDebitAdvisePDf(bool isDraft);
        //LCStatementModel GetILCInfo();
    }
}