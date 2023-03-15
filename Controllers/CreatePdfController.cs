using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SterlingPDF.Repository.Interfaces;

namespace SterlingPDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePdfController : ControllerBase
    {
        private readonly IPDFGeneratorCore _pDFGeneratorCore;

        public CreatePdfController(IPDFGeneratorCore pDFGeneratorCore)
        {
            _pDFGeneratorCore = pDFGeneratorCore;
        }


        [HttpGet,Route("GetCreatedPdf")]
        public IActionResult Get()
        {
            var response = _pDFGeneratorCore.CreateReceiptPDf(true);
            return Ok(response);
        }

        [HttpGet,Route("GetDebitAdvisePdf")]
        public IActionResult GetDebitAdvisePdf()
        {
            var response = _pDFGeneratorCore.CreateDebitAdvisePDf(true);
            return Ok(response);
        }
    }
}
