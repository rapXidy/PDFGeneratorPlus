namespace SterlingPDF.Models
{
    public class LCStatementModel
    {
        public string MessagePrepApplication { get; set; }
        public string UniqueMessageId { get; set; }
        public string Status { get; set; }
        public string Format { get; set; }
        public string SubFormat { get; set; }
        public string Identifier { get; set; }
        public string Expansion { get; set; }
        public string Nature { get; set; }
        public char LT { get; set; }
        public char LT2 { get; set; }
        public string Application { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string TransactionReference { get; set; }
        public string Priority { get; set; }
        public string Monitoring { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime? ValueDate { get; set; }
        public DateTime? ACKNAKRecptDate { get; set; }
        public string TransactionResultDetails { get; set; }
        public string SenderInstitution { get; set; }
        public string ReceiverInstitution { get; set; }
        public string ExpansionSender { get; set; }
        public string ExpansionReceiver { get; set; }
        public string F27_Number { get; set; }
        public string F27_Total { get; set; }
        public string F40A_FormOfCredit { get; set; }
        public string F20_CreditNumber { get; set; }
        public DateTime F31C_DateOfIssue { get; set; }
        public DateTime F31D_DateOfExpiry { get; set; }
        public string F31D_PlaceOfExpiry { get; set; }
        public string F51A_ApplicantBank { get; set; }
        public string F51A_IdentifierCode { get; set; }
        public string F40E_ApplicableRules { get; set; }
        public string F50_Applicant { get; set; }
        public string F59_BeneficiaryName { get; set; }
        public string F59_BeneficiaryAddress { get; set; }
        public string F32B_CurrencyCode { get; set; }
        public string F32B_Amount { get; set; }
        public string F32B_Currency { get; set; }
        public string F41A_IdentifierCode { get; set; }
        public string F41A_Code { get; set; }
        public string F43P_PartialShipment { get; set; }
        public string F43T_TransShipment { get; set; }
        public string F44E_PortOfDeparture { get; set; }
        public string F44F_PortOfDischarge { get; set; }
        public DateTime? F44C_DateOfLatestShipment { get; set; }
        public string F45A_DescriptionOfGoods { get; set; }
        public string F46A_DocumentsRequired { get; set; }
        public string F47A_AdditionalConditions { get; set; }
        public string F71D_Charges { get; set; }
        public string F48_Days { get; set; }
        public string F48_Narrative { get; set; }
        public string F49_ConfirmationInstructions { get; set; }
        public string F78_InstructionAccountNo { get; set; }
        public string F72Z_SenderReceiverInfo { get; set; }
        public string Other_DeliveryOverdueRequest { get; set; }
        public string Other_NetworkRequest { get; set; }
        public string Other_PaymentConfirmationStatus { get; set; }
        public string Other_ConfirmedCurrency { get; set; }
        public string Other_ConfirmedAmount { get; set; }
        public DateTime? Other_ConfirmedDate { get; set; }
        public string GMT { get; internal set; }
        public string F51A_PartyIdentifier { get; internal set; }
    }
}
