using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Models
{
    //took from the raw string of csv
    //Id~
    //Claim Number~
    //Claim Date~
    //Open Amount~
    //Original Amount~
    //Status~
    //Customer Name~
    //AR Reason Code~
    //Customer Reason Code~
    //Attachment List~
    //Check Number~
    //Check Date~
    //Comments~
    //Days Outstanding~
    //Division~
    //PO Number~
    //Brand~
    //Merge Status~
    //Unresolved Amount~
    //Document Type~
    //Document Date~
    //Original Customer~
    //Location~
    //Customer Location~
    //Create Date~
    //Load Id~
    //Carrier Name~
    //Invoice Store Number

    //This really needs a better name but since the PO number is what's needed for this we'll keep it for that 
    public class PONumbertModel
    {
        public string Id { get; set; }
        public string ClaimNumber { get; set; }
        public string ClaimDate { get; set; }
        public string OpenAmount { get; set; }
        public string OriginalAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string ARReasonCode { get; set; }
        public string CustomerReasonCode { get; set; }
        public string AttachmentList { get; set; }
        public string CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public string Comments { get; set; }
        public string DaysOutstanding { get; set; }
        public string Division { get; set; }
        [CSVColumn(ImportName = "PO Number")] // would need to do this for all 
        public string PONumber { get; set; }
        public string Brand { get; set; }
        public string MergeStatus { get; set; }
        public string UnresolvedAmount { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDate { get; set; }
        public string OriginalCustomer { get; set; }
        public string Location { get; set; }
        public string CustomerLocation { get; set; }
        public string CreateDate { get; set; }
        public string LoadId { get; set; }
        public string CarrierName { get; set; }
        public string InvoiceStoreNumber { get; set; }

    }
}
