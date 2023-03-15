using SterlingPDF.Models;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using SterlingPDF.Repository.Interfaces;
using iText.Kernel.Pdf.Extgstate;

namespace SterlingPDF.Repository.Implementations
{
    public class PDFGeneratorCore : IPDFGeneratorCore
    {

        public LCStatementModel GetILCInfo()
        {
            LCStatementModel model = new LCStatementModel();
            model.MessagePrepApplication = "Alliance Message Management";
            model.UniqueMessageId = "I BRBAGB2LXXX 700 SB/ILC220210C (suffix 2203221481145)";
            model.Status = "Possible duplicate indicator set locally Message Modified Deletable";
            model.Format = "Swift";
            model.SubFormat = "Input";
            model.Identifier = "fin.700";
            model.Nature = "Financial";
            model.LT = 'A';
            model.LT2 = 'X';
            model.Application = "FIN";
            model.Sender = "NAMENGLAXXX";
            model.Receiver = "BRBAGB2LXXX";
            model.TransactionReference = "SB/ILC220210C";
            model.Priority = "Normal";
            model.Monitoring = "None";
            model.Amount = 65600.00m;
            model.Currency = "USD";
            model.ValueDate = DateTime.Now;
            model.ACKNAKRecptDate = DateTime.Now;
            model.TransactionResultDetails = "This LC is pending approval from the HQ in Marina, Lagos ";
            model.SenderInstitution = "NAMENGLAXXX";
            model.ReceiverInstitution = "BRBAGB2LXXX";
            model.ExpansionSender = "STERLING BANK PLC LAGOS LAGOS NG NIGERIA";
            model.ExpansionReceiver = "BANK OF BEIRUT (UK) LTD LONDON W1Y 7FE LONDON GB UNITED KINGDOM";
            model.GMT = "+1";
            model.F27_Number = "1 /";
            model.F27_Total = "1";
            model.F40A_FormOfCredit = "IRREVOCABLE";
            model.F20_CreditNumber = "SB/ILC220210C";
            model.F31C_DateOfIssue = DateTime.Now;
            model.F40E_ApplicableRules = "UCP LATEST VERSION";
            model.F51A_ApplicantBank = "STERLING BANK PLC LAGOS NG";
            model.F31D_DateOfExpiry = DateTime.Now;
            model.F31D_PlaceOfExpiry = "LONDON";
            model.F51A_IdentifierCode = "NAMENGLA";
            model.F51A_PartyIdentifier = "";
            model.F50_Applicant = "JOBIKAN (NIGERIA) LIMITED PLOT 41, KUDIRAT ABIOLA WAY OREGUN, IKEJA, LAGOS, NIGERIA.";
            model.F59_BeneficiaryName = "SNETOR OVERSEAS";
            model.F59_BeneficiaryAddress = "11 AVENUE DUBONNET-92407 COURBEVOIE CEDEX -FRANCE.";
            model.F32B_CurrencyCode = "USD US DOLLAR";
            model.F32B_Amount = String.Format("{0:C2}", model.Amount);
            model.F43P_PartialShipment = "ALLOWED";
            model.F43T_TransShipment = "ALLOWED";
            model.F41A_Code = "BY PAYMENT";
            model.Expansion = "";
            model.F44E_PortOfDeparture = "ANY SEAPORT IN SOUTH AFRICA";
            model.F44F_PortOfDischarge = "APAPA PORT, LAGOS, NIGERIA";
            model.F44C_DateOfLatestShipment = DateTime.Now;
            model.F45A_DescriptionOfGoods = "POLYPROPYLENE HOMOPOLYMER SASOL HSV103 AS PER PFI NO. 1122003170 DATED 14 / 03 / 2022 CFR APAPA SEAPORT, LAGOS NIGERIA.";
            model.F46A_DocumentsRequired = "LCSterling-19303";
            model.F47A_AdditionalConditions = "LCSterling-19303";
            model.F71D_Charges = "LCSterling-19303";
            model.F48_Days = "LCSterling-19303";
            model.F48_Narrative = "LCSterling-19303";
            model.F49_ConfirmationInstructions = "LCSterling-19303";
            model.F78_InstructionAccountNo = "LCSterling-19303";
            model.F72Z_SenderReceiverInfo = "THIS LETTER OF CREDIT IS SUBJECT TO UNIFORM CUSTOMS AND PRACTICE FOR DOCUMENTARY CREDIT(2007 REVISION) UCP 600.NOTIFY THE ISSUING BANK BY SWIFT UPON ADVISE OF LC";
            model.Other_DeliveryOverdueRequest = "No";
            model.Other_NetworkRequest = "No";
            model.Other_PaymentConfirmationStatus = "LCSterling-19303";
            model.Other_ConfirmedCurrency = "LCSterling-19303";
            model.Other_ConfirmedAmount = "LCSterling-19303";
            model.Other_ConfirmedDate = DateTime.Now;


            return model;
        }

        public ILCModel GetDebitAdviceInfo()
        {
            ILCModel model = new ILCModel();
            model.ApplicantName = "Chidiebere Alfred Levi";
            model.TodaysDate = DateTime.Now;
            model.ApplicantAddress = "31, Maitama Ave. Abuja, Nigeria";
            model.ILCReference = "LC-136r876Stn";
            model.Applicationnumber = "App-136r876Stn";
            model.ILCAmount = 700000.00m;
            model.BeneficiaryName = "Adegboola Muhammed";
            model.LstOfCharges = new List<ILCCharges>
            {
                new ILCCharges {ChargeDescription="LC charges for December",CommAmount=35000.00m,VATAmount=50000.50m,Total=815000.50m },
                new ILCCharges {ChargeDescription="LC charges for Jan 2021",CommAmount=65000.00m,VATAmount=50500.50m,Total=850000.50m },
                new ILCCharges {ChargeDescription="LC charges for April 19",CommAmount=15000.00m,VATAmount=60000.50m,Total=105000.50m },
                new ILCCharges {ChargeDescription="LC charges for February 30th",CommAmount=25000.00m,VATAmount=90000.50m,Total=105000.50m }
            };


            return model;
        }

        public string CreateReceiptPDf(bool isDraft)
        {
            try
            {
                var getLcInfo = GetILCInfo();
                string pdfPathFolder = "C:/OneTradePdfFolder";
                if (!Directory.Exists(pdfPathFolder))
                {
                    Directory.CreateDirectory(pdfPathFolder);
                }

                string fileName = "LC" + Guid.NewGuid().ToString().Substring(0, 4) + DateTime.Now.ToString("ddMMMMyyyyHHmmssfffff") + ".pdf";
                string fullFilePath = System.IO.Path.Combine(pdfPathFolder, fileName);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                PdfFont font2 = PdfFontFactory.CreateFont(StandardFonts.COURIER, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                DeviceRgb purpleColour = new(128, 0, 128);
                DeviceRgb blackColour = new(0, 0, 0);
                DeviceRgb whiteColour = new(255, 255, 255);
                DeviceRgb offPurpleColour = new(230, 230, 250);

                Border b1 = new SolidBorder(purpleColour, 1);
                Border b2 = new SolidBorder(blackColour, 1);



                PdfDocument pdfDocument = new(new PdfWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write)));
                Document document = new(pdfDocument, PageSize.A4, false); //find a way to include the margins
                document.SetMargins(20, 20, 20, 40);

                //===============================Set headers footers using PDFEventHandlers=========================================================
                pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new PDFHeaderEventHandler());
                PDFFooterEventHandler currentEvent = new();
                pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, currentEvent);

                // empty line
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));

                Paragraph paragraph1 = new Paragraph("Messages")
                    .SetBackgroundColor(blackColour)
                    .SetFont(font)
                    .SetBold().SetTextAlignment(TextAlignment.LEFT)
                    .SetFontColor(ColorConstants.WHITE);
                    //.SetStrokeColor(whiteColour);

                document.Add(paragraph1);

                // empty line
                document.Add(new Paragraph(""));

                Paragraph paragraph2 = new Paragraph("Message 1")
                    .SetBold()
                    .SetFont(font)
                    .SetTextAlignment(TextAlignment.CENTER);
                document.Add(paragraph2);

                // Table
                Table table = new Table(4, true).SetBorder(b2);

                Paragraph paragraph3 = new Paragraph("***Possible Duplicate Emission***");
                document.Add(paragraph3);

                //Paragraph paragraph4 = new Paragraph("Message Identifier")
                //    .SetBackgroundColor(purpleColour)
                //    .SetBold();

                Cell cellHeaderName = new Cell(4,4)
                  .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                  .SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER)
                  .SetMarginLeft(2F).SetMarginRight(2F)
                  .SetBold()
                  .Add(new Paragraph("Message Identifier"));

                //table.AddHeaderCell(cellHeaderName);
                // Headings
                Cell cellProductId = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Message Preparation Application: "));

                Cell cellProductName = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .Add(new Paragraph(getLcInfo.MessagePrepApplication));
                
                Cell cellmsg = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Unique Message Identifier: "));

                Cell cellmsg2 = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .Add(new Paragraph(getLcInfo.UniqueMessageId));

                //cell header here
                Cell cellHeader2 = new Cell(4, 4)
                  .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                  .SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER)
                  .SetMarginLeft(2F).SetMarginRight(2F).SetBold()
                  .Add(new Paragraph("Message Header"));

                //4x 4 cells here

                Cell cellQuantity = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Status: "));

                Cell cellUnitPrice = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .Add(new Paragraph(getLcInfo.Status));
                
                Cell cell1Quantity = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Format: "));

                Cell cell2UnitPrice = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Format));
                  
                Cell cell1subformat = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Sub-Format: "));

                Cell cell2subformat = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.SubFormat));
                   
                Cell cell1Quantity1 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Identifier: "));

                Cell cell2UnitPrice1 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Identifier));
                  
                Cell cell1subformat1 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Expansion: "));

                Cell cell2subformat1 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Expansion));
                
                Cell cell1Quantity2 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Application "));

                Cell cell2UnitPrice2 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Application));
                  
                Cell cell1subformat2 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Nature: "));

                Cell cell2subformat2 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Nature));
                
                Cell cell1Quantity3 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Sender: "));

                Cell cell2UnitPrice3 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Sender));
                  
                Cell cell1subformat3 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("LT: "));

                Cell cell2subformat3 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.LT.ToString()));
                                
                Cell cell1Quantity4 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Receiver: "));

                Cell cell2UnitPrice4 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Receiver));
                  
                Cell cell1subformat4 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("LT: "));

                Cell cell2subformat4 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.LT2.ToString()));
                                                
                Cell cell1Quantity5 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Transaction Reference: "));

                Cell cell2UnitPrice5 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.TransactionReference));
                  
                Cell cell1subformat5 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Priority: "));

                Cell cell2subformat5 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Priority));
                                                                
                Cell cell1Quantity6 = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Monitoring: "));

                Cell cell2UnitPrice6 = new Cell(2, 2)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Monitoring));
                  
                Cell cell1Quantity7 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Amount: "));

                Cell cell2UnitPrice7 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(String.Format("{0:C2}", getLcInfo.Amount)));
                   
                Cell cell1subformat7 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Currency: "));

                Cell cell2subformat7 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.Currency));
                                  
                Cell cell1Quantity8 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Value Date: "));

                Cell cell2UnitPrice8 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.ValueDate?.ToString("dddd, dd MMMM yyyy")));
                   
                Cell cell1subformat8 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("ACK/NAK Reception Date/Time: "));

                Cell cell2subformat8 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.ACKNAKRecptDate?.ToString("dddd, dd MMMM yyyy")));
                                                  
                Cell cell1Quantity9 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("(GMT): "));

                Cell cell2UnitPrice9 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.GMT));
                   
                Cell cell1subformat9 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Transaction Result Details: "));

                Cell cell2subformat9 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.TransactionResultDetails));


                //cell header here
                Cell cellHeader3 = new Cell(4, 4)
                  .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                  .SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER)
                  .SetMarginLeft(2F).SetMarginRight(2F).SetBold()
                  .Add(new Paragraph("Sender / Receiver"));

                //other 4 x 4 cells
                Cell cell1Quantity10 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Sender Institution: "));

                Cell cell2UnitPrice10 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.SenderInstitution));

                Cell cell1subformat10 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Expansion: "));

                Cell cell2subformat10 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.ExpansionSender));
                
                Cell cell1Quantity11 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Receiver Institution: "));

                Cell cell2UnitPrice11 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.ReceiverInstitution));

                Cell cell1subformat11 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .Add(new Paragraph("Expansion: "));

                Cell cell2subformat11 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetFont(font2)
                   .SetFontSize(11F)
                   .Add(new Paragraph(getLcInfo.ExpansionReceiver));

                //cell header here
                Cell cellHeader4 = new Cell(4, 4)
                  .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                  .SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER)
                  .SetMarginLeft(2F).SetMarginRight(2F).SetBold()
                  .Add(new Paragraph("Message Text"));

                //other cells
                Cell cellblock4 = new Cell(4, 4)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetBorder(Border.NO_BORDER)
                   .SetBold()
                   .SetFontColor(ColorConstants.LIGHT_GRAY)
                   .SetFontSize(8F)
                   .Add(new Paragraph("Block 4"));

                //===============================

                Cell celltxt = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F27: "));
                
                Cell celltxt1 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Sequence of Total "));
                                
                Cell celltxt2 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                
                Cell celltxt3 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));
                                                                
                Cell celltxt4 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));
                                                                
                Cell celltxt5 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Number: "));

                                                                                
                Cell celltxt6 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F27_Number));

                                                                                                
                Cell celltxt7 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                
                                                                                                
                Cell celltxt8 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                                                            
                Cell celltxt9 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Total: "));

                                                                                            
                Cell celltxt10 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F27_Total));

                                                                                           
                Cell celltxt11 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                                                                           
                Cell celltxt12 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F40A: "));

                                                                                                           
                Cell celltxt13 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Form of Documentary Credit: "));

                                                                                                           
                Cell celltxt14 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F40A_FormOfCredit));

                                                                                                           
                Cell celltxt15 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                                                                                           
                Cell celltxt16 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F20: "));

                                                                                                                           
                Cell celltxt17 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Documentary Credit Number "));

                                                                                                                           
                Cell celltxt18 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F20_CreditNumber));

                                                                                                                           
                Cell celltxt19 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                                                                                                           
                Cell celltxt20 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F31C: "));

                                                                                                                                           
                Cell celltxt21 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Date of Issue "));

                                                                                                                                           
                Cell celltxt22 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F31C_DateOfIssue.ToString("yy-MM-dd") + " "+ getLcInfo.F31C_DateOfIssue.ToString("yyyy-MMM-dd")));

                                                                                                                                           
                Cell celltxt23 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(""));

                                                                                                                                           
                Cell celltxt24 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F40E: "));

                                                                                                                                           
                Cell celltxt25 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Applicable Rules "));

                                                                                                                                           
                Cell celltxt26 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Applicable Rules:"));

                                                                                                                                           
                Cell celltxt27 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F40E_ApplicableRules));


                Cell celltxt29 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F31D:  Date and Place of Expiry: "));


                Cell celltxt30 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Date: "+ getLcInfo.F31D_DateOfExpiry.ToString("yyyyMMMdd")));
                
                Cell celltxt31 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F31D_DateOfExpiry.ToString("yy/MM/dd")));


                Cell celltxt32 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph($"Place:  {getLcInfo.F31D_PlaceOfExpiry}"));


                Cell celltxt33 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F51A: Applicant Bank: "));


                Cell celltxt34 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph($"{getLcInfo.F51A_ApplicantBank}"));


                Cell celltxt35 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph($"Party Identifier: {getLcInfo.F51A_PartyIdentifier}"));


                Cell celltxt36 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph($"Identifier Code: {getLcInfo.F51A_IdentifierCode}"));

                
                Cell celltxt37 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F50: "));

                
                Cell celltxt38 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Applicant: "));

                Cell celltxt39 = new Cell(2, 2)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F50_Applicant));

                Cell celltxt40 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("F59: "));
                
                Cell celltxt41 = new Cell(1, 1)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph("Beneficiary Name and Address: "));
                
                Cell celltxt42 = new Cell(2, 2)
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetBorder(Border.NO_BORDER)
                  .SetFont(font2)
                  .SetFontSize(9F)
                  .Add(new Paragraph(getLcInfo.F59_BeneficiaryName + ", " + getLcInfo.F59_BeneficiaryAddress));


                //add the cells to the table
                table.AddCell(cellHeaderName);
                table.AddCell(cellProductId);
                table.AddCell(cellProductName);
                table.AddCell(cellmsg);
                table.AddCell(cellmsg2);
                table.AddCell(cellHeader2);
                //table.AddCell(cellProductName);

                //4x4 cells here

                table.AddCell(cellQuantity);
                table.AddCell(cellUnitPrice);
                table.AddCell(cell1Quantity);
                table.AddCell(cell2UnitPrice);
                table.AddCell(cell1subformat);
                table.AddCell(cell2subformat);
                table.AddCell(cell1Quantity1);
                table.AddCell(cell2UnitPrice1);
                table.AddCell(cell1subformat1);
                table.AddCell(cell2subformat1);
                table.AddCell(cell1Quantity2);
                table.AddCell(cell2UnitPrice2);
                table.AddCell(cell1subformat2);
                table.AddCell(cell2subformat2);
                table.AddCell(cell1Quantity3);
                table.AddCell(cell2UnitPrice3);
                table.AddCell(cell1subformat3);
                table.AddCell(cell2subformat3);
                table.AddCell(cell1Quantity4);
                table.AddCell(cell2UnitPrice4);
                table.AddCell(cell1subformat4);
                table.AddCell(cell2subformat4);
                table.AddCell(cell1Quantity5);
                table.AddCell(cell2UnitPrice5);
                table.AddCell(cell1subformat5);
                table.AddCell(cell2subformat5);
                table.AddCell(cell1Quantity6);
                table.AddCell(cell2UnitPrice6);
                table.AddCell(cell1Quantity7);
                table.AddCell(cell2UnitPrice7);
                table.AddCell(cell1subformat7);
                table.AddCell(cell2subformat7);
                table.AddCell(cell1Quantity8);
                table.AddCell(cell2UnitPrice8);
                table.AddCell(cell1subformat8);
                table.AddCell(cell2subformat8);
                table.AddCell(cell1Quantity9);
                table.AddCell(cell2UnitPrice9);
                table.AddCell(cell1subformat9);
                table.AddCell(cell2subformat9);
                //header here
                table.AddCell(cellHeader3);
                //other cells below

                table.AddCell(cell1Quantity10);
                table.AddCell(cell2UnitPrice10);
                table.AddCell(cell1subformat10);
                table.AddCell(cell2subformat10);
                table.AddCell(cell1Quantity11);
                table.AddCell(cell2UnitPrice11);
                table.AddCell(cell1subformat11);
                table.AddCell(cell2subformat11);

                //header here
                table.AddCell(cellHeader4);
                //other cells below
                table.AddCell(cellblock4);
                table.AddCell(celltxt);
                table.AddCell(celltxt1);
                table.AddCell(celltxt2);
                table.AddCell(celltxt3);
                table.AddCell(celltxt4);
                table.AddCell(celltxt5);
                table.AddCell(celltxt6);
                table.AddCell(celltxt7);
                table.AddCell(celltxt8);
                table.AddCell(celltxt9);
                table.AddCell(celltxt10);
                table.AddCell(celltxt11);
                table.AddCell(celltxt12);
                table.AddCell(celltxt13);
                table.AddCell(celltxt14);
                table.AddCell(celltxt15);
                table.AddCell(celltxt16);
                table.AddCell(celltxt17);
                table.AddCell(celltxt18);
                table.AddCell(celltxt19);
                table.AddCell(celltxt20);
                table.AddCell(celltxt21);
                table.AddCell(celltxt22);
                table.AddCell(celltxt23);
                table.AddCell(celltxt24);
                table.AddCell(celltxt25);
                table.AddCell(celltxt26);
                table.AddCell(celltxt27);
                table.AddCell(celltxt29);
                table.AddCell(celltxt30);
                table.AddCell(celltxt31);
                table.AddCell(celltxt32);
                table.AddCell(celltxt33);
                table.AddCell(celltxt34);
                table.AddCell(celltxt35);
                table.AddCell(celltxt36);
                table.AddCell(celltxt37);
                table.AddCell(celltxt38);
                table.AddCell(celltxt39);
                table.AddCell(celltxt40);
                table.AddCell(celltxt41);
                table.AddCell(celltxt42);


                document.Add(table);

                //Write the page number
                currentEvent.WritePageNumbers(pdfDocument, document);

                //Add watermark if doc isDraft = true;
                if (isDraft)
                {
                    string logoPath = AppDomain.CurrentDomain.BaseDirectory + "\\SterlingLogo2use.png";

                    WatermarkPdf(logoPath,"","", document,pdfDocument,20,0.2F,2F,10F,null,null,40F);

                }
                //close the document
                document.Close();

                return "Sample PDF successfully generated";
            }
            catch (Exception ex)
            {

                throw;
            }
            //return " ";
        }

        public string CreateDebitAdvisePDf(bool isDraft)
        {
            try
            {

                var getLcInfo = GetDebitAdviceInfo();
                string pdfPathFolder = "C:/OneTradePdfFolder";
                if (!Directory.Exists(pdfPathFolder))
                {
                    Directory.CreateDirectory(pdfPathFolder);
                }

                string fileName = "LC" + Guid.NewGuid().ToString().Substring(0, 4) + DateTime.Now.ToString("ddMMMMyyyyHHmmssfffff") + ".pdf";
                string fullFilePath = System.IO.Path.Combine(pdfPathFolder, fileName);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                PdfFont font2 = PdfFontFactory.CreateFont(StandardFonts.COURIER, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                DeviceRgb purpleColour = new(128, 0, 128);
                DeviceRgb blackColour = new(0, 0, 0);
                DeviceRgb whiteColour = new(255, 255, 255);
                DeviceRgb offPurpleColour = new(230, 230, 250);

                Border b1 = new SolidBorder(purpleColour, 1);
                Border b2 = new SolidBorder(blackColour, 1);



                PdfDocument pdfDocument = new(new PdfWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write)));
                Document document = new(pdfDocument, PageSize.A4, false); //find a way to include the margins
                document.SetMargins(20, 20, 20, 40);

                //===============================Set headers footers using PDFEventHandlers=========================================================
                pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new PDFHeaderEventHandler());
                PDFFooterEventHandler currentEvent = new();
                pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, currentEvent);

                // empty line
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));

                document.Add(new Paragraph(getLcInfo.TodaysDate.ToString("dd MMM yyyy")));
                document.Add(new Paragraph(getLcInfo.ApplicantName));
                document.Add(new Paragraph(getLcInfo.ApplicantAddress));

                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));

                document.Add(new Paragraph("Dear Sir/Ma,"));
                document.Add(new Paragraph(""));

                document.Add(new Paragraph($"Debit Advise for your Import Letter of Credit (Service)- {getLcInfo.ILCReference}")
                    .SetTextAlignment(TextAlignment.CENTER));
                document.Add(new Paragraph(""));

                document.Add(new Paragraph("We are pleased to inform you that an import letter of Credit has been established successfully on your behalf. Kindly see below the details of your ILC"));
                document.Add(new Paragraph($"Applicant Name: {getLcInfo.ApplicantName}"));
                document.Add(new Paragraph($"Beneficiary Name: {getLcInfo.BeneficiaryName}"));
                document.Add(new Paragraph($"ILC Reference: {getLcInfo.ILCReference}"));
                document.Add(new Paragraph($"Application Number: {getLcInfo.Applicationnumber}"));
                document.Add(new Paragraph($"Amount: {String.Format("{0:C2}", getLcInfo.ILCAmount)}"));

                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                document.Add(new Paragraph(""));
                
                document.Add(new Paragraph("Kindly find below a breakdown of all the charges"));
                document.Add(GetDebitAdvisePdfTable());

                //Write the page number
                currentEvent.WritePageNumbers(pdfDocument, document);

                //Add watermark if doc isDraft = true;
                if (isDraft)
                {
                    string logoPath = AppDomain.CurrentDomain.BaseDirectory + "\\SterlingLogo2use.png";

                    WatermarkPdf(logoPath, "", "", document, pdfDocument, 20, 0.2F, 2F, 10F, null, null, 40F);

                }
                //close the document
                document.Close();

                return "Sample PDF successfully generated";
            }
            catch (Exception ex)
            {

                throw;
            }
                //return "";
        }

        private Table GetDebitAdvisePdfTable()
        {
            // Table
            Table table = new Table(5, false);

            // Headings
            Cell SN = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("S/N"));

            Cell ChargeDescription = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("Charge Description"));

            Cell CommAmount = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Comm. Amount"));

            Cell VATAmount = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("VAT Amount"));
            
            Cell cellTotal = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Total"));

            table.AddCell(SN);
            table.AddCell(ChargeDescription);
            table.AddCell(CommAmount);
            table.AddCell(VATAmount);
            table.AddCell(cellTotal);

            var charges = GetDebitAdviceInfo();
            if (charges.LstOfCharges.Any())
            {
                foreach (var item in charges.LstOfCharges)
                {
                    int i = 1;
                    Cell cId = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(i.ToString()));

                    Cell cName = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .Add(new Paragraph(item.ChargeDescription));

                    Cell cQty = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .Add(new Paragraph(String.Format("{0:C2}", item.CommAmount)));

                    Cell cPrice = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .Add(new Paragraph(String.Format("{0:C2}", item.VATAmount)));
                    
                    Cell cTotal = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .Add(new Paragraph(String.Format("{0:C2}", item.CommAmount+item.VATAmount)));

                    table.AddCell(cId);
                    table.AddCell(cName);
                    table.AddCell(cQty);
                    table.AddCell(cPrice);
                    table.AddCell(cTotal);
                    i++;
                }

            }

            return table;
        }


        public void WatermarkPdf(
           //byte[] sourceData,
           string watermarkImgPath,
           string watermarkText,
           string textFont,
           Document doc,
           PdfDocument pdfDoc,
           int? fontSize,
           float? opacity,
           float? x,
           float? y,
           TextAlignment? textAlign,
           VerticalAlignment? vertAlign,
           float? radAngle
           )
        {
            try
            {
                //using (var ms = new MemoryStream())
                //{
                //    var sourceStream = new MemoryStream(sourceData);

                //pdfDoc = new PdfDocument(new PdfReader(sourceStream), new PdfWriter(ms));
                //doc = new Document(pdfDoc);
                PdfFont font = PdfFontFactory.CreateFont(FontProgramFactory.CreateFont(string.IsNullOrEmpty(textFont) ? StandardFonts.TIMES_ROMAN : textFont));
                Paragraph paragraph = new Paragraph(new Text(watermarkText)).SetFont(font).SetFontSize(fontSize ?? 7);

                // Loop through all pages for adding watermark text/image at all pages of pdf
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfPage pdfPage = pdfDoc.GetPage(i);
                    Rectangle pageSize = pdfPage.GetPageSize();
                    float pg_x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
                    float pg_y = (pageSize.GetTop() + pageSize.GetBottom()) / 2;
                    doc.ShowTextAligned(paragraph, x ?? pg_x, y ?? pg_y, i, textAlign ?? TextAlignment.LEFT, vertAlign ?? VerticalAlignment.TOP, radAngle ?? 0);
                    if (!string.IsNullOrEmpty(watermarkImgPath))
                    {
                        PdfCanvas over = new PdfCanvas(pdfPage);
                        over.SaveState();
                        PdfExtGState gs1 = new PdfExtGState().SetFillOpacity(opacity ?? 0.5f);
                        over.SetExtGState(gs1);
                        ImageData img = ImageDataFactory.Create(watermarkImgPath);
                        float w = img.GetWidth();
                        float h = img.GetHeight();
                        over.AddImageWithTransformationMatrix(img, w, 0, 0, h, pg_x - (w / 2), pg_y - (h / 2), true);
                        over.RestoreState();
                    }
                }
                //doc.Close();
                //return ms.ToArray();
                //}

            }
            catch (Exception)
            {

                throw;
            }
        }

    }

    /// <summary>
    /// Call this class to place the logo
    /// </summary>
    public class PDFHeaderEventHandler : IEventHandler
    {

        public void HandleEvent(Event currentEvent)
        {
            try
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                //var destinationPath = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, @"imgs\SterlingLogo.png");
                string logoPath = AppDomain.CurrentDomain.BaseDirectory + "\\SterlingLogo3.png";
                var logo = ImageDataFactory.Create(logoPath);
                PdfPage page = docEvent.GetPage();
                PdfDocument pdf = docEvent.GetDocument();
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new(page.GetLastContentStream(), page.GetResources(), pdf);
                if (pdf.GetPageNumber(page) == 1)
                {
                    //i want the logo just on page 1
                    pdfCanvas.AddImageAt(logo, pageSize.GetWidth() - logo.GetWidth() - 480, pageSize.GetHeight() - logo.GetHeight() - 15, true);
                    _ = new Canvas(pdfCanvas, pageSize);
                }
                else
                {
                    _ = new Canvas(pdfCanvas, pageSize);
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// PDF Footer EventHandler to handle the footers on all pages
    /// </summary>
    public class PDFFooterEventHandler : IEventHandler
    {
        public void HandleEvent(Event currentEvent)
        {
            try
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;

                PdfPage page = docEvent.GetPage();
                PdfDocument pdf = docEvent.GetDocument();
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new(page.GetLastContentStream(), page.GetResources(), pdf);
                int pageNumber = pdf.GetPageNumber(page);
                int numberOfPages = pdf.GetNumberOfPages();

                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                DeviceRgb offPurpleColour = new(230, 230, 250);

                float[] tableWidth = { 445, 50F };
                Table footerTable = new Table(tableWidth).SetFixedPosition(0F, 15F, pageSize.GetWidth()).SetBorder(Border.NO_BORDER);

                var botom = pageSize.GetBottom() + 15F;
                var getwidth = pageSize.GetWidth();

                footerTable.AddCell(new Cell().Add(new Paragraph("Sterling Bank PLC is authorised by the Central Bank of Nigeria and insured by the National Deposit Insurance Commission and regulated by the Financial Conduct Authority (Company Number: 23336654) Registered Office: 37 Marina Street, Lagos XY11 6TF"))
                                    .SetFont(font).SetFontSize(7F).SetBackgroundColor(offPurpleColour).SetBorder(Border.NO_BORDER).SetPaddingLeft(25F).SetPaddingRight(10F));



                Canvas canvas = new(pdfCanvas, pageSize);
                canvas.Add(footerTable).SetBorder(Border.NO_BORDER);

            }
            catch (Exception)
            {
                //_logger.LogError(ex, "An error occurred while in HandleEvent method in PDFFooterEventHandler class : {RequestId}");

                throw;
            }

        }

        /// <summary>
        /// Call this method Write the page numbers
        /// </summary>
        /// <param name="pdf"> pdfDocument</param>
        /// <param name="document">Document</param>
        public void WritePageNumbers(PdfDocument pdf, Document document)
        {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, PdfEncodings.CP1252, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
            DeviceRgb offPurpleColour = new(230, 230, 250);
            int numberOfPages = pdf.GetNumberOfPages();

            for (int i = 1; i <= numberOfPages; i++)
            {
                // Write aligned text to the specified by parameters point
                document.ShowTextAligned(new Paragraph("Page " + i + " of " + numberOfPages).SetFont(font).SetFontSize(7F).SetBackgroundColor(offPurpleColour).SetBorder(Border.NO_BORDER).SetWidth(50F).SetPaddings(8F, 28F, 9F, 7F).SetTextAlignment(TextAlignment.RIGHT),
                        555, 15.5f, i, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);
            }
        }

        //     Text watermark in pdf document.
       

    }
}
