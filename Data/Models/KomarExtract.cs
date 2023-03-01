using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAssesment.Data.Models
{
  
    //Taken from csv filename
    [Table("KomarExtract")]
    public class KomarExtract
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
        //[CSVColumn(ImportName = "PO Number")] // would need to do this for all but if using EF, don't need this
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

    public class KomarExtractConfiguration : IEntityTypeConfiguration<KomarExtract>
    {
        public void Configure(EntityTypeBuilder<KomarExtract> builder)
        {
            builder.Property(x => x.Id).HasColumnType("nvarchar(100)");
            builder.Property(x => x.ClaimNumber).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.ClaimDate).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.OpenAmount).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.OriginalAmount).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.Status).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CustomerName).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.ARReasonCode).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CustomerReasonCode).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.AttachmentList).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CheckNumber).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CheckDate).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.Comments).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.DaysOutstanding).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.Division).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.PONumber).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.Brand).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.MergeStatus).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.UnresolvedAmount).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.DocumentType).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.DocumentDate).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.OriginalCustomer).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.Location).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CustomerLocation).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CreateDate).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.LoadId).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.CarrierName).HasColumnType("nvarchar(2500)");
            builder.Property(x => x.InvoiceStoreNumber).HasColumnType("nvarchar(2500)");
        }
    }
}
